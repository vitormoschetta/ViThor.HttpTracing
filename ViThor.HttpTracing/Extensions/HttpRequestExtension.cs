using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ViThor.HttpTracing.Extensions
{
    public static class HttpRequestExtension
    {
        public static string CorrelationAttributeName = "X-Correlation-ID";

        public static string GetCorrelationId(this ActionExecutingContext request)
        {

            var correlationId = request?.HttpContext?.Request?.Headers[CorrelationAttributeName].ToString();

            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = createCorrelationId();
                request?.HttpContext.Request.Headers.Add(CorrelationAttributeName, correlationId);
            }

            return correlationId;
        }

        public static string GetCorrelationId(this HttpRequest request)
        {
            var correlationId = request?.Headers[CorrelationAttributeName].ToString();

            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = createCorrelationId();
                request?.Headers.Add(CorrelationAttributeName, correlationId);
            }

            return correlationId;
        }

        private static string createCorrelationId() => System.Guid.NewGuid().ToString();
    }
}