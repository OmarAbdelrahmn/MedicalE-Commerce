namespace Medical_E_Commerce.Contracts.Auth.RefreshTokem;

public record RefreshTokenRequest
(
    string Token,
    string RefreshToken
    );
