using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("varchar(100)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(c => c.Articles).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId).IsRequired();
        }
    }
}
