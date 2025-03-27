using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Contracts.Auth;

public record ForgetPasswordRequest
([EmailAddress]
    [Required]
    string Email);
