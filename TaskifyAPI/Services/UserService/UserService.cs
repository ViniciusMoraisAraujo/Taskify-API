using TaskifyAPI.Dtos;
using TaskifyAPI.Exceptions;
using TaskifyAPI.Models;
using TaskifyAPI.Repository.UserRepository;
using TaskifyAPI.Services.PasswordHasher;
using TaskifyAPI.Services.TokenService;
using TaskifyAPI.ViewModels.Accounts;

namespace TaskifyAPI.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(ITokenService tokenService, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<RegisterViewModel> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        var verification = await _userRepository.ExistsByEmailAsync(userRegisterDto.Email);
        if (verification)
            throw new EmailAlreadyExistException();
        
        var passwordHash = _passwordHasher.HashPassword(userRegisterDto.Password);

        var user = new User
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            PasswordHash = passwordHash
        };
        await _userRepository.CreateUserAsync(user);
        var result = new RegisterViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
        };
        return result;
    }

    public async Task<LoginViewModel> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetByEmailAsync(userLoginDto.Email);
        if (user == null)
            throw new Exception("User not found");
        
        var passwordIsValid = _passwordHasher.VerifyHashedPassword(userLoginDto.Password, user.PasswordHash);
        if (!passwordIsValid)
            throw new Exception("Password is incorrect");
        
        var token = _tokenService.GenerateToken(user);

        var result = new LoginViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Token = token
        };
        return result;
    }
}