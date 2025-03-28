using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Contracts.Roles;

public record RoleRequest
(
    [Length(3,20)]
    [Required]
    string Name
    );
