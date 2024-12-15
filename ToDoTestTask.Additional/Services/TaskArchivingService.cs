using Microsoft.IdentityModel.Tokens;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Core.Extensions;
using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Additional.Services;

public sealed class TaskArchivingService : ITaskArchivingService
{
    private const int MaxDaysToArchive = 0;
    private const int MaxHoursToArchive = -8;

    private readonly IToDoTasksService _toDoTasksService;

    public TaskArchivingService(IToDoTasksService toDoTasksService)
    {
        _toDoTasksService = toDoTasksService;
    }
    
    public async Task ArchiveTasks(CancellationToken cancellationToken)
    {
        var tasks = await _toDoTasksService
            .GetAll(null, cancellationToken);
        
        if(tasks.IsNullOrEmpty())
            return;

        var tasksToArchive = tasks
            .Where(t => t.Days == MaxDaysToArchive 
                        && t.Hours == MaxHoursToArchive);

        await ChangeStatusToArchived(tasksToArchive, cancellationToken);
    }

    private async Task ChangeStatusToArchived(IEnumerable<ToDoTask> tasksToArchive, CancellationToken cancellationToken)
    {
        var editEntities = tasksToArchive
            .Select(t => ToDoTask
                .Create(
                    t.Title,
                    t.Description,
                    t.DueTime.DateToString(),
                    t.Priority.ToString(),
                    ToDoTask.TaskStatus.Archived.ToString(),
                    t.Id));

        foreach (ToDoTask entity in editEntities)
            await _toDoTasksService.TryEdit(
                entity.Id.GetValueOrDefault(), 
                entity.Title, 
                entity.Description, 
                entity.DueTime.DateToString(), 
                entity.Priority.ToString(), 
                entity.Status.ToString(), 
                cancellationToken);
    }
}