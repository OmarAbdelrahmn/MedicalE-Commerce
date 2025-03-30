using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Item;
using Medical_E_Commerce.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Medical_E_Commerce.Service.Item;

public class ItemService(ApplicationDbcontext dbcontext) : IItemService
{
    public async Task<Result<IEnumerable<ItemResponse>>> GetAllCare(int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c=>c.Id == PharmacyId);

        if(!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var CareItems =await dbcontext.Items.Where(c=>c.PharmacyId == PharmacyId && c.Type == "care")
            .ToListAsync();

        return Result.Success<IEnumerable<ItemResponse>>(CareItems.Adapt<IEnumerable<ItemResponse>>());
    }
    
    public async Task<Result<IEnumerable<ItemResponse>>> GetAllMedicine(int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c=>c.Id == PharmacyId);

        if(!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var MedicinItems =await dbcontext.Items.Where(c=>c.PharmacyId == PharmacyId && c.Type == "medicine")
            .ToListAsync();

        return Result.Success<IEnumerable<ItemResponse>>(MedicinItems.Adapt<IEnumerable<ItemResponse>>());
    }

    public async Task<Result<ItemResponse>> GetById(int PharmacyId, int id)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<ItemResponse>(PharmacyErrors.PharmcayNotFound);

        var item = await dbcontext.Items.FindAsync(id);

        if(item == null)
            return Result.Failure<ItemResponse>(ItmesErrors.ItmesNotFound);

        return Result.Success(item.Adapt<ItemResponse>());
    }

    public async Task<Result<IEnumerable<ItemResponse>>> GetByName(int PharmacyId, string Name)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);

        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<ItemResponse>>(PharmacyErrors.PharmcayNotFound);

        var item = await dbcontext.Items
            .Where(c=>c.Name.Contains(Name))
            .ToListAsync();

        if (item == null)
            return Result.Failure<IEnumerable<ItemResponse>>(ItmesErrors.ItmesNotFound);

        return Result.Success(item.Adapt<IEnumerable<ItemResponse>>());
    }
}
