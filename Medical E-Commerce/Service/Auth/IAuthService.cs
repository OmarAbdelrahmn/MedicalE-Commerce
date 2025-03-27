using Medical_E_Commerce.Abstractions;
using Microsoft.AspNetCore.Identity.Data;

namespace Medical_E_Commerce.Service.Auth;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterRequest request);

}
