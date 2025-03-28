namespace Medical_E_Commerce.Contracts.User;

public record UpdateUserProfileRequest
(
    string UserFullName,
    string UserAddress
    );
