using Mapster;
using Medical_E_Commerce.Abstractions;
using Medical_E_Commerce.Abstractions.Errors;
using Medical_E_Commerce.Contracts.Article;
using Medical_E_Commerce.Persistence;
using Medical_E_Commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Article;

public class ArticleService(ApplicationDbcontext context) : IArticleService
{
    public async Task<Result<ArticleResponse>> AddAsynce(ArticleRequest request, CancellationToken cancellationToken = default)
    {
        var Article = request.Adapt<Entities.Article>();

        await context.Articles.AddAsync(Article);
        await context.SaveChangesAsync();

        return Result.Success(Article.Adapt<ArticleResponse>());

    }

    public async Task<Result<IEnumerable<ArticleResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        var articles = await context.Articles
            .ProjectToType<ArticleResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<ArticleResponse>>(articles);
    }

    public async Task<Result<ArticleResponse>> GetByIdAsynce(int id, CancellationToken cancellationToken = default)
    {
        var articles = await context.Articles
            .Where(x => x.Id == id)
            .ProjectToType<ArticleResponse>()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (articles is null)
            return Result.Failure<ArticleResponse>(ArticleErrors.ArticleNotNound);

        return Result.Success(articles);
    }

    public async Task<Result<IEnumerable<ArticleResponse>>> GetByNameAsynce(string Name, CancellationToken cancellationToken = default)
    {
        var articles = await context.Articles
                   .Where(x => x.Name.Contains(Name))
                   .ProjectToType<ArticleResponse>()
                   .AsNoTracking()
                   .ToListAsync(cancellationToken);

        if (articles is null)
            return Result.Failure<IEnumerable<ArticleResponse>>(ArticleErrors.ArticleNotNound);

        return Result.Success<IEnumerable<ArticleResponse>>(articles);
    }

    public async Task<Result<ArticleResponse>> UpdateAsynce(int id, ArticleRequest request, CancellationToken cancellationToken = default)
    {
        var article = await context.Articles
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            return Result.Failure<ArticleResponse>(ArticleErrors.ArticleNotNound);

        article.Name = request.Name;
        article.Description = request.Description;

        context.Articles.Update(article);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(article.Adapt<ArticleResponse>());
    }
}
