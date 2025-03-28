using System.ComponentModel.DataAnnotations;

namespace Medical_E_Commerce.Contracts.Article;

public record ArticleRequest
(
    [Length(3, 50)]
    [Required]
    string Name,
    [Required]
    [Length(3, 500)]
    string Description
    );