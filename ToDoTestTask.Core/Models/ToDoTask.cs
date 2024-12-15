using System.Globalization;

namespace ToDoTestTask.Core.Models;

public class ToDoTask
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DueTime { get; set; }
    
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }

    public int? Days { get; set; }
    public int? Hours { get; set; }
    public bool IsRunningOut => Days == 0 && Hours <= 8; 
    
    
    public enum TaskStatus
    {
        InProgress = 0,
        New = 1,
        Completed = 2,
        Archived = 3,
    }
    
    public enum TaskPriority
    {
        High = 0,
        Medium = 1,
        Low = 2,
    }
    
    public static ToDoTask Create(string title, string description, string dueDate, string priority, 
        string status="New", int? id = null, int? days = null, int? hours = null) =>
        new()
        {
            Id = id,
            Title = title,
            Description = description,
            DueTime = DateTime.TryParseExact(
                dueDate, 
                "dd.MM.yyyy HH:mm", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out DateTime date)
            ? date
            : throw new ArgumentException(nameof(dueDate)),
            Priority = Enum
                .TryParse(priority,
                    true, out ToDoTask.TaskPriority priorityItem)
                ? priorityItem
                : throw new ArgumentException(nameof(priority)),
            Status = Enum
                .TryParse(status, true, out ToDoTask.TaskStatus statusItem)
                ? statusItem
                : throw new ArgumentException(nameof(status)),
            Days = days,
            Hours = hours
        };
    
    public static ToDoTask Create(string title, string description, DateTime dueDate, TaskPriority priority, TaskStatus status, 
        int? id = null, int? days = null, int? hours = null) =>
        new()
        {
            Id = id,
            Title = title,
            Description = description,
            DueTime = dueDate,
            Priority = priority,
            Status = status,
            Days = days,
            Hours = hours
        };
}