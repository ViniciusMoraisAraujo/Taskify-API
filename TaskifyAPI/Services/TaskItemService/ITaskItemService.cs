using TaskifyAPI.Dtos.TaskItemDtos;
using TaskifyAPI.Models;
using TaskifyAPI.ViewModels.TaskItem;

namespace TaskifyAPI.Services.TaskItemService;

public interface ITaskItemService
{
    Task<CreateTaskItemViewModel> CreateTaskItemAsync(CreateTaskItemDto createTaskItemDto);
    Task<List<TaskItemViewModel>> GetTaskItemAsync();
    Task<UpdateTaskItemViewModel> UpdateTaskItemAsync(int id, UpdateTaskItemDto updateTaskItemDto);
}