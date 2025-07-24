namespace TaskifyAPI.Exceptions;

public class TaskItemNotFoundException : Exception
{
    public TaskItemNotFoundException(int id) : base($"TaskItem {id} not found"){}
}