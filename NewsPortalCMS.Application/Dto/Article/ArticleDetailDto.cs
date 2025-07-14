using NewsPortalCMS.Application.Dto.Category;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Dto.Article
{
    public record ArticleDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public ArticleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public CategoryDetailDto Category { get; set; } = null!;
    }
}
