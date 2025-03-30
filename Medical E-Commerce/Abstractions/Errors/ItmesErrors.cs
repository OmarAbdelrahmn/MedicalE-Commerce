namespace Medical_E_Commerce.Abstractions.Errors;

public class ItmesErrors
{
    public static readonly Error ItmesNotFound = new("Itmes.Notfound", "Itmes Not Found ", StatusCodes.Status404NotFound);
    public static readonly Error PharmacyNameIsExist = new("Pharmacy.PharmacyNameIsExist", "Pharmacy with this name is already exists", StatusCodes.Status404NotFound);
    public static readonly Error noitem = new("Pharmacy.noItem", "No Item Found To Update", StatusCodes.Status404NotFound);

}
