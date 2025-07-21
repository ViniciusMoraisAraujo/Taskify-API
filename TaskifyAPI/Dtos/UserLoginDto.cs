namespace TaskifyAPI.Dtos;

public class UserLoginDto
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}