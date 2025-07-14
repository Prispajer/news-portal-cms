using FluentValidation;
using NewsPortalCMS.Application.Dto.Article;

namespace NewsPortalCMS.Api.Validators.Article
{
    public class CreateArticleDtoValidator : AbstractValidator<CreateArticleDto>
    {
        public CreateArticleDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters long");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
        }
    }
}
