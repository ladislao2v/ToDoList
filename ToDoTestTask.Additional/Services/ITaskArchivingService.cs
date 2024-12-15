namespace ToDoTestTask.Additional.Services;

public interface ITaskArchivingService
{
    Task ArchiveTasks(CancellationToken cancellationToken);
}