using Medical_E_Commerce.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace Medical_E_Commerce.Service.Auth;

public class AuthService(
    UserManager<ApplicationUser> manager,
    ILogger<AuthService> logger,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender,
    SignInManager<ApplicationUser> signInManager,
    ApplicationDbcontext dbContext,
    IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> manager = manager;
    private readonly SignInManager<ApplicationUser> signInManager = signInManager;
    private readonly ApplicationDbcontext context = dbContext;
    private readonly IJwtProvider jwtProvider = jwtProvider;
    private readonly int RefreshTokenExpiryDays = 60;

    public async Task<Result> RegisterAsync(Registerrequest request)
    {
        var emailIsEx = await manager.Users.AnyAsync(c => c.Email == request.Email);

        if (emailIsEx)
            return Result.Failure(UserErrors.UserAlreayExists);

        var user = request.Adapt<ApplicationUser>();

        user.UserName = user.Email;

        var result = await manager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var code = await manager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            logger.LogInformation("Configration code : {code}", code);

            await sendemail(user, code);

            await manager.AddToRoleAsync(user, DefaultRoles.Member);

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

    public async Task<Result<AuthResponse>> SingInAsync(AuthRequest request)
    {

        if (await manager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        if (user.IsDisable)
            return Result.Failure<AuthResponse>(UserErrors.Disableuser);

        var result = await signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.Succeeded)
        {
            var userRoles = await manager.GetRolesAsync(user);

            var (Token, ExpiresIn) = jwtProvider.GenerateToken(user, userRoles);

            var RefreshToken = GenerateRefreshToken();

            var RefreshExpiresIn = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);


            user.RefreshTokens.Add(new RefreshToken
            {
                Token = RefreshToken,
                ExpiresOn = RefreshExpiresIn,

            });

            await manager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id,
                user.Email!,
                user.UserFullName,
                user.UserAddress,
                Token,
                ExpiresIn * 60,
                RefreshToken,
                RefreshExpiresIn
            );

            return Result.Success(response);
        }

        var error = result.IsNotAllowed ?
             UserErrors.EmailNotConfirmed :
             result.IsLockedOut ?
             UserErrors.userLockedout :
             UserErrors.InvalidCredentials;


        return Result.Failure<AuthResponse>(error);

    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(240));
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken)
    {
        var UserId = jwtProvider.ValidateToken(Token);

        if (UserId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var user = await manager.FindByIdAsync(UserId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        if (user.IsDisable)
            return Result.Failure<AuthResponse>(UserErrors.Disableuser);

        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(UserErrors.userLockedout);


        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

        if (UserRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        UserRefreshToken.RevokedOn = DateTime.UtcNow;

        var userRoles = await manager.GetRolesAsync(user);

        var (newToken, ExpiresIn) = jwtProvider.GenerateToken(user, userRoles);

        var newRefreshToken = GenerateRefreshToken();

        var RefreshExpiresIn = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);


        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = RefreshExpiresIn,

        });

        await manager.UpdateAsync(user);

        var response = new AuthResponse(
            user.Id,
            user.Email!,
            user.UserFullName,
            user.UserAddress,
            newToken,
            ExpiresIn * 60,
            newRefreshToken,
            RefreshExpiresIn
        );

        return Result.Success(response);
    }

    public async Task<Result> RevokeRefreshTokenAsync(string Token, string RefreshToken)
    {
        var UserId = jwtProvider.ValidateToken(Token);

        if (UserId is null)
            return Result.Failure(UserErrors.InvalidCredentials);

        var user = await manager.FindByIdAsync(UserId);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

        if (UserRefreshToken is null)
            return Result.Failure(UserErrors.InvalidCredentials);

        UserRefreshToken.RevokedOn = DateTime.UtcNow;

        await manager.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {

        if (await manager.FindByIdAsync(request.UserId) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);


        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfermation);

        var code = request.Code;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {

            return Result.Failure(UserErrors.InvalidCredentials);
        }


        var result = await manager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        var errors = result.Errors.First();
        return Result.Failure(new Error(errors.Code, errors.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> ResendEmailAsync(ResendEmailRequest request)
    {
        if (await manager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success();


        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfermation);

        var code = await manager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        logger.LogInformation("Configration code : {code}", code);

        //send email
        await sendemail(user, code);

        return Result.Success();
    }

    private async Task sendchangepasswordemail(ApplicationUser user, string code)
    {
        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailbody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            new Dictionary<string, string> {
                    { "{{name}}", user.UserFullName } ,
                    { "{{action_url}}", $"{origin}/auth/forgetpassword?email={user.Email}&code={code}" }

            });

        BackgroundJob.Enqueue(() => emailSender.SendEmailAsync(user.Email!, "Survay basket : change password", emailbody));
        await Task.CompletedTask;
    }

    public async Task<Result> ForgetPassordAsync(ForgetPasswordRequest request)
    {
        if (await manager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        var code = await manager.GeneratePasswordResetTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        logger.LogInformation("Reset code : {code}", code);

        //send email
        await sendchangepasswordemail(user, code);

        return Result.Success();

    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await manager.FindByEmailAsync(request.Email);

        if (user == null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCredentials);

        IdentityResult identityResult;

        try
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

            identityResult = await manager.ResetPasswordAsync(user, code, request.Password);

        }
        catch (FormatException)
        {

            identityResult = IdentityResult.Failed(manager.ErrorDescriber.InvalidToken());
        }

        if (identityResult.Succeeded)
            return Result.Success();

        var error = identityResult.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
    }

}
