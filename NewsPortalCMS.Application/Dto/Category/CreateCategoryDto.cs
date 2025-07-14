using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.Dto.Category
{
    public record CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
