using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Auth;
using Microsoft.AspNetCore.Identity.Data;

namespace Medical_E_Commerce.Service.Auth;

public interface IAuthService
{
    Task<Result> RegisterAsync(Registerrequest request);
    Task<Result<AuthResponse>> SingInAsync(AuthRequest request);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<Result> ResendEmailAsync(ResendEmailRequest request);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken);
    Task<Result> RevokeRefreshTokenAsync(string Token, string RefreshToken);
}
