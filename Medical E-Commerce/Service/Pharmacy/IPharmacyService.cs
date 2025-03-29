using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Pharmacy;

namespace Medical_E_Commerce.Service.Pharmacy;

public interface IPharmacyService
{
    Task<Result<PharmacyResponse>> GetByNameAsync(string name);
    Task<Result<PharmacyResponse>> GetByIdAsync(int Id);
    Task<Result<PharmacyResponse>> AddAsync(PharmacyRequest request);
    Task<Result<PharmacyResponse>> UpdateAsync(int Id , PharmacyRequest request);
    Task<Result<IEnumerable<PharmacyResponse>>> GetAllAsync();
}
