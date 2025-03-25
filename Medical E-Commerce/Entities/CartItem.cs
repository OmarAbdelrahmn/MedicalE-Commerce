namespace Medical_E_Commerce.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int Count{ get; set; }
    public int ItemId { get; set; }
    public int CartId { get; set; }
    public Item Item { get; set; } = default!;
    public Cart Cart { get; set; } = default!;
}
