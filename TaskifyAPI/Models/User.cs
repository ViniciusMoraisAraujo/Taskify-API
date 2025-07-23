using TaskifyAPI.Enums;

namespace TaskifyAPI.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash  { get; set; }
    public List<TaskItem> TaskItem { get; set; } = new List<TaskItem>();
    public IList<Role> Role { get; set; } 
    public DateTime CreateAt { get; set; } = DateTime.Now;
}