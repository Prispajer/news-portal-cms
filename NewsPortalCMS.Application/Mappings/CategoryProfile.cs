using AutoMapper;
using NewsPortalCMS.Application.Dto.Category;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDetailDto>();
            CreateMap<CategoryDetailDto, Category>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
