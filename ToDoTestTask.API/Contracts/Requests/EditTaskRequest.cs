namespace ToDoTestTask.Contracts.Requests;

public record EditTaskRequest(string Title, string Description, string DueDate, string Priority, string Status);