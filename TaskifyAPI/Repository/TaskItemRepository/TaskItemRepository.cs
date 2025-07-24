using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Exceptions;
using TaskifyAPI.Models;

namespace TaskifyAPI.Repository.TaskItemRepository;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskyfyDataContext _context;
    
    public TaskItemRepository(TaskyfyDataContext context)
        => _context = context;


    public async Task CreateTaskAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var taskItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        
        if (taskItem == null)
            throw new TaskItemNotFoundException(id);
        
        _context.Tasks.Remove(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskItem>> GetTaskAsync(int userId)
    {
        var taskItems = await _context.Tasks.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();

        return taskItems;
    }

    public async Task<TaskItem> GetTaskByIdAsync(int taskItemId)
    {
        var taskItem = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == taskItemId);
        if (taskItem == null)
            throw new TaskItemNotFoundException(taskItemId);

        return taskItem;
    }

    public async Task<List<TaskItem>> GetTasksAsync(int userId)
    {
        return await _context.Tasks.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    }
}