using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Service.Pharmacy;

public class PharmacyService(ApplicationDbcontext dbcontext) : IPharmacyService
{
    public async Task<Result<PharmacyResponse>> AddAsync(PharmacyRequest request)
    {
        var PhamacyNameIsExist = await dbcontext.Pharmacies.AnyAsync(c => c.Name == request.Name);

        if (PhamacyNameIsExist)
            return Result.Failure<PharmacyResponse>(PharmacyErrors.PharmacyNameIsExist);

        var pharmacy = request.Adapt<Entities.Pharmacy>();

        await dbcontext.Pharmacies.AddAsync(pharmacy);
        await dbcontext.SaveChangesAsync();

        return Result.Success(pharmacy.Adapt<PharmacyResponse>());
    }

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

    public async Task<Result<SearchResultGroup>> GetalAsync(string name)
    {
        var result = new SearchResultGroup
        {
            Pharmacies = dbcontext.Pharmacies
         .Where(p => p.Name.Contains(name))
         .ProjectToType<PharmacyResponse>()
         .ToList(),

            Items =  dbcontext.Items
         .Where(i => i.Name.Contains(name))
         .ProjectToType<ItemResponse>()
         .ToList()
        };

        if (result.Pharmacies.Count == 0 && result.Items.Count == 0)
            return Result.Failure<SearchResultGroup>(PharmacyErrors.No);

        return Result.Success(result);
    }

    public async Task<Result<PharmacyResponse>> GetByIdAsync(int Id)
    {
        var pharmacy = await dbcontext.Pharmacies
             .Include(c => c.Items)
             .Where(c => c.Id == Id)
             .ProjectToType<PharmacyResponse>()
             .SingleOrDefaultAsync();

        if (pharmacy is null)
            return Result.Failure<PharmacyResponse>(PharmacyErrors.PharmcayNotFound);

        return Result.Success(pharmacy);

    }

    public async Task<Result<IEnumerable<PharmacyResponse>>> GetByNameAsync(string name)
    {
        var pharmacy = await dbcontext.Pharmacies
            .Include(c => c.Items)
            .Where(c => c.Name.Contains(name))
            .ProjectToType<PharmacyResponse>()
            .ToListAsync();

        if (pharmacy is null)
            return Result.Failure<IEnumerable<PharmacyResponse>>(PharmacyErrors.PharmcayNotFound);

        return Result.Success<IEnumerable<PharmacyResponse>>(pharmacy);
    }

    public async Task<Result<PharmacyResponse>> UpdateAsync(int Id, PharmacyRequest request)
    {

        var PhamacyIsExist = await dbcontext.Pharmacies.AnyAsync(c => c.Name == request.Name && c.Id != Id);

        if (PhamacyIsExist)
            return Result.Failure<PharmacyResponse>(PharmacyErrors.PharmacyNameIsExist);

        var pharmacy = await dbcontext.Pharmacies.FindAsync(Id);

        pharmacy!.Location = request.Location;
        pharmacy.ImageURL = request.ImageURL;
        pharmacy.Name = request.Name;
        pharmacy.PhoneNumbers = request.PhoneNumbers;
        pharmacy.WhatsUrl = request.WhatsUrl;
        pharmacy.MapsLocation = request.MapsLocation;


        dbcontext.Pharmacies.Update(pharmacy);
        await dbcontext.SaveChangesAsync();

        return Result.Success(pharmacy.Adapt<PharmacyResponse>());
    }
}
