using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts;
using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Service.Order;

public interface IOrderService
{
    Task<Result<OrderResopnse>> GetUserId(int Userid);
    Task<Result<OrderResopnse>> GetpharmacyId(int PharmacyId);

}
