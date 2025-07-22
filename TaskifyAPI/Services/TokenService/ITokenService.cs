using TaskifyAPI.Models;

namespace TaskifyAPI.Services.TokenService;

public interface ITokenService
{
    string GenerateToken(User user);
}