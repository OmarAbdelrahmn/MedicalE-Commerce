﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medical_E_Commerce.Persistence.EntitiesConfigrations;

public class UserRolesConfigrations : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = DefaultUsers.AdminId,
                RoleId = DefaultRoles.AdminRoleId
            }
         );
    }
}
