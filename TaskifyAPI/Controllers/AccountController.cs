using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Dtos;

namespace TaskifyAPI.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TaskyfyDataContext _context;

    AccountController(TaskyfyDataContext context)
    {
        _context = context;
    }

    [HttpPost("v1/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto userRegisterDto)
    {
        return Ok(await RegisterAsync(userRegisterDto));
    }
}
