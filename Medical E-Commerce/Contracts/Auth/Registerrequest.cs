﻿namespace Medical_E_Commerce.Contracts.Auth;

public record Registerrequest
(
    string Email,
    string Password,
    string UserFullName,
    string UserAdress
    );
