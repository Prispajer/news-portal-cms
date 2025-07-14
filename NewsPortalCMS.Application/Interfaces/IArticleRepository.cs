using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface IArticleRepository
    {
        Task<Article> CreateAsync(Article article);
        Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status);
        Task<Article?> GetByIdAsync(Guid id);
        Task UpdateAsync(Article article);
        Task<Article?> GetBySlugAsync(string slug);
    }
}
