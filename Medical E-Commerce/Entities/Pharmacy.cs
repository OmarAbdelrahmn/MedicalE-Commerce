using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Entities;

public class Pharmacy
{
    public int Id { get; set; }

    [Length(3, 50)]
    public string Name { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public string PhoneNumbers { get; set; } = string.Empty;
    public string WhatsUrl { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string MapsLocation { get; set; } = string.Empty;
    public string WorkTime { get; set; } = " 24 hours ";
    public ICollection<Item> Items { get; set; } = [];
    public string? AdminId { get; set; } = string.Empty;
    public ApplicationUser? Admin { get; set; } = default!;
}
