namespace Medical_E_Commerce.Contracts.Item;

public record ItemDetailsResponse
(
    int Id,
    string Name,
    string Type,
    string ImageURL,
    int Price,
    string? EffectiveSubstance,
    int Count,
    string? Brand,
    int PharmacyId
);
