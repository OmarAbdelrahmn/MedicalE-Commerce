﻿using FluentValidation;

namespace Medical_E_Commerce.Contracts.User;

public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileValidator()
    {

        RuleFor(i => i.UserAddress)
             .NotEmpty()
             .Length(3, 100);


        RuleFor(i => i.UserFullName)
            .NotEmpty()
            .Length(3, 100);

    }

}
