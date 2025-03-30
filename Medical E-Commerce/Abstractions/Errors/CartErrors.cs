namespace Medical_E_Commerce.Abstractions.Errors;

public class CartErrors
{
    public static readonly Error CartIsAlreadyExsicts = new("Cart.CartError", "This user Already have a Cart", StatusCodes.Status405MethodNotAllowed);
    public static readonly Error CartNotFound = new("Cart.NotFound", "Cart Not Found , Add one first", StatusCodes.Status404NotFound);

}
