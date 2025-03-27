using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Auth;
using Microsoft.AspNetCore.Identity.Data;

namespace Medical_E_Commerce.Service.Auth;

public interface IAuthService
{
    Task<Result> RegisterAsync(Registerrequest request);

}
