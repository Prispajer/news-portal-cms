using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly NewsPortalCmsDbContext _context;

        public ArticleRepository(NewsPortalCmsDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateAsync(Article article)
        {
            await _context.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status)
        {
            IQueryable<Article> query = _context.Articles.Include(a => a.Category);
            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status);
            }
            return await query.ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(Guid id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<Article?> GetBySlugAsync(string slug)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }
    }
}
