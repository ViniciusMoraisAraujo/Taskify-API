namespace TaskifyAPI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? CompleteDate { get; set; }
    public User User { get; set; }
    public TaskStatus Status { get; set; } = Pending;
}