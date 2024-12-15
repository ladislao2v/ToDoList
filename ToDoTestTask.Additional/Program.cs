using ToDoTestTask.Additional.Services;
using ToDoTestTask.Application.DataValidators;
using ToDoTestTask.Application.Services.TimeService;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Data.Repositories;

ITimeService timeService = new TimeService();
ITaskRequireDataValidator validator = new TaskRequireDataValidator();
IToDoTasksRepository repository = new ToDoTasksRepository("Server=your_server;Database=your_database;User Id=your_user;Password=your_password;");
IToDoTasksService toDoTasksService = new ToDoTasksService(repository, timeService, validator, true);
ITaskArchivingService taskArchivingService = new TaskArchivingService(toDoTasksService);

while (true)
{
    await Task.Delay(TimeSpan.FromHours(1));
    await taskArchivingService.ArchiveTasks(CancellationToken.None);
}