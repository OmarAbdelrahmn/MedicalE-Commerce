using Medical_E_Commerce.Abstractions;
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
    public async Task<IActionResult> GetById(string Name, CancellationToken cancellationToken)
    {
        var ge = await service.GetByNameAsynce(Name, cancellationToken);
        return ge.IsSuccess ? Ok(ge.Value) : ge.ToProblem();
    }
}
