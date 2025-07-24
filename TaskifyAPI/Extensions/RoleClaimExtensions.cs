using System.Security.Claims;
using TaskifyAPI.Models;

namespace TaskifyAPI.Extensions;

public static class RoleClaimExtensions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var claims = new List<Claim>{        
                new Claim("user_id", user.Id.ToString()),
                new Claim("user_email", user.Email),
                new Claim("user_name", user.UserName),
            };
        claims.AddRange(user.Role.Select(role => new Claim("user_role", role.Name)));
        
        return claims;
    }
}