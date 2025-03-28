namespace Medical_E_Commerce.Abstractions.Errors;

public class ArticleErrors
{
    public static readonly Error ArticleNotNound = new("Article.Notfound", "Article Not Found in the system, would you add it ??", StatusCodes.Status404NotFound);

}
