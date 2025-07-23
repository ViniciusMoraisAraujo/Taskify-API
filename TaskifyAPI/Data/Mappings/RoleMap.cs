using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyAPI.Models;

namespace TaskifyAPI.Data.Mappings;

public class RoleMap : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasMany(x => x.Users);
    }
}