using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Contracts.Item;

public record ItemResponse
(
    int Id ,
    string Name ,
    string Type ,
    string ImageURL,
    int Price ,
    string? EffectiveSubstance ,
    int Count ,
    string? Brand
    );
