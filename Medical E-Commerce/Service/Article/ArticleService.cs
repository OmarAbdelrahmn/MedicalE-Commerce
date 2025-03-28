using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Article;
using Medical_E_Commerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Article;

public class ArticleService(ApplicationDbcontext context) : IArticleService
{
    public async Task<Result<IEnumerable<ArticleResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var Diseases = await context.Articles
            .ProjectToType<ArticleResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<ArticleResponse>>(Diseases);
    }

    public async Task<Result<ArticleResponse>> GetByIdAsynce(int id, CancellationToken cancellationToken = default)
    {
        var Diseases = await context.Articles
            .Where(x => x.Id == id)
            .ProjectToType<ArticleResponse>()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (Diseases is null)
            return Result.Failure<ArticleResponse>(ArticleErrors.ArticleNotNound);

        return Result.Success(Diseases);
    }

    public async Task<Result<ArticleResponse>> GetByNameAsynce(string Name, CancellationToken cancellationToken = default)
    {
        var Diseases = await context.Articles
                   .Where(x => x.Name.Contains(Name))
                   .ProjectToType<ArticleResponse>()
                   .AsNoTracking()
                   .SingleOrDefaultAsync(cancellationToken);

        if (Diseases is null)
            return Result.Failure<ArticleResponse>(ArticleErrors.ArticleNotNound);

        return Result.Success(Diseases);
    }
}
