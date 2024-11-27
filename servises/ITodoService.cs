using TodoList2.Model;

namespace TodoList2.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<TodoItem> UpdateAsync(int id, TodoItem todoItem);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TodoItem>> SearchAsync(string query);
        Task<IEnumerable<TodoItem>> GetByCategoryAsync(string category);
    }
}
