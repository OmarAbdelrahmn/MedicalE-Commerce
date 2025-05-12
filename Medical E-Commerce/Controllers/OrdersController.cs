namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
public class OrdersController(IOrderService service) : ControllerBase
{
    private readonly IOrderService service = service;

    [HttpGet("user")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetOrders()
    {
        var uder = User.GetUserId();
        var orders = await service.GetUserId(uder!);

        return orders.IsSuccess
            ? Ok(orders.Value)
            : orders.ToProblem();
    }

    [HttpGet("pharmacy/{PharmacyId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOrders(int PharmacyId)
    {
        var UserId = User.GetUserId();
        var orders = await service.GetpharmacyId(UserId!, PharmacyId);

        return orders.IsSuccess
            ? Ok(orders.Value)
            : orders.ToProblem();
    }
}
