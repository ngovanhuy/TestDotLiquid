using AutoMapper;
using DotLiquid.Extends.Models;
using Services.Domain;

namespace TestLiquid.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<Blog, BlogModel>();
                cfg.CreateMap<BlogPost, ArticleModel>();
                cfg.CreateMap<Collection, CollectionModel>();
                cfg.CreateMap<Linklist, LinklistModel>();
                cfg.CreateMap<Link, LinkModel>().ForMember(x => x.BaseUrl, opt => opt.MapFrom(p => p.Url))
                            .ForMember(x => x.BaseType, opt => opt.MapFrom(p => p.Type));
                cfg.CreateMap<StaticPage, PageModel>();
            });
        }
    }
}