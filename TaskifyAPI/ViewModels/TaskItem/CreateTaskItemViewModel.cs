using TaskifyAPI.Enums;
using TaskifyAPI.Models;

namespace TaskifyAPI.ViewModels.TaskItem;

public class CreateTaskItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string userName { get; set; }
    public StatusTask StatusTask { get; set; }
}