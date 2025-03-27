using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Auth;
using Medical_E_Commerce.Service.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Registerrequest request)
    {
        var response = await service.RegisterAsync(request);

        return response.IsSuccess ?
            Ok() :
            response.ToProblem();
    }
}
