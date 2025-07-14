using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Application.Dto.Category;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryDetailDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Result<IEnumerable<CategoryDetailDto>>> GetCategoriesAsync();
    }
}
