using FluentValidation.TestHelper;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Application.Validators.Fluent.Article;

public class CreateArticleDtoValidatorTests
{
    private readonly CreateArticleDtoValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_TitleIsEmpty()
    {
        var model = new CreateArticleDto
        {
            Title = "",
            Content = "Zawartość przykładowa",
            Author = "Autor",
            CategoryId = Guid.NewGuid()
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
