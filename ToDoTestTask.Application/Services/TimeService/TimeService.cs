namespace ToDoTestTask.Application.Services.TimeService;

public sealed class TimeService : ITimeService
{
    public TimeToDue GetTimeToDueDate(DateTime dueDate)
    {
        TimeSpan timeSpan = dueDate - DateTime.Now;
        
        return new (timeSpan.Days, timeSpan.Hours);
    }
}