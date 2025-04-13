using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.CartItem;
using Medical_E_Commerce.Contracts.Fav;

namespace Medical_E_Commerce.Service.Fav;

public interface IFavService
{
    Task<Result> AddItem(string UserId, int ItemId);

    Task<Result<FavResponse>> Show(string UserId);
    Task<Result> Clear(string UserId);
}
