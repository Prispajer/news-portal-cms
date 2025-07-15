using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Domain.Services;

public class ArticleStatsServiceTests
{
    [Fact]
    public void GenerateStats_CorrectlyCountsArticlesAndPopularCategory()
    {
        var category1 = new Category { Id = Guid.NewGuid(), Name = "Tech" };
        var category2 = new Category { Id = Guid.NewGuid(), Name = "Biz" };

        var articles = new List<Article>
        {
            new() { Status = ArticleStatus.Published, Category = category1 },
            new() { Status = ArticleStatus.Draft, Category = category2 },
            new() { Status = ArticleStatus.Published, Category = category1 },
        };

        var stats = ArticleStatsService.GenerateArticleStatsAsync(articles);

        Assert.Equal(2, stats.PublishedCount);
        Assert.Equal(1, stats.DraftCount);
        Assert.Equal("Tech", stats.MostUsedCategory);
    }
}
