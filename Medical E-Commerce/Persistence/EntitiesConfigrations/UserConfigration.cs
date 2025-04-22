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

        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, "P@ssword1234"),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            UserFullName = "CareCapsole-Admin",
            UserAddress = "lives in CareCapsole"
        });
    }
}
