using Medical_E_Commerce.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical_E_Commerce.Persistence.EntitiesConfigrations;

public class RolesConfigrations : IEntityTypeConfiguration<ApplicationRoles>
{
    public void Configure(EntityTypeBuilder<ApplicationRoles> builder)
    {

        builder.HasData(
            [
                new ApplicationRoles
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    IsDefault = false,
                    IsDeleted = false
                },
                new ApplicationRoles
                {
                    Id = DefaultRoles.MemberRoleId,
                    Name = DefaultRoles.Member,
                    ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
                    NormalizedName = DefaultRoles.Member.ToUpper(),
                    IsDefault = true,
                    IsDeleted = false
                }
            ]
        );
    }
}
