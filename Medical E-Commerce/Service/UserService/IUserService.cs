using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.User;

namespace Medical_E_Commerce.Service.UserService;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetUserProfile(string id);
    Task<Result> UpdateUserProfile(string id, UpdateUserProfileRequest request);
    Task<Result> ChangePassword(string id, ChangePasswordRequest request);
}
