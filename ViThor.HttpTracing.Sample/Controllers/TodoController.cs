using Microsoft.AspNetCore.Mvc;
using ViThor.HttpTracing.Sample.Models;

namespace ViThor.HttpTracing.Sample.Controllers;

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
}
