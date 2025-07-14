namespace NewsPortalCMS.Application.Dto.Article
{
    public record CreateArticleDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }
}
