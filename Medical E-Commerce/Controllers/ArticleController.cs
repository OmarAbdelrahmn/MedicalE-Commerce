using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Consts;
using Medical_E_Commerce.Contracts.Article;
using Medical_E_Commerce.Service.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]

public class ArticleController(IArticleService service) : ControllerBase
{
    private readonly IArticleService service = service;

    [HttpGet("by-id/{Id}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken)
    {
        var ge = await service.GetByIdAsynce(Id, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    [HttpGet("")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {

        var ge = await service.GetAll(cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();

    }

    [HttpGet("by-name/{Name}")]
    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> GetByName(string Name, CancellationToken cancellationToken)
    {
        var ge = await service.GetByNameAsynce(Name, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    
    [HttpPost("")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Add(ArticleRequest request, CancellationToken cancellationToken)
    {
        var ge = await service.AddAsynce(request, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Update(int id, ArticleRequest request, CancellationToken cancellationToken)
    {
        var ge = await service.UpdateAsynce(id, request, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
}
