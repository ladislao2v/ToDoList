using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDoTestTask.Application.Services.ToDoTasksService;
using ToDoTestTask.Contracts.Requests;
using ToDoTestTask.Contracts.Responses;
using ToDoTestTask.Core.Extensions;
using ToDoTestTask.Core.Models;

namespace ToDoTestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoTaskController : ControllerBase
{
    private readonly IToDoTasksService _tasksService;

    public ToDoTaskController(IToDoTasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        if(await _tasksService.TryCreate(
            request.Title,
            request.Description,
            request.DueDate,
            request.Priority,
            cancellationToken))
            return Ok();

        return BadRequest();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditTask(int id, [FromBody] EditTaskRequest request,
        CancellationToken cancellationToken)
    {
        if(await _tasksService.TryEdit(
               id,
               request.Title,
               request.Description,
               request.DueDate,
               request.Priority,
               request.Status,
               cancellationToken))
            return Ok();

        return BadRequest();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id, CancellationToken cancellationToken)
    {
        ToDoTask? task = await _tasksService
            .Get(id, cancellationToken);

        if (task == null)
            return BadRequest();
        
        ToDoTaskResponse response =
            new ToDoTaskResponse(
                task.Id.GetValueOrDefault(),
                task.Title,
                task.Description,
                task.DueTime.DateToString(),
                task.Priority.ToString(),
                task.Status.ToString(),
                task.Days.GetValueOrDefault(),
                task.Hours.GetValueOrDefault(),
                task.IsRunningOut
            );

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] GetTaskRequest request, CancellationToken cancellationToken)
    {
        List<ToDoTask> tasks = await _tasksService
            .GetAll(request.SortType, cancellationToken);

        if (tasks.IsNullOrEmpty())
            return Ok(new ToDoTasksResponse(new List<ToDoTaskResponse>()));

        List<ToDoTaskResponse> responses = tasks
            .Select(n => new ToDoTaskResponse(
                n.Id.GetValueOrDefault(), 
                n.Title, 
                n.Description, 
                n.DueTime.DateToString(), 
                n.Priority.ToString(), 
                n.Status.ToString(),
                n.Days.GetValueOrDefault(),
                n.Hours.GetValueOrDefault(),
                n.IsRunningOut))
            .ToList();

        return Ok(new ToDoTasksResponse(responses));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id, CancellationToken cancellationToken)
    {
        if (!await _tasksService.TryDelete(id, cancellationToken))
            return BadRequest();

        return Ok();
    }
}