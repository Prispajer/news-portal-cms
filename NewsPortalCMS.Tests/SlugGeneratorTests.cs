using NewsPortalCMS.Application.Services;
using Moq;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

public class SlugGeneratorTests
{
    [Fact]
    public async Task GenerateUniqueSlugAsync_GeneratesSlugCorrectly()
    {
        var repoMock = new Mock<IArticleRepository>();
        repoMock.Setup(r => r.GetBySlugAsync(It.IsAny<string>())).ReturnsAsync((Article?)null);

        var service = new SlugService(repoMock.Object);

        var slug = await service.GenerateUniqueSlugAsync("Mój piękny tytuł!");

        Assert.Equal("moj-piekny-tytul", slug);
    }
}
