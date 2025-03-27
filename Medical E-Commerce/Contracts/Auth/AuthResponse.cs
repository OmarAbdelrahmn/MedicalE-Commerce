﻿namespace Medical_E_Commerce.Contracts.Auth;

public record AuthResponse
(
    string Id,
    string Email,
    string UserFullName,
    string UserAdress,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshExpiresIn
    );
