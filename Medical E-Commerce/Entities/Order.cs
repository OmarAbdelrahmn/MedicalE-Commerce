namespace Medical_E_Commerce.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public IList<int>? ItemsId { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public int PharmacyId { get; set; }

    public ApplicationUser User { get; set; } = default!;
}
