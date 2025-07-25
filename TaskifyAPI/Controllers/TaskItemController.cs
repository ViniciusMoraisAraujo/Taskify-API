using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.Services.TaskItemService;
using TaskifyAPI.ViewModels;
using TaskifyAPI.ViewModels.TaskItem;

namespace TaskifyAPI.Controllers;

[ApiController]
[Authorize]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }
    
    [HttpPost("v1/tasks/create")]
    public async Task<IActionResult> CreateTaskItemAsync(CreateTaskItemDto dto)
    {
        var result = await _taskItemService.CreateTaskItemAsync(dto);
        return Ok(new ResultViewModel<CreateTaskItemViewModel>(true, "taskItem created ",result ));
    }

    [HttpGet("v1/tasks/get")]
    public async Task<IActionResult> GetTaskItemAsync()
    {
        var taskItem = await _taskItemService.GetTaskItemAsync();
        return Ok(new ResultViewModel<List<TaskItemViewModel>>(true, "taskItem retrieved", taskItem));
    }

    [HttpPut("v1/tasks/update/{id}")]
    public async Task<IActionResult> UpdateTaskItemAsync([FromRoute]int id,  UpdateTaskItemDto dto)
    {
        await _taskItemService.UpdateTaskItemAsync(id, dto);
        return Ok(new ResultViewModel<UpdateTaskItemViewModel>(true, "taskItem updated"));
    }

    [HttpDelete("v1/tasks/delete/{id}")]
    public async Task<IActionResult> DeleteTaskItemAsync([FromRoute] int id)
    {
        await  _taskItemService.DeleteTaskItemAsync(id);
        return Ok(new ResultViewModel<string?>(true, "Task item deleted", null));
    }
}