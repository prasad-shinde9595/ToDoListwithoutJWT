using Microsoft.EntityFrameworkCore;
using TodoList2.Model;
using TodoList2.Repositories;

namespace TodoList2.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateAsync(int id, TodoItem todoItem)
        {
            var existingItem = await _context.TodoItems.FindAsync(id);
            if (existingItem == null) return null;

            existingItem.Title = todoItem.Title;
            existingItem.Description = todoItem.Description;
            existingItem.Category = todoItem.Category;
            existingItem.Priority = todoItem.Priority;
            existingItem.IsCompleted = todoItem.IsCompleted;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return false;

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TodoItem>> SearchAsync(string query)
        {
            return await _context.TodoItems
                .Where(t => t.Title.Contains(query) || t.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetByCategoryAsync(string category)
        {
            return await _context.TodoItems
                .Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
