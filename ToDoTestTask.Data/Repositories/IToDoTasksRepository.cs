using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Data.Repositories;

public interface IToDoTasksRepository
{
    public Task<bool> Create(ToDoTask task, CancellationToken cancellationToken);
    public Task<bool> Edit(ToDoTask task, CancellationToken cancellationToken);
    public Task<List<ToDoTask>> GetAll(string? sortType, CancellationToken cancellationToken);
    public Task<ToDoTask?> Get(int id, CancellationToken cancellationToken);
    public Task<bool> Delete(int id, CancellationToken cancellationToken);
}