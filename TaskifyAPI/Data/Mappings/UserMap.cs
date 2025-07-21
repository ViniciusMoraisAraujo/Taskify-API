using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyAPI.Models;

namespace TaskifyAPI.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnName("UserName")
            .HasMaxLength(50)
            .HasColumnType("NVARCHAR(50)");
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasMaxLength(160)
            .HasColumnType("NVARCHAR(160)");

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash");

        builder.Property(x => x.Role)
            .HasColumnName("Role")
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(x => x.CreateAt)
            .HasColumnName("CreateAt")
            .HasColumnType("DATETIME");
        
        builder.HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();
        
        builder
            .HasMany(x => x.TaskItem)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_User_TaskItem")
            .OnDelete(DeleteBehavior.Cascade);
    }
}