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

            modelBuilder.Entity<Category>().HasData(
           new Category { Id = new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), Name = "Tech" },
           new Category { Id = new Guid("b2c3d4e5-f6a1-7890-1234-56789abcdef1"), Name = "Sport" }
       );

            modelBuilder.Entity<Article>().HasData(
                new Article
                {
                    Id = Guid.Parse("f9c68059-1e83-47a9-b6de-84ab11223344"),
                    Title = "Witaj!",
                    Content = "Pierwszy artykuł",
                    CategoryId = new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
