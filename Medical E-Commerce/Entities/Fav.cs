namespace Medical_E_Commerce.Entities;

public class Fav
{
    public int ItemId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public Item Item { get; set; } = default!;

    public ApplicationUser User { get; set; } = default!;

}
