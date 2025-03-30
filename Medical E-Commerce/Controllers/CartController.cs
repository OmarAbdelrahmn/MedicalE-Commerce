using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Consts;
using Medical_E_Commerce.Contracts.CartItem;
using Medical_E_Commerce.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
public class CartController(ICartService service) : ControllerBase
{
    private readonly ICartService service = service;

    [HttpPost("")]
    [Authorize(Roles = DefaultRoles.Member)]
    public async Task<IActionResult> CreateCart()
    {
        var UserId = User.GetUserId();

        var response = await service.CreateCart(UserId!);

        return response.IsSuccess ? Created() : response.ToProblem();
    }

    [HttpPost("add-item")]
    [Authorize(Roles = DefaultRoles.Member)]
    public async Task<IActionResult> AddItemCart(AddCartItemToCart Item)
    {
        var UserId = User.GetUserId();

        var response = await service.AddItem(UserId!, Item);

        return response.IsSuccess ? Ok() : response.ToProblem();
    }

    [HttpGet("")]
    [Authorize(Roles = DefaultRoles.Member)]
    public async Task<IActionResult> ShowItems()
    {
        var UserId = User.GetUserId();

        var response = await service.Show(UserId!);

        return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
    }

    [HttpPut("")]
    [Authorize(Roles = DefaultRoles.Member)]
    public async Task<IActionResult> Clear()
    {
        var UserId = User.GetUserId();

        var response = await service.Clear(UserId!);

        return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
    }
}
