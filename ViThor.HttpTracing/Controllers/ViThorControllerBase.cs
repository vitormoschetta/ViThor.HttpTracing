using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViThor.HttpTracing.Extensions;

namespace ViThor.HttpTracing.Controllers
{
    [ApiController]
    public class ViThorControllerBase : ControllerBase
    {
        protected readonly string CorrelationId;
        // protected readonly HttpClient _httpClient;

        public ViThorControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            CorrelationId = httpContextAccessor.HttpContext.Request.GetCorrelationId();
        }
    }
}