namespace Medical_E_Commerce.Contracts.Admin;

public record UserResponse
(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsDisable,
    IEnumerable<string> Roles
    );
