using FluentValidation;

namespace Medical_E_Commerce.Contracts.Pharmacy;

public class PharmacyRequestValidator : AbstractValidator<PharmacyRequest>
{
    public PharmacyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.ImageURL)
            .NotEmpty()
            .WithMessage("ImageURL is required");

        RuleFor(x => x.PhoneNumbers)
            .NotEmpty()
            .WithMessage("PhoneNumbers is required");

        RuleFor(x => x.WhatsUrl)
            .NotEmpty()
            .WithMessage("WhatsUrl is required");

        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Location is required");

        RuleFor(x => x.MapsLocation)
            .NotEmpty()
            .WithMessage("MapsLocation is required");
    }
}
