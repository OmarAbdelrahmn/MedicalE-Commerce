using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Service.Item;

public interface IItemService
{
    Task<Result<ItemResponse>> GetById(int PharmacyId , int id);
    Task<Result<IEnumerable<ItemResponse>>> GetByName(int PharmacyId , string Name);
    Task<Result<IEnumerable<ItemResponse>>> GetAllCare(int PharmacyId);
    Task<Result<IEnumerable<ItemResponse>>> GetAllMedicine(int PharmacyId);
    Task<Result<IEnumerable<ItemResponse>>> AddAsync(int PharmacyId , ItemRequest request);
    Task<Result<IEnumerable<ItemResponse>>> UpdateAsync(int PharmacyId , ItemRequest request);
}
