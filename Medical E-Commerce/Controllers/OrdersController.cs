using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Service.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
public class OrdersController(IOrderService service) : ControllerBase
{
    private readonly IOrderService service = service;

    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetOrders(string UserId)
    {
       var orders = await service.GetUserId(UserId);
       
        return orders.IsSuccess
            ? Ok(orders.Value)
            : orders.ToProblem();
    }
    
    [HttpGet("pharmacy/{PharmacyId}")]
    public async Task<IActionResult> GetOrders(int PharmacyId)
    {
       var orders = await service.GetpharmacyId(PharmacyId);
       
        return orders.IsSuccess
            ? Ok(orders.Value)
            : orders.ToProblem();
    }
}
