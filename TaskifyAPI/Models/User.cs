using TaskifyAPI.Enums;

namespace TaskifyAPI.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash  { get; set; }
    public List<TaskItem> TaskItem { get; set; } = new List<TaskItem>();
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime CreateAt { get; set; } = DateTime.Now;
}