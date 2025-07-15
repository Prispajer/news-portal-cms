using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Validators.Business
{
    public class ArticleBusinessValidator
    {
        public static Result<Article> ValidateArticleExists(Article? article)
        {
            if (article == null)
                return Result<Article>.Failure("Article was not found");
            return Result<Article>.Success(article, "Article was found");
        }
    }
}

