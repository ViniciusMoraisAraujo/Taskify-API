using TaskifyAPI.Dtos;
using TaskifyAPI.ViewModels.Accounts;

namespace TaskifyAPI.Services.UserService;

public interface IUserService
{
    Task<RegisterViewModel> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<string> LoginAsync(UserLoginDto userLoginDto);
}