﻿using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Authentication;

public class JwtOptions
{
    public static string SectionName = "Jwt";

    [Required]
    public string Issuer { get; init; } = string.Empty;


    [Required]
    public string Audience { get; init; } = string.Empty;


    [Required]
    public string Key { get; init; } = string.Empty;


    [Required]
    [Range(1, int.MaxValue)]
    public int ExpiryIn { get; init; }
}
