namespace ToDoTestTask.Application.Services.TimeService;

public interface ITimeService
{
    TimeToDue GetTimeToDueDate(DateTime dueDate);
}