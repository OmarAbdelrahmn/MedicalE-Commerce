namespace Medical_E_Commerce.Entities;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ICollection<CartItem>? Items { get; set; } = [];
    public ApplicationUser User { get; set; } = default!;
}
