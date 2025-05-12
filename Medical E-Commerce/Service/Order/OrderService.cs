namespace Medical_E_Commerce.Service.Order;

public class OrderService(ApplicationDbcontext dbcontext) : IOrderService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result<IEnumerable<OrderResopnse>>> GetpharmacyId(string UserId , int PharmacyId)
    {
        var PharmacyIsExcists = await dbcontext.Pharmacies.AnyAsync(c => c.Id == PharmacyId);
        if (!PharmacyIsExcists)
            return Result.Failure<IEnumerable<OrderResopnse>>(PharmacyErrors.PharmcayNotFound);

        var role = await dbcontext.Pharmacies.Where(c => c.Id == PharmacyId && c.AdminId == UserId).SingleOrDefaultAsync();

        if (role == null)
            return Result.Failure<IEnumerable<OrderResopnse>>(PharmacyErrors.Donthave);

        var orders = await dbcontext.Orders.Where(c => c.PharmacyId == PharmacyId).ToListAsync();

        if (orders == null)
            return Result.Failure<IEnumerable<OrderResopnse>>(RolesErrors.Orderno);

        var orres = orders.Adapt<IEnumerable<OrderResopnse>>();

        return Result.Success<IEnumerable<OrderResopnse>>(orres);
    }

    public async Task<Result<IEnumerable<OrderResopnse>>> GetUserId(string Userid)
    {
        var orders = await dbcontext.Orders.Where(c => c.UserId == Userid).ToListAsync();

        if (orders == null)
            return Result.Failure<IEnumerable<OrderResopnse>>(RolesErrors.Ordernon);

        var orres = orders.Adapt<IEnumerable<OrderResopnse>>();

        return Result.Success<IEnumerable<OrderResopnse>>(orres);
    }


}
