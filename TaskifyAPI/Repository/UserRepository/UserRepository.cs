using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Exceptions;
using TaskifyAPI.Models;
using TaskifyAPI.ViewModels.Accounts;

namespace TaskifyAPI.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly TaskyfyDataContext _context;

    public UserRepository(TaskyfyDataContext context)
    {
        _context = context;
    }


    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            throw new UserNotFoundException(id);
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}