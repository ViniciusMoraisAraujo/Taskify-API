using TaskifyAPI.Models;

namespace TaskifyAPI.Repository.UserRepository;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task CreateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
    Task <User?> GetByEmailAsync(string email);
    Task UpdateUserAsync(User user);
}