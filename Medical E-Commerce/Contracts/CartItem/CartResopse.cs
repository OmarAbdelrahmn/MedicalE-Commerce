namespace Medical_E_Commerce.Contracts.CartItem;

public record CartResopse
(
    int Id ,
    string UserId ,
     IList<CartItemResponse>? Items
    );
