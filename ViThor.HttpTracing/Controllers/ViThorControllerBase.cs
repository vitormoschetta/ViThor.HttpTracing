using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViThor.HttpTracing.Extensions;

namespace ViThor.HttpTracing.Controllers
{
    /// <summary>
    /// Controlador base que adiciona uma propriedade 'X-Correlation-ID' no cabeçalho da instância HttpClient com o valor do tracing. 
    /// <para/>
    /// Base controller that adds an 'X-Correlation-ID' property in the header of the HttpClient instance with the tracing value.
    /// </summary>
    [ApiController]
    public class ViThorControllerBase : ControllerBase
    {
        protected readonly string CorrelationId;
        protected readonly HttpClient _httpClient;

        public ViThorControllerBase(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            CorrelationId = httpContextAccessor.HttpContext.Request.GetCorrelationId();
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Correlation-ID", CorrelationId);
        }
    }
}