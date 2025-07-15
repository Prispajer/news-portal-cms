using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewsPortalCMS.Infrastructure.Data;

public class NewsPortalCmsDbContextFactory : IDesignTimeDbContextFactory<NewsPortalCmsDbContext>
{
    public NewsPortalCmsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NewsPortalCmsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NewsPortalCMS;Username=postgres;Password=randompassword");
        return new NewsPortalCmsDbContext(optionsBuilder.Options);
    }
}


