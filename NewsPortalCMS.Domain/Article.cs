namespace NewsPortalCMS.Domain
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
