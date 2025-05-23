﻿using Medical_E_Commerce.Contracts.Auth.RefreshTokem;

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
            //Ok("{\"massege\" : \"We have sent you an Email , Please confirm the Email to continue\"}") :
            Ok(new Resu("We have sent you an Email , Please confirm the Email if you want")) :
            response.ToProblem();
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] AuthRequest request)
    {
        var response = await service.SingInAsync(request);

        return response.IsSuccess ?
            Ok(response.Value) :
            response.ToProblem();
    }


    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request)
    {
        var response = await service.ConfirmEmailAsync(request);

        return response.IsSuccess ?
            Ok(new Resu("Email confirmed successfully")) :
            response.ToProblem();
    }


    [HttpPost("resend-configration-email")]
    public async Task<IActionResult> ResendConfirmEmailAsync([FromBody] ResendEmailRequest request)
    {
        var response = await service.ResendEmailAsync(request);

        return response.IsSuccess ?
            Ok(new Resu("We resend your confirmation Email")) :
            response.ToProblem();
    }



    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.GetRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ?
            Ok(response.Value) :
            response.ToProblem();
    }



    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await service.RevokeRefreshTokenAsync(request.Token, request.RefreshToken);

        return response.IsSuccess ?
            Ok(new Resu("Token revoked successfully")) :
            response.ToProblem();
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {
        var response = await service.ForgetPassordAsync(request);

        return response.IsSuccess ?
                Ok(new Resu("we sent forget password email")) :
                response.ToProblem();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] Contracts.Auth.ResetPasswordRequest request)
    {
        var response = await service.ResetPasswordAsync(request);

        return response.IsSuccess ?
                Ok(new Resu("Your Password Has been reset successfully")) :
                response.ToProblem();
    }
}



public class Resu(string massage)
{
    public string Massage { get; set; } = massage;
}