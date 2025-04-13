namespace Medical_E_Commerce.Entities;

public class Fav
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = default!;

}
