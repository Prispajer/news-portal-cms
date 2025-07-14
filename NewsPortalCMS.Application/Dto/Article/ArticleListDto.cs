using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Dto.Article
{
    public record ArticleListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public ArticleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
