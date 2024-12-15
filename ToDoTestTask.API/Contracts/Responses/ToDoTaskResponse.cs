namespace ToDoTestTask.Contracts.Responses;

public record ToDoTaskResponse(int Id, string Title, string Description, string DueDate, 
    string Priority, string Status, int Days, int Hours, bool IsRunningOut);