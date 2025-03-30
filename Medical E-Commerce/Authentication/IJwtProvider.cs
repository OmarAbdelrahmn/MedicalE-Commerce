namespace Medical_E_Commerce.Authentication;

public interface IJwtProvider
{
    (string Token, int Expiry) GenerateToken(ApplicationUser user, IEnumerable<string> roles);

    string? ValidateToken(string token);
}
