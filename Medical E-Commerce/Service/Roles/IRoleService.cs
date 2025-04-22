using Medical_E_Commerce.Contracts.Roles;

namespace Medical_E_Commerce.Service.Roles;

public interface IRoleService
{
    Task<Result<IEnumerable<RolesResponse>>> GetRolesAsync(bool? IncludeDisable = false);
    Task<Result> ToggleStatusAsync(string RollId);
    Task<Result> addroleAsync(RoleRequest request);
    Task<Result> UpdateRoleAsync(string Id, RoleRequest request);
}
