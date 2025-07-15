namespace NewsPortalCMS.Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
        public DateTime CreatedAt { get; set; } 

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
