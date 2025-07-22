using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Dtos;
using TaskifyAPI.Services.UserService;
using TaskifyAPI.ViewModels;
using TaskifyAPI.ViewModels.Accounts;

namespace TaskifyAPI.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
       _userService = userService;
    }

    [HttpPost("v1/account/register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var userResult = await _userService.RegisterUserAsync(userRegisterDto);

        return Ok(new ResultViewModel<RegisterViewModel>(true, "User Created!", userResult));
    }

    [HttpPost("v1/account/login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var result = await _userService.LoginAsync(userLoginDto);
        
        return Ok(result);
    }
}
