﻿using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Pharmacy;
using Medical_E_Commerce.Service.Pharmacy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PharmacyController(IPharmacyService service) : ControllerBase
{
    private readonly IPharmacyService service = service;

    [HttpGet("by-name/{Name}")]
    public async Task<IActionResult> GetByNameAsync(string Name)
    {
        var result = await service.GetByNameAsync(Name);

        return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();
    }
    
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await service.GetByIdAsync(id);

        return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAsync()
    {
        var result = await service.GetAllAsync();

        return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();
    }
    
    [HttpPost("")]
    public async Task<IActionResult> AddAsync([FromBody] PharmacyRequest request)
    {
        var result = await service.AddAsync(request);

        return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();
    }
    
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute]int Id , [FromBody] PharmacyRequest request)
    {
        var result = await service.UpdateAsync(Id,request);

        return result.IsSuccess ?
            Ok(result.Value)
            : result.ToProblem();
    }
}
