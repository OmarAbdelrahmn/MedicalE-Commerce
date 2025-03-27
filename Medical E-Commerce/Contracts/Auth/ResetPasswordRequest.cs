namespace Medical_E_Commerce.Contracts.Auth;

public record ResetPasswordRequest
(
    string Email,
    string Code,
    string Password
    );
