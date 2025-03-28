using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Admin;

namespace Medical_E_Commerce.Service.Admin;

public interface IAdminService
{
    Task<IEnumerable<UserResponse>> GetAllUsers();
    Task<Result<UserResponse>> GetUserAsync(string Id);
    Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request);
    Task<Result> UpdateUserAsync(string UserId, UpdateUserRequest request);
    Task<Result> ToggleStatusAsync(string UserId);
    Task<Result> EndLockOutAsync(string UserId);
}
