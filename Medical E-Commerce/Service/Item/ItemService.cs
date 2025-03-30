using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Item;
using Medical_E_Commerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Item;

public class ItemService(ApplicationDbcontext dbcontext) : IItemService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result<ItemResponse>> AddAsync(int PharmacyId, ItemRequest request)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<ItemResponse>(PharmacyErrors.PharmcayNotFound);

        var Item = await dbcontext.Items.Where(c => c.Name == request.Name).SingleOrDefaultAsync();

        if (Item == null)
        {
            var newitem = new Entities.Item()
            {
                Name = request.Name,
                PharmacyId = PharmacyId,
                Brand = request.Brand,
                Count = request.Count,
                ImageURL = request.ImageURL,
                EffectiveSubstance = request.EffectiveSubstance,
                Price = request.Price,
                Type = request.Type
            };
            await dbcontext.Items.AddAsync(newitem);
            await dbcontext.SaveChangesAsync();

            var Itemres = new ItemResponse(newitem.Id, newitem.Name, newitem.Type, newitem.ImageURL, newitem.Price, newitem.EffectiveSubstance, newitem.Count, newitem.Brand);
            return Result.Success(Itemres);
        }

        Item.Count = Item.Count + request.Count;

        dbcontext.Update(Item);
        await dbcontext.SaveChangesAsync();

        return Result.Success(Item.Adapt<ItemResponse>());


    }

    public async Task<Result<IEnumerable<ItemResponse>>> GetAll(int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var CareItems = await dbcontext.Items
            .Where(c => c.PharmacyId == PharmacyId)
            .AsNoTracking()
            .ToListAsync();

        return Result.Success(CareItems.Adapt<IEnumerable<ItemResponse>>());
    }

    public async Task<Result<IEnumerable<ItemResponse>>> GetAllCare(int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var CareItems = await dbcontext.Items
            .Where(c => c.PharmacyId == PharmacyId && c.Type == "care")
            .ToListAsync();

        return Result.Success<IEnumerable<ItemResponse>>(CareItems.Adapt<IEnumerable<ItemResponse>>());
    }

    public async Task<Result<IEnumerable<ItemResponse>>> GetAllMedicine(int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var MedicinItems = await dbcontext.Items.Where(c => c.PharmacyId == PharmacyId && c.Type == "medicine")
            .ToListAsync();

        return Result.Success<IEnumerable<ItemResponse>>(MedicinItems.Adapt<IEnumerable<ItemResponse>>());
    }

    public async Task<Result<ItemResponse>> GetById(int PharmacyId, int id)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<ItemResponse>(PharmacyErrors.PharmcayNotFound);

        var item = await dbcontext.Items.FindAsync(id);

        if (item == null)
            return Result.Failure<ItemResponse>(ItmesErrors.ItmesNotFound);

        return Result.Success(item.Adapt<ItemResponse>());
    }

    public async Task<Result<IEnumerable<ItemResponse>>> GetByName(int PharmacyId, string Name)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var item = await dbcontext.Items
            .Where(c => c.Name.Contains(Name))
            .ToListAsync();

        if (item == null)
            return Result.Failure<IEnumerable<ItemResponse>>(ItmesErrors.ItmesNotFound);

        return Result.Success(item.Adapt<IEnumerable<ItemResponse>>());
    }

    public async Task<Result<ItemResponse>> UpdateAsync(int PharmacyId, int ItemId, ItemRequest request)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<ItemResponse>(PharmacyErrors.PharmcayNotFound);

        var Item = await dbcontext.Items.FindAsync(ItemId);

        if (Item == null)
            return Result.Failure<ItemResponse>(ItmesErrors.noitem);


        Item.EffectiveSubstance = request.EffectiveSubstance;
        Item.Price = request.Price;
        Item.Name = request.Name;
        Item.Brand = request.Brand;
        Item.Count = request.Count;
        Item.Type = request.Type;

        dbcontext.Items.Update(Item);
        await dbcontext.SaveChangesAsync();

        return Result.Success(Item.Adapt<ItemResponse>());
    }
}
