namespace Medical_E_Commerce.Contracts.Auth;

public class ResendEmailRequestValidator : AbstractValidator<ResendEmailRequest>
{
    public ResendEmailRequestValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty();
    }
}
