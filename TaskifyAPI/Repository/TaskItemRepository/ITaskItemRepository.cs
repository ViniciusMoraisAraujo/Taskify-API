using TaskifyAPI.Models;

namespace TaskifyAPI.Repository.TaskItemRepository;

public interface ITaskItemRepository
{
    Task CreateTaskAsync(TaskItem task);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int id);
    Task<List<TaskItem>> GetTaskAsync(int userId);
    
    Task<TaskItem> GetTaskByIdAsync( int taskItemId);
    Task<List<TaskItem>> GetTasksAsync(int userId);
}