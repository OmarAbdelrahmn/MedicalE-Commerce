namespace Medical_E_Commerce.Abstractions.Errors;

public class PharmacyErrors
{
    public static readonly Error PharmcayNotFound = new("Pharmacy.Notfound", "Pharmacy Not Found ", StatusCodes.Status404NotFound);
    public static readonly Error No = new("PharmacyOrItems.Notfound", "Pharmacy Or Items Not Found ", StatusCodes.Status404NotFound);
    public static readonly Error PharmacyNameIsExist = new("Pharmacy.PharmacyNameIsExist", "Pharmacy with this name is already exists", StatusCodes.Status404NotFound);
    public static readonly Error Donthave = new("access deline", "you do not have the right to access this Pharmacy", StatusCodes.Status401Unauthorized);

}
