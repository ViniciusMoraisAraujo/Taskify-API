using TaskifyAPI.Enums;

namespace TaskifyAPI.ViewModels.TaskItem;

public class UpdateTaskItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public StatusTask Status { get; set; }
}