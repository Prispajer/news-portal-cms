using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<bool> GetByNameAsync(string name);
    }
}
