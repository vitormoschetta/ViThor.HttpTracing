using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ViThor.HttpTracing.Extensions
{
    public static class HttpRequestExtension
    {
        public static string CorrelationAttributeName = "X-Correlation-ID";

        public static string GetCorrelationId(this ActionExecutingContext request)
        {
            if (request.HttpContext.TraceIdentifier.Contains("->"))
                return request.HttpContext.TraceIdentifier;

            var callerTraceId = request.HttpContext.Request.Headers[CorrelationAttributeName].ToString() ?? string.Empty;
            var receiverTraceId = request.HttpContext.TraceIdentifier;
            var correlationId = $"[{callerTraceId}] -> [{receiverTraceId}]";

            request.HttpContext.TraceIdentifier = correlationId;

            return correlationId;
        }

        public static string GetCorrelationId(this HttpRequest request)
        {
            if (request.HttpContext.TraceIdentifier.Contains("->"))
                return request.HttpContext.TraceIdentifier;

            var callerTraceId = request.HttpContext.Request.Headers[CorrelationAttributeName].ToString() ?? string.Empty;
            var receiverTraceId = request.HttpContext.TraceIdentifier;
            var correlationId = $"[{callerTraceId}] -> [{receiverTraceId}]";

            request.HttpContext.TraceIdentifier = correlationId;

            return correlationId;
        }
    }
}