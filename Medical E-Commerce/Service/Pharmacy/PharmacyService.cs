using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Pharmacy;
using Medical_E_Commerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Pharmacy;

public class PharmacyService(ApplicationDbcontext dbcontext) : IPharmacyService
{
    public async Task<Result<IEnumerable<PharmacyResponse>>> GetAllAsync()
    {
        var pharmacy = await dbcontext.Pharmacies
            .Include(c => c.Items)
            .ProjectToType<PharmacyResponse>()
            .AsNoTracking()
            .ToListAsync();

        if (pharmacy is null)
            return Result.Failure<IEnumerable<PharmacyResponse>>(PharmacyErrors.PharmcayNotFound);

        return Result.Success<IEnumerable<PharmacyResponse>>(pharmacy);
    }

    public async Task<Result<PharmacyResponse>> GetByIdAsync(int Id)
    {
       var pharmacy = await dbcontext.Pharmacies
            .Include(c=>c.Items)
            .Where(c=>c.Id == Id)
            .ProjectToType<PharmacyResponse>()
            .SingleOrDefaultAsync();

        if(pharmacy is null)
            return Result.Failure<PharmacyResponse>(PharmacyErrors.PharmcayNotFound);

        return Result.Success(pharmacy);
 
    }

    public async Task<Result<PharmacyResponse>> GetByNameAsync(string name)
    {
        var pharmacy = await dbcontext.Pharmacies
            .Include(c => c.Items)
            .Where(c => c.Name.Contains(name))
            .ProjectToType<PharmacyResponse>()
            .SingleOrDefaultAsync();

        if (pharmacy is null)
            return Result.Failure<PharmacyResponse>(PharmacyErrors.PharmcayNotFound);

        return Result.Success(pharmacy);
    }
}
