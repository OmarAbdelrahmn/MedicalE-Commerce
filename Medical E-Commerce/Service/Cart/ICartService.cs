using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.CartItem;

namespace Medical_E_Commerce.Service.Cart;

public interface ICartService
{
    Task<Result> AddItem(string UserId, AddCartItemToCart Item);

    Task<Result> CreateCart(string UserId);
    Task<Result<CartResopse>> Show(string UserId);
    Task<Result> Clear(string UserId);
    Task<Result> Pay(string UserId);
}
