using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsPortalCmsDbContext _context;

        public CategoryRepository(NewsPortalCmsDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);

        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(a => a.Articles).ToListAsync();
        }

        public async Task<bool> GetByNameAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
