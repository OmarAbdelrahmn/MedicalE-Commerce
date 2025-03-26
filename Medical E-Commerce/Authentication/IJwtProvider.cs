using Medical_E_Commerce.Entities;

namespace Medical_E_Commerce.Authentication;

public interface IJwtProvider
{
    (string Token, int Expiry) GenerateToken(ApplicationUser user, IEnumerable<string> Roles);

    string? ValidateToken(string token);
}
