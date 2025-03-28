namespace Medical_E_Commerce.Contracts.Admin;

public record CreateUserRequest
(
    string Email,
    string Password,
    string UserFullName,
    string UserAddress,
    IList<string> Roles
    );
