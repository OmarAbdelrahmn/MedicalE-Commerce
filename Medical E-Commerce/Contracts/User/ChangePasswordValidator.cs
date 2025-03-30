﻿using Medical_E_Commerce.Abstractions.Consts;

namespace Medical_E_Commerce.Contracts.User;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(i => i.CurrentPassword)
            .NotEmpty();


        RuleFor(i => i.NewPassord)
            .NotEmpty()
            .Matches(RegexPattern.Password)
            .WithMessage("Password should be 8 digits and should contains Lowercase,Uppercase,Number and Special character ")
            .NotEqual(c => c.CurrentPassword)
            .WithMessage("New password can't be same as current one");


    }

}
