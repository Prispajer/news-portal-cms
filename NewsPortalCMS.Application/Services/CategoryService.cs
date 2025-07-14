using AutoMapper;
using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Category;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDetailDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var dbCategory = _repository.GetByNameAsync(createCategoryDto.Name);
                if (dbCategory != null)
                {
                    return Result<CategoryDetailDto>.Failure("Category name must be unique.");
                }
                var category = _mapper.Map<Category>(createCategoryDto);
                category.Id = Guid.NewGuid();
                var createdCategory = await _repository.CreateAsync(category);

                var resultDto = _mapper.Map<CategoryDetailDto>(createdCategory);

                return Result<CategoryDetailDto>.Success(resultDto, "Pomyślnie utworzono dane!");
            }
            catch (Exception ex)
            {
                return Result<CategoryDetailDto>.Failure($"Błąd podczas tworzenia artykułu: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CategoryDetailDto>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _repository.GetAllAsync();

                var categoriesDto = _mapper.Map<IEnumerable<CategoryDetailDto>>(categories);

                return Result<IEnumerable<CategoryDetailDto>>.Success(categoriesDto, "Pomyślnie przywrócono dane!");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<CategoryDetailDto>>.Failure($"Błąd podczas pobierania artykułów: {ex.Message}");
            }
        }
    }
}
