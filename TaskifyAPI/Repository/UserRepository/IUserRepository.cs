using TaskifyAPI.Models;
using TaskifyAPI.ViewModels.Accounts;

namespace TaskifyAPI.Repository.UserRepository;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task CreateUserAsync(User user);
    Task<User> DeleteUserAsync(int id);
    Task <User?> GetByEmailAsync(string email); 
    Task UpdateUserAsync(User user);
}