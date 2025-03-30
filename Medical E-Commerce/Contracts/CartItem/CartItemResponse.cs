namespace Medical_E_Commerce.Contracts.CartItem;

public record CartItemResponse
(
    int Id,
    int Count,
    int ItemId,
    int CartId
    );
