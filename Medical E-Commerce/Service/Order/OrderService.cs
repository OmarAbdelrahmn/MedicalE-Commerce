﻿using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts;

namespace Medical_E_Commerce.Service.Order;

public class OrderService(ApplicationDbcontext dbcontext) : IOrderService
{
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Result<IEnumerable<OrderResopnse>>> GetpharmacyId(int PharmacyId)
    {
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
