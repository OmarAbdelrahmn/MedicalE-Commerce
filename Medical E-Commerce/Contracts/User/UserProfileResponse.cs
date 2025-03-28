namespace Medical_E_Commerce.Contracts.User;

public record UserProfileResponse
(
    string Email,
    string UserFullName,
    string UserAddress
    );
