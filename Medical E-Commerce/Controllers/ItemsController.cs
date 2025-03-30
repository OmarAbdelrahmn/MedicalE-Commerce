using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("pharmacy/{PharmacyId}/[controller]")]
[ApiController]
public class ItemsController(IItemService service) : ControllerBase
{
    private readonly IItemService service = service;

    [HttpGet("care")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetAllCares(int PharmacyId)
    {
        var result = await service.GetAllCare(PharmacyId);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpGet("")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetAll(int PharmacyId)
    {
        var result = await service.GetAll(PharmacyId);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpGet("medicine")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetAllMedicines(int PharmacyId)
    {
        var result = await service.GetAllMedicine(PharmacyId);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpGet("by-id/{Id}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetbyId(int PharmacyId, int Id)
    {
        var result = await service.GetById(PharmacyId, Id);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpGet("by-name/{Name}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetbyName(int PharmacyId, string Name)
    {
        var result = await service.GetByName(PharmacyId, Name);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddItems(int PharmacyId, ItemRequest request)
    {
        var result = await service.AddAsync(PharmacyId, request);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }

    [HttpPut("{ItemId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateItems(int PharmacyId, int ItemId, ItemRequest request)
    {
        var result = await service.UpdateAsync(PharmacyId, ItemId, request);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }
}
