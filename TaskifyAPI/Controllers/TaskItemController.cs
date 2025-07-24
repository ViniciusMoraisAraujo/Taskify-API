using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Dtos.TaskItemDtos;

namespace TaskifyAPI.Controllers;

[ApiController]
[Authorize]
public class TaskItemController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateTaskItemDto(CreateTaskItemDto dto)
    {
        
        return Ok(dto);
    }
}