using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Entities;

public class Item
{
    public int Id { get; set; }
    [Length(3, 50)]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public int Price { get; set; }
    public string? EffectiveSubstance { get; set; } = string.Empty;
    public int Count { get; set; }
    public string? Brand { get; set; } = string.Empty;

    public int PharmacyId { get; set; }
    public Pharmacy Pharmacy { get; set; } = default!;
}
