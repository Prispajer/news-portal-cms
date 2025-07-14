namespace NewsPortalCMS.Application.Dto.Article
{
    public record UpdateArticleDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
