namespace Medical_E_Commerce.Abstractions.Errors;

public class CartErrors
{
    public static readonly Error CartIsAlreadyExsicts = new("Cart.CartError", "This user Already have a Cart", StatusCodes.Status405MethodNotAllowed);
    public static readonly Error CartNotFound = new("Cart.NotFound", "Cart Not Found , Add one first", StatusCodes.Status404NotFound);
    public static readonly Error NoItem = new("Cart.NoItems", "No Item Found , Add one first", StatusCodes.Status404NotFound);
    public static readonly Error somethingwentwrong = new("Some thing went wrong ", "some thing went wrong in saving the order", StatusCodes.Status404NotFound);
    public static readonly Error FavNotFound = new("fav.NotFound", "no favourite items found, Add one first", StatusCodes.Status404NotFound);
    public static readonly Error ItemAlreadyInFav = new("fav.ItemAlreadyInFav", "item in favourite list already", StatusCodes.Status404NotFound);

}
