namespace Medical_E_Commerce.Service.Order;

public interface IOrderService
{
    Task<Result<IEnumerable<OrderResopnse>>> GetUserId(string Userid);
    Task<Result<IEnumerable<OrderResopnse>>> GetpharmacyId(int PharmacyId);

}
