using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Application.Services.ToDoTasksService;

public interface IToDoTasksService
{
    public Task<bool> TryCreate(string title, string description, string dueDate, string priority, CancellationToken cancellationToken);
    public Task<bool> TryEdit(int id, string title, string description, string dueDate, string priority, string status, CancellationToken cancellationToken);
    public Task<List<ToDoTask>> GetAll(string? sortType, CancellationToken cancellationToken);
    public Task<ToDoTask?> Get(int id, CancellationToken cancellationToken);
    public Task<bool> TryDelete(int id, CancellationToken cancellationToken);
}