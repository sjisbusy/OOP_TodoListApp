using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListApp
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public class TodoModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
