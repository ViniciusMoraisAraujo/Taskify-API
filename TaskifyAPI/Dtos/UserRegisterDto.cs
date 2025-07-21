namespace TaskifyAPI.Dtos;

public class UserRegisterDto
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}