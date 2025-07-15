using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Domain.Models;

namespace NewsPortalCMS.Domain.Services
{
    public class ArticleStatsService
    {
        public static ArticleStats GenerateArticleStatsAsync(IEnumerable<Article> articles)
        {
            var publishedCount = articles.Count(a => a.Status == ArticleStatus.Published);
            var draftCount = articles.Count(a => a.Status == ArticleStatus.Draft);
            var mostUsedCategory = articles.GroupBy(a => a.Category.Name).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();

            return ArticleStats.Create(publishedCount, draftCount, mostUsedCategory ?? "No data");
        }
    }
}
