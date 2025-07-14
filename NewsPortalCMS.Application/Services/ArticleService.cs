using AutoMapper;
using NewsPortalCMS.Application.Dto;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Validation;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper, ArticleBusinessValidator articleBusinessValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ArticleDetailDto>> CreateArticleAsync(CreateArticleDto createArticleDto)
        {
            try
            {
                var dbArticle = _mapper.Map<Article>(createArticleDto);
                dbArticle.Id = Guid.NewGuid();
                dbArticle.CreatedAt = DateTime.UtcNow;

                var articleDetailDto = _mapper.Map<ArticleDetailDto>(await _repository.CreateAsync(dbArticle));

                return Result<ArticleDetailDto>.Success(articleDetailDto, "Article created successfully.");
            }
            catch (Exception ex)
            {
                return Result<ArticleDetailDto>.Failure($"Error occurred while creating the article: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ArticleListDto>>> GetArticlesAsync(string? status)
        {
            try
            {
                ArticleStatus? articleStatus = null;

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<ArticleStatus>(status, true, out var parsedStatus))
                {
                    articleStatus = parsedStatus;
                }

                var articles = await _repository.GetAllAsync(articleStatus);

                var articlesDto = _mapper.Map<IEnumerable<ArticleListDto>>(articles);

                return Result<IEnumerable<ArticleListDto>>.Success(articlesDto, "Articles fetched successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ArticleListDto>>.Failure($"Error occurred while fetching articles: {ex.Message}");
            }
        }

        public async Task<Result<ArticleDetailDto>> GetArticleAsync(Guid id)
        {
            try
            {
                var dbArticle = await _repository.GetByIdAsync(id);
                var validationResult = ArticleBusinessValidator.ValidateArticleExists(dbArticle);

                if (!validationResult.IsSuccess)
                    return Result<ArticleDetailDto>.Failure(validationResult.Message);

                var articleDetailDto = _mapper.Map<ArticleDetailDto>(validationResult.Data);

                return Result<ArticleDetailDto>.Success(articleDetailDto, "Article found.");
            }
            catch (Exception ex)
            {
                return Result<ArticleDetailDto>.Failure($"Error occurred while fetching the article: {ex.Message}");
            }
        }

        public async Task<Result<ArticleDetailDto>> UpdateArticleAsync(Guid id, UpdateArticleDto updateArticleDto)
        {
            try
            {
                var dbArticle = await _repository.GetByIdAsync(id);
                var validationResult = ArticleBusinessValidator.ValidateArticleExists(dbArticle);

                if (!validationResult.IsSuccess)
                    return Result<ArticleDetailDto>.Failure(validationResult.Message);

                _mapper.Map(updateArticleDto, dbArticle);

                await _repository.UpdateAsync(dbArticle);

                var articleDetailDto = _mapper.Map<ArticleDetailDto>(dbArticle);
                return Result<ArticleDetailDto>.Success(articleDetailDto, "Article updated successfully.");
            }
            catch (Exception ex)
            {
                return Result<ArticleDetailDto>.Failure($"Error occurred while updating the article: {ex.Message}");
            }
        }

        public async Task<Result<ArticleDetailDto>> PublishArticleAsync(Guid id)
        {
            try
            {
                var article = await _repository.GetByIdAsync(id);
                if (article == null)
                    return Result<ArticleDetailDto>.Failure("Article not found.");

                article.Status = ArticleStatus.Published;

                await _repository.UpdateAsync(article);

                var publishedDto = _mapper.Map<ArticleDetailDto>(article);
                return Result<ArticleDetailDto>.Success(publishedDto, "Article published successfully.");
            }
            catch (Exception ex)
            {
                return Result<ArticleDetailDto>.Failure($"Error occurred while publishing the article: {ex.Message}");
            }
        }
    }
}
