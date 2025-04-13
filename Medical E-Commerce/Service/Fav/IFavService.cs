using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.CartItem;

namespace Medical_E_Commerce.Service.Fav;

public interface IFavService
{
    Task<Result> AddItem(string UserId, int ItemId);

    Task<Result<CartResopse>> Show(string UserId);
    Task<Result<CartResopse>> Clear(string UserId);
}
