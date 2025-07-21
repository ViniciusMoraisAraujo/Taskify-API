using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data.Mappings;
using TaskifyAPI.Models;

namespace TaskifyAPI.Data;

public class TaskyfyDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    public TaskyfyDataContext(DbContextOptions<TaskyfyDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TaskItemMap());
    }
}