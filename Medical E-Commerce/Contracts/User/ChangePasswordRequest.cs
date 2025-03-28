namespace Medical_E_Commerce.Contracts.User;

public record ChangePasswordRequest
(
    string CurrentPassword,
    string NewPassord
    );
