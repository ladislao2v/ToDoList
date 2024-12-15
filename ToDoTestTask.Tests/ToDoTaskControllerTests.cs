using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoTestTask.Application.DataValidators;
using ToDoTestTask.Application.Services.TimeService;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Contracts.Requests;
using ToDoTestTask.Contracts.Responses;
using ToDoTestTask.Controllers;
using ToDoTestTask.Core.Extensions;
using ToDoTestTask.Core.Models;
using ToDoTestTask.Data.Repositories;
using Xunit;

namespace ToDoTestTask.Tests;

public class ToDoTaskControllerTests
{

    [Fact]
    public async Task CreateTask_ReturnsOk()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );

        CreateTaskRequest request = new CreateTaskRequest(
            "Task", 
            "Description", 
            "31.12.2024 14:00", 
            "High");


        IActionResult result = await controller
            .CreateTask(request, CancellationToken.None);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateTask_ReturnsBadRequest()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );
        
        CreateTaskRequest request = new CreateTaskRequest(
            "Task", 
            "Description", 
            "10.12.2024 14:00", 
            "High");
        
        IActionResult result = await controller.CreateTask(request, CancellationToken.None);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task EditTask_ReturnsOk()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );

        int id = 1;
        EditTaskRequest request = new EditTaskRequest(
            "Task", 
            "Description", 
            "31.12.2024 14:00", 
            "High", 
            "New");


        IActionResult result = await controller.EditTask(id, request, CancellationToken.None);

        Assert.IsType<Ok>(result);
    }

    [Fact]
    public async Task EditTask_ReturnsBadRequest()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );

        int id = 1;
        EditTaskRequest request = new EditTaskRequest(
            "Task", 
            "Description", 
            "10.12.2024 14:00", 
            "High", 
            "New");


        IActionResult result = await controller.EditTask(id, request, CancellationToken.None);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetTask_ReturnsOk()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );
        
        var task = ToDoTask.Create(
            "Task", 
            "Description", 
            DateTime.Now.AddDays(1),
            ToDoTask.TaskPriority.High, 
            ToDoTask.TaskStatus.New, 
            1,
            1, 
            0);
        
        var result = await controller
            .GetTask(1, CancellationToken.None);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ToDoTaskResponse>(okResult.Value);
        Assert.Equal(task.Id, response.Id);
        Assert.Equal(task.Title, response.Title);
    }
    
    [Fact]
    public async Task GetTask_ReturnsBad()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );
        
        var result = await controller
            .GetTask(100, CancellationToken.None);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsOk()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );
        
        var result = await controller
            .DeleteTask(1, CancellationToken.None);
        
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsBad()
    {
        ToDoTaskController controller = 
            CreateController(
                "Connection string to test bd"
                );

        var result = await controller
            .DeleteTask(100, CancellationToken.None);

        Assert.IsType<BadRequestResult>(result);
    }

    private ToDoTaskController CreateController(string connectionString)
    {
        IToDoTasksRepository repository = 
            new ToDoTasksRepository(connectionString);
        ITimeService timeService = new TimeService();
        ITaskRequireDataValidator validator = new TaskRequireDataValidator();
        IToDoTasksService tasksService = new ToDoTasksService(repository, timeService, validator);
        ToDoTaskController controller = new ToDoTaskController(tasksService);
        return controller;
    }
}