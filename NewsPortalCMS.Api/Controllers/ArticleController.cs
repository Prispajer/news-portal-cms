using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Domain.Models;

namespace NewsPortalCMS.Api.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDetailDto>> CreateArticle([FromBody] CreateArticleDto createArticleDto)
    {
        var result = await _articleService.CreateArticleAsync(createArticleDto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return CreatedAtAction(nameof(GetArticle), new { articleId = result.Data!.Id }, result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleListDto>>> GetArticles([FromQuery] string? status)
    {
        var result = await _articleService.GetArticlesAsync(status);
        return Ok(result.Data);
    }

    [HttpGet("{articleId:guid}")]
    public async Task<ActionResult<ArticleDetailDto>> GetArticle(Guid articleId)
    {
        var result = await _articleService.GetArticleAsync(articleId);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result.Data);
    }

    [HttpPut("{articleId:guid}")]
    public async Task<ActionResult<ArticleDetailDto>> UpdateArticle(Guid articleId, [FromBody] UpdateArticleDto updateArticleDto)
    {
        var result = await _articleService.UpdateArticleAsync(articleId, updateArticleDto);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result.Data);
    }

    [HttpPost("{articleId:guid}/publish")]
    public async Task<ActionResult<ArticleDetailDto>> PublishArticle(Guid articleId)
    {
        var result = await _articleService.PublishArticleAsync(articleId);
        if (!result.IsSuccess)
            return NotFound(result.Message);
        return Ok(result.Data);
    }

    [HttpGet("/stats")]
    public async Task<ActionResult<ArticleStats>> GetStats()
    {
        var result = await _articleService.GetArticlesStatsAsync();
        return Ok(result.Data);
    }
}
