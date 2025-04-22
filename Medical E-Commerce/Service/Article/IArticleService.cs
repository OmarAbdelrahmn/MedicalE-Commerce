using Medical_E_Commerce.Contracts.Article;

namespace Medical_E_Commerce.Service.Article;

public interface IArticleService
{
    Task<Result<ArticleResponse>> GetByIdAsynce(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ArticleResponse>>> GetByNameAsynce(string Name, CancellationToken cancellationToken = default);
    Task<Result<ArticleResponse>> AddAsynce(ArticleRequest request, CancellationToken cancellationToken = default);
    Task<Result<ArticleResponse>> UpdateAsynce(int id, ArticleRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ArticleResponse>>> GetAll(CancellationToken cancellationToken = default);
}
