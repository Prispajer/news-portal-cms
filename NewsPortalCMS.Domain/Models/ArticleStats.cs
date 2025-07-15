namespace NewsPortalCMS.Domain.Models
{
    public class ArticleStats
    {
        public int PublishedCount { get; }
        public int DraftCount { get; }
        public string MostUsedCategory { get; } = string.Empty;

        public ArticleStats(int publishedCount, int draftCount, string mostUsedCategory)
        {
            PublishedCount = publishedCount;
            DraftCount = draftCount;
            MostUsedCategory = mostUsedCategory;
        }

        public static ArticleStats Create(int publishedCount, int draftCount, string mostUsedCategory)
            => new ArticleStats(publishedCount, draftCount, mostUsedCategory);
    }
}
