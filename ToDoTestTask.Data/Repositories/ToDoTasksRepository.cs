using Microsoft.Data.SqlClient;
using ToDoTestTask.Core.Models;
using ToDoTestTask.Data.Extensions;
using static ToDoTestTask.Data.Constants.TaskQueries;

namespace ToDoTestTask.Data.Repositories;

public sealed class ToDoTasksRepository : IToDoTasksRepository
{
    private readonly string? _connectionString;
    private readonly Dictionary<string, Func<ToDoTask, object>> _sorts = new()
    {
        ["status"] = t => t.Status,
        ["priority"] = t => t.Priority,
        ["duedate"] = t => t.DueTime,
    };

    public ToDoTasksRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<bool> Create(ToDoTask task, CancellationToken cancellationToken)
    {
        await using SqlCommand command = 
            await CreateCommandByQuery(CreateTaskQuery);

        command.Parameters.AddTaskParameters(task);

        return await TryExecuteNonQueryCommand(command, cancellationToken);
    }

    public async Task<bool> Edit(ToDoTask task, CancellationToken cancellationToken)
    {
        await using SqlCommand command = 
            await CreateCommandByQuery(EditTaskQuery);

        command.Parameters.AddTaskParametersWithId(task);

        return await TryExecuteNonQueryCommand(command, cancellationToken);
    }

    public async Task<List<ToDoTask>> GetAll(string? sortType, CancellationToken cancellationToken)
    {
        await using SqlCommand command = 
            await CreateCommandByQuery(SelectAllFromTasksQuery);
        
        await command.Connection.OpenAsync(cancellationToken);

        await using var reader = await command
            .ExecuteReaderAsync(cancellationToken);

        List<ToDoTask> tasks = new();
        
        while (await reader.ReadAsync(cancellationToken))
            tasks.Add(reader.GetTask());

        if (sortType != null)
            tasks = tasks
                .OrderBy(_sorts[sortType])
                .ToList();
        
        return tasks;
    }

    public async Task<ToDoTask?> Get(int id, CancellationToken cancellationToken)
    {
        await using SqlCommand command =
            await CreateCommandByQuery(FindTaskByIdQuery);
        
        await command.Connection.OpenAsync(cancellationToken);
        
        command.Parameters.AddWithValue("@Id", id);

        await using var reader = await command
            .ExecuteReaderAsync(cancellationToken);

        if(await reader.ReadAsync(cancellationToken))
            return reader.GetTask();

        return null;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await using SqlCommand command = 
            await CreateCommandByQuery(DeleteByIdQuery);

        command.Parameters.AddWithValue("@Id", id);

        return await TryExecuteNonQueryCommand(command, cancellationToken);
    }

    private async Task<SqlCommand> CreateCommandByQuery(string query)
    {
        SqlConnection connection = 
            new SqlConnection(_connectionString);
        
        return new SqlCommand(query, connection);
    }

    private async Task<bool> TryExecuteNonQueryCommand(SqlCommand command, CancellationToken cancellationToken)
    {
        await command.Connection.OpenAsync(cancellationToken);
        
        int rowsAffected = await command
            .ExecuteNonQueryAsync(cancellationToken);

        return rowsAffected > 0;
    }
}