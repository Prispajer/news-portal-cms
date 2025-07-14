using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<bool> GetByNameAsync(string name);
    }
}
