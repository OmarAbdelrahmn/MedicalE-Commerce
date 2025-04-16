using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts;

namespace Medical_E_Commerce.Service.Order;

public class OrderService(ApplicationDbcontext dbcontext) : IOrderService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result<OrderResopnse>> GetpharmacyId(int PharmacyId)
    {
        var orders = await dbcontext.Orders.Where(c => c.PharmacyId == PharmacyId).ToListAsync();

        if (orders == null)
            return Result.Failure<OrderResopnse>();
    }

    public Task<Result<OrderResopnse>> GetUserId(int Userid)
    {
        throw new NotImplementedException();
    }
}
