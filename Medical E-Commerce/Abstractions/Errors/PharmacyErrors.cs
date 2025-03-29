namespace Medical_E_Commerce.Abstractions.Errors;

public class PharmacyErrors
{
    public static readonly Error PharmcayNotFound = new("Pharmacy.Notfound", "Pharmacy Not Found ", StatusCodes.Status404NotFound);

}
