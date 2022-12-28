using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViThor.HttpTracing.Utils;

namespace ViThor.HttpTracing.Middlewares
{
    public class ViThorExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ViThorExceptionHandlingMiddleware> _logger;

        public ViThorExceptionHandlingMiddleware(RequestDelegate next, ILogger<ViThorExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var code = HttpStatusCode.InternalServerError;

            var result = JsonManagerSerialize.Serialize(
                new
                {
                    error = exception.Message,
                    traceID = context.TraceIdentifier
                }
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}