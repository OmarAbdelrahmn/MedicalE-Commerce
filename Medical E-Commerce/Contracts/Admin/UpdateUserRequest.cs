namespace Medical_E_Commerce.Contracts.Admin;

public record UpdateUserRequest
(
    string Email,
    string UserFullName,
    string UserAddress,
    IList<string> Roles
    );
