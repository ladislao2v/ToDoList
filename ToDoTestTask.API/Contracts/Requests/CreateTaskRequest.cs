namespace ToDoTestTask.Contracts.Requests;

public record CreateTaskRequest(string Title, string Description, string DueDate, string Priority);