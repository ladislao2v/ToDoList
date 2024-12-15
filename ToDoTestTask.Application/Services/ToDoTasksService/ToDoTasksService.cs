using ToDoTestTask.Application.DataValidators;
using ToDoTestTask.Application.Services.TimeService;
using ToDoTestTask.Core.Models;
using ToDoTestTask.Data.Repositories;

namespace ToDoTestTask.Application.Services.ToDoTasksService;

public sealed class ToDoTasksService : IToDoTasksService
{
    private readonly IToDoTasksRepository _repository;
    private readonly ITimeService _timeService;
    private readonly ITaskRequireDataValidator _validator;
    private readonly bool _canCreateOrEditAll;

    public ToDoTasksService(IToDoTasksRepository repository, ITimeService timeService, 
        ITaskRequireDataValidator validator, bool canCreateOrEditAll = false)
    {
        _repository = repository;
        _timeService = timeService;
        _validator = validator;
        _canCreateOrEditAll = canCreateOrEditAll;
    }
    
    public async Task<bool> TryCreate(string title, string description, string dueDate, string priority, CancellationToken cancellationToken)
    {
        if (!_validator.TryValidate(title, dueDate, priority))
            throw new ArgumentException("Invalid data");

        ToDoTask task = ToDoTask
            .Create(title, description, dueDate, priority);

        if (!CanCreateOrEdit(task.DueTime))
            return false;

        return await _repository.Create(task, cancellationToken);
    }

    public async Task<bool> TryEdit(int id, string title, string description, string dueDate, string priority, string status, CancellationToken cancellationToken)
    {
        if (!_validator.TryValidate(title, dueDate, priority, status))
            throw new ArgumentException("Invalid data");

        ToDoTask? task = await Get(id, cancellationToken);

        if (task == null || !CanCreateOrEdit(task.DueTime))
            return false;
        
        task = ToDoTask
            .Create(title, description, dueDate, priority, status, id);

        return await _repository.Edit(task, cancellationToken);
    }

    public async Task<List<ToDoTask>> GetAll(string? sortType, CancellationToken cancellationToken)
    {
        List<ToDoTask> tasks = await _repository
            .GetAll(sortType, cancellationToken);

        tasks
            .ForEach(t =>
            {
                t.Days = _timeService.GetTimeToDueDate(t.DueTime).Days;
                t.Hours = _timeService.GetTimeToDueDate(t.DueTime).Hours;
            });

        return tasks;
    }

    public async Task<ToDoTask?> Get(int id, CancellationToken cancellationToken)
    {
        ToDoTask? entity = await _repository
            .Get(id, cancellationToken);

        if (entity == null)
            return null;

        int days = _timeService
            .GetTimeToDueDate(entity.DueTime)
            .Days;
        int hours = _timeService
            .GetTimeToDueDate(entity.DueTime)
            .Hours;

        entity.Days = days;
        entity.Hours = hours;

        return entity;
    }

    public async Task<bool> TryDelete(int id, CancellationToken cancellationToken) => 
        await _repository.Delete(id, cancellationToken);

    private bool CanCreateOrEdit(DateTime dueTime)
    {
        if (_canCreateOrEditAll)
            return true;
        
        TimeToDue timeToDue = _timeService
            .GetTimeToDueDate(dueTime);

        if (timeToDue.Days <= 0 && timeToDue.Hours < 0)
            return false;

        return true;
    }
}