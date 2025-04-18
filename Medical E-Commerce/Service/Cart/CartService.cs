using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.CartItem;

namespace Medical_E_Commerce.Service.Cart;

public class CartService(UserManager<ApplicationUser> manager, ApplicationDbcontext dbcontext) : ICartService
{
    private readonly UserManager<ApplicationUser> manager = manager;
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result> AddItem(string UserId, AddCartItemToCart Item)
    {
        var cart = await dbcontext.Carts.Where(c => c.UserId == UserId).SingleOrDefaultAsync();

        if (cart == null)
            return Result.Failure(CartErrors.CartNotFound);

        cart.Items!.Add(new Entities.CartItem()
        {
            CartId = cart.Id,
            ItemId = Item.ItemId,
            Count = 1
        });

        dbcontext.Update(cart);
        dbcontext.SaveChanges();

        if (cart.Items.Any())
            return Result.Success();

        return Result.Failure(new Error("CartError", "some thing went wrong", StatusCodes.Status500InternalServerError));
    }

    public async Task<Result> Clear(string UserId)
    {
        var cart = await dbcontext.Carts.Where(c => c.UserId == UserId).SingleOrDefaultAsync();

        if (cart == null)
            return Result.Failure<CartResopse>(CartErrors.CartNotFound);

        var dv = await dbcontext.CartItems
            .Where(c => c.CartId == cart.Id)
            .ExecuteDeleteAsync();

        return Result.Success();
    }

    public async Task<Result> CreateCart(string UserId)
    {
        var cartisex = await dbcontext.Carts.AnyAsync(c => c.UserId == UserId);

        if (cartisex)
            return Result.Failure(CartErrors.CartIsAlreadyExsicts);

        var cart = new Entities.Cart()
        {
            UserId = UserId
        };

        await dbcontext.AddAsync(cart);
        await dbcontext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> Pay(string UserId)
    {
        var cart = await dbcontext.Carts.Where(c => c.UserId == UserId).SingleOrDefaultAsync();

        if (cart == null)
            return Result.Failure<CartResopse>(CartErrors.CartNotFound);

        var cartItems = await dbcontext.CartItems.AnyAsync(c => c.CartId == cart.Id);

        if (!cartItems)
            return Result.Failure<CartResopse>(CartErrors.NoItem);

        var OrderPrice = await dbcontext.CartItems
            .Where(c => c.CartId == cart.Id)
            .Select(c => c.Item.Price * c.Count).SumAsync();

        var pharmayID = cart.Items!.Select(c => c.Item.PharmacyId).FirstOrDefault();

        var ItemsId = cart.Items!.Select(c => c.ItemId).ToList();

        var order = new Entities.Order()
        {
            UserId = UserId,
            ItemsId = ItemsId,
            TotalPrice = OrderPrice,
            PharmacyId = pharmayID
        };

        var result = await dbcontext.Orders.AddAsync(order);
        await dbcontext.SaveChangesAsync();

        if (result == null)
            return Result.Failure<CartResopse>(CartErrors.somethingwentwrong);

        var dv = await dbcontext.CartItems
            .Where(c => c.CartId == cart.Id)
            .ExecuteDeleteAsync();

        if (dv == 0)
            return Result.Failure<CartResopse>(CartErrors.somethingwentwrong);


        return Result.Success();

    }

    public async Task<Result<CartResopse>> Show(string UserId)
    {
        var cart = await dbcontext.Carts.Where(c => c.UserId == UserId)
            .Select(c => new CartResopse(
                c.Id,
                c.UserId,
                c.Items!.Select(r => new CartItemResponse(
                    r.Id,
                    r.Count,
                    r.ItemId,
                    r.CartId
                    )
                ).ToList()))
            .SingleOrDefaultAsync();

        if (cart == null)
            return Result.Failure<CartResopse>(CartErrors.CartNotFound);

        return Result.Success(cart);
    }
}
