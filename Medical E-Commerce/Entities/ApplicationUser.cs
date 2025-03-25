using Microsoft.AspNetCore.Identity;

namespace Medical_E_Commerce.Entities;

public class ApplicationUser : IdentityUser
{
    public string UserFullName { get; set; } = string.Empty;
    public string UserAddress { get; set; } = string.Empty;
    public int? ImageId { get; set; }
    public Image? Image { get; set; } = default!;
}
