using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data.Mappings;
using TaskifyAPI.Migrations;
using TaskifyAPI.Models;

namespace TaskifyAPI.Data;

public class TaskyfyDataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Role> Roles { get; set; }


    public TaskyfyDataContext(DbContextOptions<TaskyfyDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TaskItemMap());
        
        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Name = "User"},
            new Role() { Id = 2, Name = "Admin"}
            );
    }
}