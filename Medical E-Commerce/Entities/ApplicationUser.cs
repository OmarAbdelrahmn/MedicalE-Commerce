using Microsoft.AspNetCore.Identity;

namespace Medical_E_Commerce.Entities;

public class ApplicationUser : IdentityUser
{
    public string UserFullName { get; set; } = string.Empty;
    public string UserAddress { get; set; } = string.Empty;
}
