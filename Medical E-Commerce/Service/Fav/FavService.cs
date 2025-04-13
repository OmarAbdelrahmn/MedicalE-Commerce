using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.CartItem;

namespace Medical_E_Commerce.Service.Fav;

public class FavService(ApplicationDbcontext dbcontext) : IFavService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result> AddItem(string UserId, int ItemId)
    {
        var FavItems = await dbcontext.Fav.Where(c => c.UserId == UserId).ToListAsync();

        if (FavItems == null)
            return Result.Failure(CartErrors.CartNotFound);

    }

    public Task<Result<CartResopse>> Clear(string UserId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CartResopse>> Show(string UserId)
    {
        throw new NotImplementedException();
    }
}
