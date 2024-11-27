using Microsoft.AspNetCore.Mvc;
using TodoList2.Model;
using TodoList2.Services;

namespace TodoList2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return Ok(await _todoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoService.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound(new { Message = "Todo item not found." });
            }
            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdItem = await _todoService.CreateAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, [FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedItem = await _todoService.UpdateAsync(id, todoItem);
            if (updatedItem == null)
            {
                return NotFound(new { Message = "Todo item not found." });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var success = await _todoService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Todo item not found." });
            }
            return NoContent();
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> SearchTodoItems([FromQuery] string query)
        {
            return Ok(await _todoService.SearchAsync(query));
        }

        [HttpGet("Category/{category}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByCategory(string category)
        {
            return Ok(await _todoService.GetByCategoryAsync(category));
        }
    }
}
