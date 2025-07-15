using AutoMapper;
using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Category;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDetailDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var dbCategory = await _categoryRepository.GetByNameAsync(createCategoryDto.Name);

            if (dbCategory)
            {
                return Result<CategoryDetailDto>.Failure("Category name must be unique");
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            category.Id = Guid.NewGuid();
            var createdCategory = await _categoryRepository.CreateAsync(category);

            var categoryDetailDto = _mapper.Map<CategoryDetailDto>(createdCategory);

            return Result<CategoryDetailDto>.Success(categoryDetailDto, "Data has been created");
        }

        public async Task<Result<IEnumerable<CategoryDetailDto>>> GetCategoriesAsync()
        {
            var dbCategories = await _categoryRepository.GetAllAsync();

            var categoriesDetailDto = _mapper.Map<IEnumerable<CategoryDetailDto>>(dbCategories);

            return Result<IEnumerable<CategoryDetailDto>>.Success(categoriesDetailDto, "Data has been restored");
        }
    }
}
