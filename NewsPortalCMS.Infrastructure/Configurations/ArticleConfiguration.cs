using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations
{
    public class ArticleConfiguration: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Content).HasColumnType("text").IsRequired();
            builder.Property(x => x.Author).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Slug).HasColumnType("varchar(150)").IsRequired();
            builder.Property(x => x.Status).HasConversion(v => v.ToString(), v => (ArticleStatus)Enum.Parse(typeof(ArticleStatus), v)).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        }
    }
}
