using AutoMapper;
using Services.Filter;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class ArticlesModel : BaseModel, IEnumerable
    {
        private BlogModel blogModel;
        private BlogPostService blogService = new BlogPostService();
        private Dictionary<string, object> _loadedModel;

        public ArticlesModel(BlogModel blogModel)
        {
            this.blogModel = blogModel;
            _loadedModel = new Dictionary<string, object>();
        }

        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("articles"))
            {
                var blogPostFilter = new BlogPostFilter
                {
                    Page = 1,
                    Limit = 10,
                    BlogId = blogModel.Id
                };
                var articles = blogService.Filter(blogPostFilter);
                var articlesModel = Mapper.Map<List<ArticleModel>>(articles);
                _loadedModel.Add("articles", articlesModel);
                return articlesModel.GetEnumerator();
            }
            return ((List<ProductModel>)_loadedModel["articles"]).GetEnumerator();
        }
    }
}