﻿using TaskifyAPI.Dtos;
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
    private readonly IPasswordHasherService _passwordHasherService;

    public UserService(ITokenService tokenService, IPasswordHasherService passwordHasherService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _passwordHasherService = passwordHasherService;
        _userRepository = userRepository;
    }

    public async Task<RegisterViewModel> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        var emailExists = await _userRepository.ExistsByEmailAsync(userRegisterDto.Email);
        if (emailExists)
            throw new EmailAlreadyExistException();
        
        var passwordHash = _passwordHasherService.HashPassword(userRegisterDto.Password);

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
        
        var passwordIsValid = _passwordHasherService.VerifyHashedPassword(userLoginDto.Password, user.PasswordHash);
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

    public async Task<DeleteUserViewModel> UserDeleteAccountAsync(int id)
    {
        var user = await _userRepository.DeleteUserAsync(id);
        var userDelete = new DeleteUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
        };
        return userDelete;
    }
}