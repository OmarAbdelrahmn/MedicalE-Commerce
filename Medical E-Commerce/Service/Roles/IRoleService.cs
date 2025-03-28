using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Roles;

namespace Medical_E_Commerce.Services.Roles;

public interface IRoleService
{
    Task<Result<IEnumerable<RolesResponse>>> GetRolesAsync(bool? IncludeDisable = false);
    Task<Result> ToggleStatusAsync(string RollId);
    Task<Result<RoleDetailsResponse>> addroleAsync(RoleRequest request);
    Task<Result> UpdateRoleAsync(string Id, RoleRequest request);
}
