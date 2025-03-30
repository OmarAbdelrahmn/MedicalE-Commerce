using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Contracts.CartItem;

public record AddCartItemToCart
(
    [Required]
    int ItemId
    );
