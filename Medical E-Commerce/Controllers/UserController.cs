using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Files;
using Medical_E_Commerce.Contracts.User;
using Medical_E_Commerce.Extensions;
using Medical_E_Commerce.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("me")]
[ApiController]
[Authorize]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService service = service;

    [HttpGet("")]
    public async Task<IActionResult> ShowUserProfile()
    {
        var result = await service.GetUserProfile(User.GetUserId()!);

        return Ok(result.Value);
    }

    [HttpPut("info")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequest request)
    {
        var result = await service.UpdateUserProfile(User.GetUserId()!, request);

        return NoContent();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await service.ChangePassword(User.GetUserId()!, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImagesAsync([FromForm] UpdoadImagessRequest request)
    {
        await service.UpoadImage(User.GetUserId()!, request.Image);

        return Created();
    }


    [HttpGet("image-stream")]
    public async Task<IActionResult> stream()
    {
        var (filestream, contenttype, filename) = await service.FileStream(User.GetUserId()!);

        return filestream is null ?
            NoContent() :
            File(filestream, contenttype, filename, enableRangeProcessing: true);
    }
}
