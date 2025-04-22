using Medical_E_Commerce.Contracts.Fav;

namespace Medical_E_Commerce.Service.Fav;

public class FavService(ApplicationDbcontext dbcontext) : IFavService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result> AddItem(string UserId, int ItemId)
    {
        var Fav = await dbcontext.Fav
             .Where(c => c.UserId == UserId && c.ItemId == ItemId)
             .SingleOrDefaultAsync();
        if (Fav != null)
            return Result.Failure(CartErrors.ItemAlreadyInFav);

        var Item = await dbcontext.Items
            .Where(c => c.Id == ItemId)
            .SingleOrDefaultAsync();

        if (Item == null)
            return Result.Failure(ItmesErrors.ItmesNotFound);

        var favItem = new Entities.Fav()
        {
            UserId = UserId,
            ItemId = ItemId
        };
        await dbcontext.AddAsync(favItem);
        await dbcontext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> Clear(string UserId)
    {
        var item = await dbcontext.Fav
            .Where(c => c.UserId == UserId)
            .ToListAsync();

        if (item == null)
            return Result.Failure(CartErrors.FavNotFound);

        var dele = await dbcontext.Fav
            .Where(c => c.UserId == UserId)
            .ExecuteDeleteAsync();

        if (dele == 0)
            return Result.Failure(CartErrors.FavNotFound);

        return Result.Success();
    }

    public async Task<Result<FavResponse>> Show(string UserId)
    {
        var FavItems = await dbcontext.Fav.Where(c => c.UserId == UserId).Select(c => c.ItemId).ToListAsync();

        if (FavItems == null)
            return Result.Failure<FavResponse>(CartErrors.FavNotFound);


        var fav = FavItems.Adapt<FavResponse>();

        return Result.Success(fav);

    }
}
