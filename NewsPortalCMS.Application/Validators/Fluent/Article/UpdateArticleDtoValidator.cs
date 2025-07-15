using FluentValidation;
using NewsPortalCMS.Application.Dto.Article;

namespace NewsPortalCMS.Application.Validators.Fluent.Article
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator()
        {
            When(x => x.Title is not null, () =>
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title cannot be empty if provided");
            });

            When(x => x.Content is not null, () =>
            {
                RuleFor(x => x.Content)
                    .MinimumLength(10).WithMessage("Content must be at least 10 characters long");
            });

            When(x => x.CategoryId is not null, () =>
            {
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category ID cannot be empty");
            });
        }
    }
}
