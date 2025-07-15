using AutoMapper;
using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Validators.Business;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Domain.Models;
using NewsPortalCMS.Domain.Services;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace NewsPortalCMS.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ISlugService _slugService;
        public ArticleService(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            ISlugService slugService)
        {
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _mapper = mapper;
            _slugService = slugService;
        }

        public async Task<Result<ArticleDetailDto>> CreateArticleAsync(CreateArticleDto createArticleDto)
        {
            var dbCategory = await _categoryRepository.GetByIdAsync(createArticleDto.CategoryId);

            var categoryValidation = CategoryBusinessValidator.ValidateCategoryExists(dbCategory);
            if (!categoryValidation.IsSuccess)
                return Result<ArticleDetailDto>.Failure(categoryValidation.Message);

            var dbArticle = _mapper.Map<Article>(createArticleDto);
            dbArticle.Id = Guid.NewGuid();
            dbArticle.CreatedAt = DateTime.UtcNow;
            dbArticle.Slug = await _slugService.GenerateUniqueSlugAsync(dbArticle.Title);

            var articleDetailDto = _mapper.Map<ArticleDetailDto>(await _articleRepository.CreateAsync(dbArticle));

            return Result<ArticleDetailDto>.Success(articleDetailDto, "Article created successfully");
        }

        public async Task<Result<ArticleStats>> GetArticlesStatsAsync()
        {
            var dbArticles = await _articleRepository.GetAllAsync();

            var generatedArticlesStats = ArticleStatsService.GenerateArticleStatsAsync(dbArticles);

            return Result<ArticleStats>.Success(generatedArticlesStats, "Article statistics fetched successfully");
        }

        public async Task<Result<IEnumerable<ArticleListDto>>> GetArticlesAsync(string? status)
        {
            ArticleStatus? articleStatus = null;

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ArticleStatus>(status, true, out var parsedStatus))
            {
                articleStatus = parsedStatus;
            }

            var dbArticles = await _articleRepository.GetAllAsync(articleStatus);

            var articleListDto = _mapper.Map<IEnumerable<ArticleListDto>>(dbArticles);

            return Result<IEnumerable<ArticleListDto>>.Success(articleListDto, "Articles fetched successfully");
        }

        public async Task<Result<ArticleDetailDto>> GetArticleAsync(Guid id)
        {

            var dbArticle = await _articleRepository.GetByIdAsync(id);

            var articleValidation = ArticleBusinessValidator.ValidateArticleExists(dbArticle);
            if (!articleValidation.IsSuccess)
                return Result<ArticleDetailDto>.Failure(articleValidation.Message);

            var articleDetailDto = _mapper.Map<ArticleDetailDto>(articleValidation.Data);

            return Result<ArticleDetailDto>.Success(articleDetailDto, "Article retrieved successfully");
        }

        public async Task<Result<ArticleDetailDto>> UpdateArticleAsync(Guid id, UpdateArticleDto updateArticleDto)
        {
            var dbArticle = await _articleRepository.GetByIdAsync(id);

            var articleValidation = ArticleBusinessValidator.ValidateArticleExists(dbArticle);
            if (!articleValidation.IsSuccess)
                return Result<ArticleDetailDto>.Failure(articleValidation.Message);

            var dbCategory = await _categoryRepository.GetByIdAsync(updateArticleDto.CategoryId!.Value);

            var categoryValidation = CategoryBusinessValidator.ValidateCategoryExists(dbCategory);
            if (!categoryValidation.IsSuccess)
                return Result<ArticleDetailDto>.Failure(categoryValidation.Message);

            _mapper.Map(updateArticleDto, dbArticle);

            if (dbArticle!.Title != updateArticleDto.Title)
            {
                dbArticle.Slug = await _slugService.GenerateUniqueSlugAsync(updateArticleDto.Title!);
            }

            await _articleRepository.UpdateAsync(dbArticle);

            var articleDetailDto = _mapper.Map<ArticleDetailDto>(dbArticle);
            return Result<ArticleDetailDto>.Success(articleDetailDto, "Article updated successfully");
        }

        public async Task<Result<ArticleDetailDto>> PublishArticleAsync(Guid id)
        {
            var dbArticle = await _articleRepository.GetByIdAsync(id);
            var articleValidation = ArticleBusinessValidator.ValidateArticleExists(dbArticle);

            if (!articleValidation.IsSuccess)
                return Result<ArticleDetailDto>.Failure(articleValidation.Message);

            articleValidation.Data!.Status = ArticleStatus.Published;
            await _articleRepository.UpdateAsync(articleValidation.Data);

            var articleDetailDto = _mapper.Map<ArticleDetailDto>(articleValidation.Data);
            return Result<ArticleDetailDto>.Success(articleDetailDto, "Article published successfully");
        }
    }
}
