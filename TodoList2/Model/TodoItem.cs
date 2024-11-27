namespace TodoList2.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // e.g., "Work" or "Personal"
        public string Priority { get; set; } // e.g., "High", "Medium", "Low"
        public bool IsCompleted { get; set; } = false;
    }
}
