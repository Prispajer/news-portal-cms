using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Dto.Category;
using NewsPortalCMS.Application.Interfaces;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDetailDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryService.CreateCategoryAsync(createCategoryDto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);     
        return CreatedAtAction(nameof(GetCategories), new { articleId = result.Data!.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDetailDto>>> GetCategories()
    {
        var result = await _categoryService.GetCategoriesAsync();
        return Ok(result);
    }
}
