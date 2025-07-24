using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.Models;
using TaskifyAPI.Repository.TaskItemRepository;
using TaskifyAPI.Repository.UserRepository;
using TaskifyAPI.ViewModels.TaskItem;

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
        var userClaims = _httpContextAccessor.HttpContext?.User;
        var userIdClaim = userClaims?.FindFirst("user_id")?.Value;
        
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User not authenticate");
        
        var userId = int.Parse(userIdClaim);
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
}