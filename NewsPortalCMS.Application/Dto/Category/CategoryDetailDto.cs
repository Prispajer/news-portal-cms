using NewsPortalCMS.Application.Dto.Article;

namespace NewsPortalCMS.Application.Dto.Category
{
    public record CategoryDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
