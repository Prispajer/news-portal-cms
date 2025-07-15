using FluentValidation;
using NewsPortalCMS.Application.Dto.Category;

namespace NewsPortalCMS.Application.Validators.Fluent.Category
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name is required");
        }
    }
}
