using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.Services.TaskItemService;
using TaskifyAPI.ViewModels;
using TaskifyAPI.ViewModels.TaskItem;

namespace TaskifyAPI.Controllers;

[ApiController]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }
    
    [HttpPost("v1/tasks/create")]
    [Authorize]
    public async Task<IActionResult> CreateTaskItemAsync(CreateTaskItemDto dto)
    {
        var result = await _taskItemService.CreateTaskItemAsync(dto);
        return Ok(new ResultViewModel<CreateTaskItemViewModel>(true, "taskItem created ",result ));
    }

    [HttpGet("v1/tasks/get")]
    [Authorize]
    public async Task<IActionResult> GetTaskItemAsync()
    {
        var taskItem = await _taskItemService.GetTaskItemAsync();
        return Ok(new ResultViewModel<List<TaskItemViewModel>>(true, "taskItem retrieved", taskItem));
    }

    [HttpPut("v1/tasks/update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateTaskItemAsync([FromRoute]int id)
    {
        var taskItem = await _taskItemService.GetTaskItemAsync();
    }
}