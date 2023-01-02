using System.Text;
using Microsoft.AspNetCore.Mvc;
using ViThor.HttpTracing.Controllers;
using ViThor.HttpTracing.Sample.Shared.Models;
using ViThor.HttpTracing.Utils;

namespace ViThor.HttpTracing.Sample.AppOne.Controllers;

[ApiController]
[Route("[controller]")]
// ViThorControllerBase is a custom controller that inherits from ControllerBase and adds a CorrelationId property
public class TodoController : ViThorControllerBase
{
    public TodoController(IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : base(httpContextAccessor, httpClient)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        Console.WriteLine($"TraceID: {CorrelationId}");

        var response = await _httpClient.GetAsync("http://localhost:5002/todo");
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return BadRequest(content);
        }

        var items = JsonManagerSerialize.Deserialize<IEnumerable<TodoItem>>(content);
        return items.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> Get(int id)
    {
        Console.WriteLine($"TraceID: {CorrelationId}");

        var response = await _httpClient.GetAsync($"http://localhost:5002/todo/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var item = JsonManagerSerialize.Deserialize<TodoItem>(content);
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Post(TodoItem todoItem)
    {
        Console.WriteLine($"TraceID: {CorrelationId}");

        var content = JsonManagerSerialize.Serialize(todoItem);
        var response = await _httpClient.PostAsync("http://localhost:5002/todo", new StringContent(content, Encoding.UTF8, "application/json"));
        var responseContent = await response.Content.ReadAsStringAsync();
        var item = JsonManagerSerialize.Deserialize<TodoItem>(responseContent);
        return item;
    }


    [HttpPost("test-exception")]
    public async Task<ActionResult<TodoItem>> PostException(TodoItem todoItem)
    {
        Console.WriteLine($"TraceID: {CorrelationId}");

        var content = JsonManagerSerialize.Serialize(todoItem);
        var response = await _httpClient.PostAsync("http://localhost:5002/todo/test-exception", new StringContent(content, Encoding.UTF8, "application/json"));
        var responseContent = await response.Content.ReadAsStringAsync();

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                break;
            case System.Net.HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, responseContent ?? content);
            default:
                return BadRequest(responseContent);
        }

        var item = JsonManagerSerialize.Deserialize<TodoItem>(responseContent ?? content);
        return item;
    }
}