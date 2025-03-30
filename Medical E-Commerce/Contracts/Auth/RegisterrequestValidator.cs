using Medical_E_Commerce.Abstractions.Consts;

namespace Medical_E_Commerce.Contracts.Auth;

public class RegisterrequestValidator : AbstractValidator<Registerrequest>
{

    public RegisterrequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Matches(RegexPattern.Password)
            .WithMessage("Password should be 8 digits and should contains Lowercase,Uppercase,Number and Special character ");

        RuleFor(x => x.UserFullName)
            .NotEmpty()
            .WithMessage("LastName is required")
            .Length(3, 100);


        RuleFor(x => x.UserAdress)
            .NotEmpty()
            .WithMessage("LastName is required")
            .Length(3, 100);
    }

}
