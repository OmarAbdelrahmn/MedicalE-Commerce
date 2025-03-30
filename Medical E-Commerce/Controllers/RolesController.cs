using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Consts;
using Medical_E_Commerce.Contracts.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize(Roles = DefaultRoles.Admin)]

public class RolesController(IRoleService roleService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllRoles([FromQuery] bool IncludeDisable)
    {
        var response = await roleService.GetRolesAsync(IncludeDisable);

        return response.IsSuccess ?
            Ok(response.Value) :
            response.ToProblem();

    }

    [HttpPost("")]
    public async Task<IActionResult> addrole(RoleRequest request)
    {
        var response = await roleService.addroleAsync(request);

        return response.IsSuccess ?
            Created() :
            response.ToProblem();

    }

    [HttpPut("{RoleId}")]
    public async Task<IActionResult> Updaterole(string RoleId, RoleRequest request)
    {
        var response = await roleService.UpdateRoleAsync(RoleId, request);

        return response.IsSuccess ?
            NoContent() :
            response.ToProblem();
    }


    [HttpPut("toggle-status/{RoleId}")]
    public async Task<IActionResult> ToggleStatus(string RoleId)
    {
        var response = await roleService.ToggleStatusAsync(RoleId);

        return response.IsSuccess ?
            NoContent() :
            response.ToProblem();

    }
}
