using Microsoft.AspNetCore.Mvc;
using ViThor.HttpTracing.Sample.Shared.Models;

namespace ViThor.HttpTracing.Sample.AppTwo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetAll()
    {
        return new List<TodoItem>
        {
            new TodoItem { Id = 1, Name = "Test", IsComplete = true },
            new TodoItem { Id = 2, Name = "Test2", IsComplete = false }
        };
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        return new TodoItem { Id = id, Name = "Test", IsComplete = true };
    }

    [HttpPost]
    public ActionResult<TodoItem> Post(TodoItem todoItem)
    {
        return todoItem;
    }

    [HttpPost("test-exception")]
    public ActionResult<TodoItem> PostException(TodoItem todoItem)
    {
        throw new Exception("Test exception App Two");
    }
}
