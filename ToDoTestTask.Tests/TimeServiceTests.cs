using ToDoTestTask.Application.Services.TimeService;
using Xunit;

namespace ToDoTestTask.Tests;

public class TimeServiceTests
{
    [Fact]
    public void GetTimeToDueDate_ReturnPositiveTime()
    {
        TimeService timeService = new TimeService();

        DateTime tomorrow = DateTime.Now.AddDays(1);

        TimeToDue result = timeService.GetTimeToDueDate(tomorrow);
        
        Assert.True(result.Days >= 0);
        Assert.True(result.Hours >= 0);
    }
    
    [Fact]
    public void GetTimeToDueDate_ReturnNegativeTime()
    {
        TimeService timeService = new TimeService();

        DateTime yesterday = DateTime.Now.AddDays(-1);

        TimeToDue result = timeService.GetTimeToDueDate(yesterday);
        
        Assert.True(result.Days <= 0);
        Assert.True(result.Hours < 0);
    }
}