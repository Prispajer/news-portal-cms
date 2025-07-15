using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Validators.Business
{
    public class CategoryBusinessValidator
    {
        public static Result<Category> ValidateCategoryExists(Category? category)
        {
            if (category == null)
                return Result<Category>.Failure("Category was not found");
            return Result<Category>.Success(category, "Category was found");
        }
    }
}
