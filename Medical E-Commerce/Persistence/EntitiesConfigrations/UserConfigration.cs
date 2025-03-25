using Medical_E_Commerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical_E_Commerce.Persistence.EntitiesConfigrations;

public class UserConfigration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(c => c.UserFullName)
            .HasMaxLength(50)
            .IsRequired();
        

        builder.Property(c => c.UserAddress)
            .HasMaxLength(50)
            .IsRequired();
    }
}
