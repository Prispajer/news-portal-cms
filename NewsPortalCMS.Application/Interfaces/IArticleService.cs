using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Domain.Models;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface IArticleService
    {
        Task<Result<ArticleDetailDto>> CreateArticleAsync(CreateArticleDto createArticleDto);
        Task<Result<IEnumerable<ArticleListDto>>> GetArticlesAsync(string? status);
        Task<Result<ArticleStats>> GetArticlesStatsAsync();
        Task<Result<ArticleDetailDto>> GetArticleAsync(Guid id);
        Task<Result<ArticleDetailDto>> UpdateArticleAsync(Guid id, UpdateArticleDto updateArticleDto);
        Task<Result<ArticleDetailDto>> PublishArticleAsync(Guid id);

    }
}
