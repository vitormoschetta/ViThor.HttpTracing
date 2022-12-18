using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ViThor.HttpTracing.Extensions;
using ViThor.HttpTracing.Models;
using ViThor.HttpTracing.Utils;

namespace ViThor.HttpTracing.Filters
{
    /// <summary>
    /// Filtro responsável por identificar o tracing da requisição (chave 'X-Correlation-ID'), fazer o logging da requisição (Username, Controller, Action, Parmams, etc), 
    /// adicionar o tracing no cabeçalho da resposta, e fazer o logging da resposta.
    /// <para/>
    /// Filter responsible for identifying the request tracing (key 'X-Correlation-ID'), logging the request (Username, Controller, Action, Parmams, etc),
    /// adding the tracing to the response header, and logging the response.    
    /// </summary>
    public class ViThorTraceFilter : IAsyncActionFilter
    {
        private readonly ILogger<ViThorTraceFilter> _logger;

        public ViThorTraceFilter(ILogger<ViThorTraceFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var loggingRequestModel = new LoggingRequestModel
            {
                CorrelationId = context.GetCorrelationId(),
                Username = context.HttpContext.User?.Identity?.Name,
                Controller = context.ActionDescriptor.RouteValues["controller"],
                Action = context.ActionDescriptor.RouteValues["action"],
                QueryParameters = context.HttpContext.Request.QueryString.Value,
                Arguments = JsonManagerSerialize.Serialize(context.ActionArguments)
            };

            _logger.LogInformation(loggingRequestModel.ToString());

            var resultnext = await next();

            resultnext.HttpContext.Response.Headers.Add(HttpRequestExtension.CorrelationAttributeName, loggingRequestModel.CorrelationId);

            var loggingResponseModel = new LoggingResponseModel
            {
                CorrelationId = loggingRequestModel.CorrelationId,
                Username = loggingRequestModel.Username,
                Controller = loggingRequestModel.Controller,
                Action = loggingRequestModel.Action
            };

            var objectResult = resultnext.Result as ObjectResult;
            if (objectResult != null)
            {
                var result = JsonManagerSerialize.Serialize(objectResult.Value);

                loggingResponseModel.Result = result;
            }

            _logger.LogInformation(loggingResponseModel.ToString());
        }
    }
}