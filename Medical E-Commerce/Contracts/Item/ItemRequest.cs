﻿namespace Medical_E_Commerce.Contracts.Item;

public record ItemRequest
(
    string Name,
    string Type,
    string ImageURL,
    int Price,
    string? EffectiveSubstance,
    int Count,
    string? Brand
    );
