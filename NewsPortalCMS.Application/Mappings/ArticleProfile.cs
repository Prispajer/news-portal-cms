using AutoMapper;
using NewsPortalCMS.Application.Dto.Article;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Article, ArticleDetailDto>();
            CreateMap<CreateArticleDto, Article>();
        }
    }
}
