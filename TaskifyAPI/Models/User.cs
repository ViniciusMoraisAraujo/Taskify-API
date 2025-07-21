namespace TaskifyAPI.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash  { get; set; }
    public ICollection<TaskItem> TaskItem { get; set; } = new ICollection<TaskItem>();
    public UserRole Role { get; set; } = User;
    public DateTime CreateAt { get; set; } = DateTime.Now;
}