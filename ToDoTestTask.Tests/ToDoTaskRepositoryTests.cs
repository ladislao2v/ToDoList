using ToDoTestTask.Core.Models;
using ToDoTestTask.Data.Repositories;
using Xunit;

namespace ToDoTestTask.Tests;

public class ToDoTaskRepositoryTests
{
    [Fact]
    public async Task Create_ReturnTrue()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        ToDoTask entity = ToDoTask.Create(
            "Task",
            "Description",
            "31.12.2024 14:00",
            "High",
            "New"
        );

        bool result = await repository.Create(entity, CancellationToken.None);

        Assert.True(result);
    }
    
    [Fact]
    public async Task Create_ReturnFalse()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        ToDoTask entity = ToDoTask.Create(
            "Task",
            "Description",
            "10.12.2024 14:00",
            "High",
            "New"
        );

        bool result = await repository.Create(entity, CancellationToken.None);

        Assert.False(result);
    }

    
    [Fact]
    public async Task Edit()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        ToDoTask entity = ToDoTask.Create(
            "Task",
            "Description",
            "31.12.2024 14:00",
            "High",
            "New",
            1
        );

        bool result = await repository.Edit(entity, CancellationToken.None);

        Assert.True(result);
    }
    
    [Fact]
    public async Task GetAll()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        List<ToDoTask> result = await repository.GetAll(null, CancellationToken.None);

        Assert.True(result.Count > 0);
    }
    
    [Fact]
    public async Task GetAll_InputStatusSort()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        List<ToDoTask> nonSorted = await repository.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await repository.GetAll("status", CancellationToken.None);

        Assert.Equal(result, nonSorted.OrderBy(t => t.Status).ToList());
    }
    
    [Fact]
    public async Task GetAll_InputPrioritySort()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        List<ToDoTask> nonSorted = await repository.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await repository.GetAll("priority", CancellationToken.None);

        Assert.Equal(result, nonSorted.OrderBy(t => t.Priority).ToList());
    }
    
    [Fact]
    public async Task GetAll_InputDueDateSort()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        List<ToDoTask> nonSorted = await repository.GetAll(null, CancellationToken.None);
        List<ToDoTask> result = await repository.GetAll("duetime", CancellationToken.None);

        Assert.Equal(result, nonSorted.OrderBy(t => t.DueTime).ToList());
    }
    
    [Fact]
    public async Task Get_InputExistentId()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        ToDoTask? result = await repository.Get(1, CancellationToken.None);

        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Get_InputNonExistentId()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        ToDoTask? result = await repository.Get(100, CancellationToken.None);

        Assert.Null(result);
    }
    
    [Fact]
    public async Task Delete_InputExistentId()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bd");

        bool result = await repository.Delete(1, CancellationToken.None);

        Assert.True(result);
    }
    
    [Fact]
    public async Task Delete_InputNonExistentId()
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository("Connection string to test bdd");

        bool result = await repository.Delete(100, CancellationToken.None);

        Assert.False(result);
    }
}