using Microsoft.EntityFrameworkCore;
using TodoList2.Model;

namespace TodoList2.Repositories
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
