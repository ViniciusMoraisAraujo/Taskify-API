using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyAPI.Models;

namespace TaskifyAPI.Data.Mappings;

public class TaskItemMap : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.CreateAt)
            .HasColumnName("CreateAt")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.CompleteDate)
            .HasColumnName("CompleteDate")
            .HasColumnType("DATETIME");

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasConversion<int>();


        builder
            .HasOne(x => x.User)
            .WithMany(x => x.TaskItem)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_TaskItem_User")
            .OnDelete(DeleteBehavior.Cascade);
    }
}