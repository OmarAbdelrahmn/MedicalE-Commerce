using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Extensions;
using Medical_E_Commerce.Service.Fav;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Member,Admin")]
public class FavController(IFavService service) : ControllerBase
{
    private readonly IFavService service = service;

    [HttpPost("{ItmeId}")]
    public async Task<IActionResult> AddItem(int ItmeId)
    {
        var userid = User.GetUserId();

        var result = await service.AddItem(userid!, ItmeId);
        return result.IsSuccess
            ? Ok()
            : result.ToProblem();
    }
    
    [HttpDelete("")]
    public async Task<IActionResult> Clear()
    {
        var userid = User.GetUserId();

        var result = await service.Clear(userid!);
        return result.IsSuccess
            ? Ok()
            : result.ToProblem();
    }


    [HttpGet("")]
    public async Task<IActionResult> GetItem()
    {
        var userid = User.GetUserId();

        var result = await service.Show(userid!);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


}
