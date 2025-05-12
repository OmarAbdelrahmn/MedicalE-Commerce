namespace Medical_E_Commerce.Service.Pharmacy;

public interface IPharmacyService
{
    Task<Result<IEnumerable<PharmacyResponse>>> GetByNameAsync(string name);
    Task<Result<SearchResultGroup>> GetalAsync(string name);
    Task<Result<PharmacyResponse>> GetByIdAsync(int Id);
    Task<Result<PharmacyResponse>> AddAsync(string UserId, PharmacyRequest request);
    Task<Result<PharmacyResponse>> UpdateAsync(string userId, int Id, PharmacyRequest request);
    Task<Result<IEnumerable<PharmacyResponse>>> GetAllAsync();
}
