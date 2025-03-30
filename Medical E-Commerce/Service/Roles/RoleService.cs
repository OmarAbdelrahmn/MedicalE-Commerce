
using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Roles;
using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Roles;

public class RoleService(RoleManager<ApplicationRoles> roleManager, ApplicationDbcontext dbcontext) : IRoleService
{
    private readonly RoleManager<ApplicationRoles> roleManager = roleManager;
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result> addroleAsync(RoleRequest request)
    {
        var roleisexists = await roleManager.RoleExistsAsync(request.Name);

        if (roleisexists)
            return Result.Failure(RolesErrors.ROleIsExcists);


        var role = new ApplicationRoles
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString(),

        };

        var result = await roleManager.CreateAsync(role);

        if (result.Succeeded)
            return Result.Success();


        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result<IEnumerable<RolesResponse>>> GetRolesAsync(bool? IncludeDisable = false)
    {
        var roles = await roleManager.Roles
            .Where(c => !c.IsDeleted || IncludeDisable == true)
            .ProjectToType<RolesResponse>()
            .ToListAsync();

        return Result.Success<IEnumerable<RolesResponse>>(roles);
    }

    public async Task<Result> ToggleStatusAsync(string RollId)
    {
        if (await roleManager.FindByIdAsync(RollId) is not { } role)
            return Result.Failure(RolesErrors.NotFound);

        role.IsDeleted = !role.IsDeleted;

        await roleManager.UpdateAsync(role);

        return Result.Success();
    }

    public async Task<Result> UpdateRoleAsync(string Id, RoleRequest request)
    {
        if (await roleManager.FindByIdAsync(Id) is not { } role)
            return Result.Failure(RolesErrors.NotFound);

        var roleisexists = await roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != Id);

        if (roleisexists)
            return Result.Failure(RolesErrors.DaplicatedRole);

        role.Name = request.Name;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
            return Result.Success();



        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


    }
}
