using ToDoTestTask.Application.DataValidators;
using ToDoTestTask.Application.Services.TimeService;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Core.Models;
using ToDoTestTask.Data.Repositories;
using Xunit;

namespace ToDoTestTask.Tests;

public class ToDoTaskServiceTests
{
    [Fact]
    public async Task TryCreate_InputValidData_ReturnTrue()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
                );

        string title = "Task";
        string description = "Description";
        string dueDate = "31.12.2024 14:00";
        string priority = "High";

        bool result = await toDoTasksService.TryCreate(
            title,
            description,
            dueDate,
            priority,
            CancellationToken.None);
        
        Assert.True(result);
    }

    [Fact]
    public async Task TryCreate_InputInvalidData_ReturnFalse()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        string title = "";
        string description = "Description";
        string dueDate = "10";
        string priority = "2";

        bool result = await toDoTasksService.TryCreate(
            title,
            description,
            dueDate,
            priority,
            CancellationToken.None);
        
        Assert.False(result);
    }
    
    [Fact]
    public async Task TryCreate_InputPastDay_ReturnFalse()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        string title = "Task";
        string description = "Description";
        string dueDate = "10.12.2024 14:00";
        string priority = "High";
        bool result = await toDoTasksService.TryCreate(
            title,
            description,
            dueDate,
            priority,
            CancellationToken.None);
        
        Assert.False(result);
    }

    [Fact]
    public async Task TryEdit_InputValidData_ReturnTrue()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        int id = 1;
        string title = "Task";
        string description = "Description";
        string dueDate = "31.12.2024 14:00";
        string priority = "High";
        string status = "Low";

        bool result = await toDoTasksService.TryEdit(
            id,
            title,
            description,
            dueDate,
            priority,
            status,
            CancellationToken.None);
        
        Assert.True(result);
    }

    [Fact]
    public async Task TryEdit_InputInvalidData_ReturnFalse()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        int id = 1;
        string title = "Task";
        string description = "Description";
        string dueDate = "31.12.2024 14:00";
        string priority = "High";
        string status = "Low";

        bool result = await toDoTasksService.TryEdit(
            id,
            title,
            description,
            dueDate,
            priority,
            status,
            CancellationToken.None);
        
        Assert.True(result);
    }

    [Fact]
    public async Task TryEdit_InputPastDay_ReturnFalse()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        int id = 2;
        string title = "Task1";
        string description = "Description2";
        string dueDate = "31.12.2024 14:00";
        string priority = "Medium";
        string status = "InProgress";

        bool result = await toDoTasksService.TryEdit(
            id,
            title,
            description,
            dueDate,
            priority,
            status,
            CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task TryEdit_InputNonExistentId()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        int id = 2;
        string title = "Task1";
        string description = "Description2";
        string dueDate = "31.12.2024 14:00";
        string priority = "Medium";
        string status = "InProgress";

        bool result = await toDoTasksService.TryEdit(
            id,
            title,
            description,
            dueDate,
            priority,
            status,
            CancellationToken.None);

        Assert.False(result);
    }
    
    [Fact]
    public async Task GetAll()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        List<ToDoTask> result = await toDoTasksService.GetAll(null, CancellationToken.None);
        
        Assert.True(result.Count > 0);
    }
    
    [Fact]
    public async Task GetAll_InputStatusSort()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        List<ToDoTask> nonSorted = await toDoTasksService.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await toDoTasksService.GetAll("status", CancellationToken.None);
        
        Assert.Equal(result, nonSorted.OrderBy(t => t.Status).ToList());
    }
    
    [Fact]
    public async Task GetAll_InputPrioritySort()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        List<ToDoTask> nonSorted = await toDoTasksService.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await toDoTasksService.GetAll("priority", CancellationToken.None);
        
        Assert.Equal(result, nonSorted.OrderBy(t => t.Priority).ToList());
    }
    
    [Fact]
    public async Task GetAll_InputDueDateSort()
    {
        IToDoTasksService toDoTasksService =
            CreateService(
                "Connection string to test bd"
            );

        List<ToDoTask> nonSorted = await toDoTasksService.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await toDoTasksService.GetAll("duetime", CancellationToken.None);
        
        Assert.Equal(result, nonSorted.OrderBy(t => t.DueTime).ToList());
    }
    
    [Fact]
    public async Task Get_InputNonExpiringDayInEightHours_ReturnTrue()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
                );

        ToDoTask? result = await toDoTaskService.Get(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsRunningOut);
    }
    
    [Fact]
    public async Task Get_InputExpiringDayInEightHours_ReturnTrue()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
            );

        ToDoTask? result = await toDoTaskService.Get(2, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsRunningOut);
    }
    
    [Fact]
    public async Task Get_InputExistentId()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
            );

        ToDoTask? result = await toDoTaskService.Get(1, CancellationToken.None);

        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Get_InputNonExistentId()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
            );

        ToDoTask? result = await toDoTaskService.Get(100, CancellationToken.None);

        Assert.Null(result);
    }
    
    [Fact]
    public async Task Delete_InputExistentId()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
            );

        bool result = await toDoTaskService.TryDelete(1, CancellationToken.None);

        Assert.True(result);
    }
    
    [Fact]
    public async Task Delete_InputNonExistentId()
    {
        IToDoTasksService toDoTaskService = 
            CreateService(
                "Connection string to test bd"
            );

        bool result = await toDoTaskService.TryDelete(100, CancellationToken.None);

        Assert.False(result);
    }

    private IToDoTasksService CreateService(string connectionString)
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository(connectionString);
        ITimeService timeService = new TimeService();
        ITaskRequireDataValidator validator = new TaskRequireDataValidator();
        return new ToDoTasksService(repository, timeService, validator);
    }
}