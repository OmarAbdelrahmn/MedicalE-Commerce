namespace Medical_E_Commerce.Entities;

public class ApplicationUser : IdentityUser
{
    public string UserFullName { get; set; } = string.Empty;
    public string UserAddress { get; set; } = string.Empty;
    public Guid? ImageId { get; set; }
    public Image? Image { get; set; } = default!;
    public bool IsDisable { get; set; }
    public List<Pharmacy>? Pharmacy { get; set; } = default!;
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
