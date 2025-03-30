
using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.CartItem;
using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Cart;

public class CartService(UserManager<ApplicationUser> manager, ApplicationDbcontext dbcontext) : ICartService
{
    private readonly UserManager<ApplicationUser> manager = manager;
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result> AddItem(string UserId ,AddCartItemToCart Item)
    {
        var cart = await dbcontext.Carts.Where(c=>c.UserId == UserId).SingleOrDefaultAsync();

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

    public async Task<Result<CartResopse>> Clear(string UserId)
    {
        var cart = await dbcontext.Carts.Where(c => c.UserId == UserId).SingleOrDefaultAsync();

        if (cart == null)
            return Result.Failure<CartResopse>(CartErrors.CartNotFound);

        var dv = await dbcontext.CartItems
            .Where(c => c.CartId == cart.Id)
            .ExecuteDeleteAsync();

        return Result.Success(cart.Adapt<CartResopse>());
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

    public async Task<Result<CartResopse>> Show(string UserId)
    {
        var cart = await dbcontext.Carts.Where(c=>c.UserId == UserId)
            .Select(c=> new CartResopse(
                c.Id,
                c.UserId,
                c.Items!.Select(r=>new CartItemResponse(
                    r.Id,
                    r.Count,
                    r.ItemId,
                    r.CartId
                    )
                ).ToList()))
            .SingleOrDefaultAsync();

        if(cart == null)
            return Result.Failure<CartResopse>(CartErrors.CartNotFound);

       return Result.Success(cart);
    }
}
