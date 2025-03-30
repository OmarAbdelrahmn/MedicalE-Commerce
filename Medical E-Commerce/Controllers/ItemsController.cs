using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Service.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> GetbyId(int PharmacyId , int Id)
    {
        var result = await service.GetById(PharmacyId , Id);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }
    
    [HttpGet("by-name/{Name}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetbyName(int PharmacyId , string Name)
    {
        var result = await service.GetByName(PharmacyId , Name);

        return result.IsSuccess ?
            Ok(result.Value) :
            result.ToProblem();
    }
}
