using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Contracts.Article;
using Medical_E_Commerce.Service.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_E_Commerce.Controllers;
[Route("[controller]")]
[ApiController]
public class ArticleController(IArticleService service) : ControllerBase
{
    private readonly IArticleService service = service;

    [HttpGet("by-id/{Id}")]
    public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken)
    {
        var ge = await service.GetByIdAsynce(Id, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {

        var ge = await service.GetAll(cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();

    }

    [HttpGet("by-name/{Name}")]
    public async Task<IActionResult> GetByName(string Name, CancellationToken cancellationToken)
    {
        var ge = await service.GetByNameAsynce(Name, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    
    [HttpPost("")]
    public async Task<IActionResult> Add(ArticleRequest request, CancellationToken cancellationToken)
    {
        var ge = await service.AddAsynce(request, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ArticleRequest request, CancellationToken cancellationToken)
    {
        var ge = await service.UpdateAsynce(id, request, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
}
