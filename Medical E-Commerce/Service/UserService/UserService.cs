using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.User;
using Medical_E_Commerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.UserService;

public class UserService(UserManager<ApplicationUser> manager) : IUserService
{
    public async Task<Result> ChangePassword(string id, ChangePasswordRequest request)
    {
        var user = await manager.FindByIdAsync(id);

        var result = await manager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassord);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result<UserProfileResponse>> GetUserProfile(string id)
    {
        var user = await manager.Users
            .Where(i => i.Id == id)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();
        ;

        return Result.Success(user);
    }

    public async Task<Result> UpdateUserProfile(string id, UpdateUserProfileRequest request)
    {
        //var user = await manager.FindByIdAsync(id);

        //user = request.Adapt(user);

        //await manager.UpdateAsync(user!);
        await manager.Users
            .Where(i => i.Id == id)
            .ExecuteUpdateAsync(set =>
            set.SetProperty(x => x.UserAddress, request.UserAddress)
               .SetProperty(x => x.UserFullName, request.UserFullName));

        return Result.Success();
    }
}
