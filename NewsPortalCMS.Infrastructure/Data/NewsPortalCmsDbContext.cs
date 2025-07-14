using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Configurations;

namespace NewsPortalCMS.Infrastructure.Data
{
    public class NewsPortalCmsDbContext(DbContextOptions<NewsPortalCmsDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ArticleConfiguration().Configure(modelBuilder.Entity<Article>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
