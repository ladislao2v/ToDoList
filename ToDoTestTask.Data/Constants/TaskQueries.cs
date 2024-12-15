namespace ToDoTestTask.Data.Constants;

public static class TaskQueries
{
    public const string CreateTaskQuery = @"INSERT INTO Tasks (Title, Description, DueDate, Priority, Status) 
                                            VALUES (@Title, @Description, @DueDate, @Priority, @Status)";

    public const string EditTaskQuery = @"UPDATE Tasks
                                        SET Title = @Title,  
                                        Description = @Description, 
                                        DueDate = @DueDate,
                                        Priority = @Priority,
                                        Status = @Status
                                        WHERE Id = @Id;";

    public const string SelectAllFromTasksQuery = @"SELECT Id, Title, Description, DueDate, Priority, Status FROM Tasks";

    public const string FindTaskByIdQuery = @"SELECT Id, Title, Description, DueDate, Priority, Status FROM Tasks
                                        WHERE Id = @Id";

    public const string DeleteByIdQuery = @"DELETE FROM Tasks 
                                        WHERE Id = @Id;";

}