﻿namespace Medical_E_Commerce.Contracts.Auth;

public record ConfirmEmailRequest
(
    string UserId,
    string Code
);
