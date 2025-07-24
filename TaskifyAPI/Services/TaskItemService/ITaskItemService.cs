using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.ViewModels.TaskItem;

namespace TaskifyAPI.Services.TaskItemService;

public interface ITaskItemService
{
    Task<CreateTaskItemViewModel> CreateTaskItemAsync(CreateTaskItemDto createTaskItemDto);
}