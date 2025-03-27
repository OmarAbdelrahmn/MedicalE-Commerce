using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Medical_E_Commerce.Service.Auth;

public class AuthService(
    UserManager<ApplicationUser> manager,
    ILogger<AuthService> logger,
    IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly UserManager<ApplicationUser> manager = manager;

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        var emailIsEx = await manager.Users.AnyAsync(c=>c.Email == request.Email);

        if(emailIsEx)
            return Result.Failure(UserErrors.UserAlreayExists);

        var user = request.Adapt<ApplicationUser>();
        
        var result = await manager.CreateAsync(user,request.Password);

        if (result.Succeeded)
        {
            var code = await manager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            logger.LogInformation("Configration code : {code}", code);

            await sendemail(user, code);

            return Result.Success();
        }
        var errors = result.Errors.First();
        return Result.Failure(new Error(errors.Code, errors.Description, StatusCodes.Status400BadRequest));
    }

    private async Task sendemail(ApplicationUser user, string code)
    {
        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailbody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string> {
                    { "{{name}}", user.UserFullName } ,
                    { "{{action_url}}", $"{origin}/auth/emailconfigration?userid={user.Id}&code={code}" }

            });

        BackgroundJob.Enqueue(() => emailSender.SendEmailAsync(user.Email!, "Survay basket : Email configration", emailbody));
        await Task.CompletedTask;
    }
}
