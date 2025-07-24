using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.Models;
using TaskifyAPI.Repository.TaskItemRepository;
using TaskifyAPI.Repository.UserRepository;
using TaskifyAPI.ViewModels.TaskItem;
using System.Linq;
using TaskifyAPI.Enums;

namespace TaskifyAPI.Services.TaskItemService;

public class TaskItemService :  ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TaskItemService(ITaskItemRepository taskItemRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _taskItemRepository = taskItemRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<CreateTaskItemViewModel> CreateTaskItemAsync(CreateTaskItemDto createTaskItemDto)
    {
        var userId = GetUserIdFromClaims();
        var user = await _userRepository.GetUserByIdAsync(userId);
        
        var taskItem = new TaskItem
        {
            Title = createTaskItemDto.Title,
            UserId = userId,
        };
        await _taskItemRepository.CreateTaskAsync(taskItem);

        var result = new CreateTaskItemViewModel
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            userName = user.UserName,
            Status = taskItem.Status,
        };
        return result;
    }

    public async Task<List<TaskItemViewModel>> GetTaskItemAsync()
    {
        var userId = GetUserIdFromClaims();
        var task = await _taskItemRepository.GetTaskAsync(userId);

        return task.Select(t => new TaskItemViewModel
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
        }).ToList();
    }

    public async Task<UpdateTaskItemViewModel> UpdateTaskItemAsync(int id,  UpdateTaskItemDto updateTaskItemDto)
    {
        var task = await _taskItemRepository.GetTaskByIdAsync(id);
        if (task == null)
            throw new Exception("Task not found");
        
        task.Status = (StatusTask)updateTaskItemDto.Status;
        
        await _taskItemRepository.UpdateTaskAsync(task);

        var result = new UpdateTaskItemViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
        };
        return result;
    }

    private int GetUserIdFromClaims()
    {
        var userClaims = _httpContextAccessor.HttpContext?.User;
        var userIdClaim = userClaims?.FindFirst("user_id")?.Value;
        
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User not authenticate");
        
        return int.Parse(userIdClaim);
    }
}