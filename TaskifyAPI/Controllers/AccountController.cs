using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Dtos;
using TaskifyAPI.Models;
using TaskifyAPI.Services.Interfaces;

namespace TaskifyAPI.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TaskyfyDataContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public AccountController(TaskyfyDataContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("v1/register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var hashedPassword = _passwordHasher.HashPassword(userRegisterDto.Password);
        var user = new User
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            PasswordHash = hashedPassword
        };
        bool verification = _context.Users.Any(u => u.Email == userRegisterDto.Email);
        if (verification)
            return BadRequest("Email already exists");
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(userRegisterDto);
    }

    [HttpPost("v1/login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
        if (user == null)
            return Unauthorized();
        
        var hashedPassword = _passwordHasher.VerifyHashedPassword(userLoginDto.PasswordHash, user.PasswordHash);
        if (!hashedPassword)
            return Unauthorized();
        
        return Ok(user);
    }

    [HttpGet("v1/get")]
    public async Task<IActionResult> Get()
    {
        var list = _context.Users.AsNoTracking().ToList();
        return Ok(list);
    }
}
