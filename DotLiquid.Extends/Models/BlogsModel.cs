using AutoMapper;
using Services.Filter;
using Services.Repository;
using System.Collections;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class BlogsModel : BaseModel, IEnumerable
    {
        private BlogService blogService = new BlogService();
        private Dictionary<string, object> _loadedModel;

        public BlogsModel()
        {
            _loadedModel = new Dictionary<string, object>();
        }

        /// <summary>
        /// Return list of object when call by liquid code in view: "{% for object in objects %}".
        /// Example: "{% for collection in collections %}".
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            if (!_loadedModel.ContainsKey("blogs"))
            {
                var blogFilter = new BlogFilter
                {
                    Limit = 10,
                    Page = 1
                };
                var blogs = blogService.Filter(blogFilter);
                var blogModels = Mapper.Map<List<BlogModel>>(blogs);
                _loadedModel.Add("blogs", blogModels);
                return blogModels.GetEnumerator();
            }
            return ((List<ProductModel>)_loadedModel["blogs"]).GetEnumerator();
        }

        /// <summary>
        /// Return Single Object of EnumerableModel.
        /// Liquid View Engine's Syntax: "{{ EnumerableModel.(SingleKey) }}".
        /// </summary>
        /// <param name="singleKey"></param>
        /// <returns></returns>
        public override object BeforeMethod(string singleKey)
        {
            var single = LoadSingleObject(singleKey);

            if (single != null)
                return single;

            return null;
        }

        protected BlogModel LoadSingleObject(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                var blogEntity = blogService.GetByAlias(alias);
                if (blogEntity != null)
                    return Mapper.Map<BlogModel>(blogEntity);

                if (alias.ToLower() == BlogModel.ALL_ARTICLE_BLOGS_ALIAS)
                {
                    return new BlogModel
                    {
                        Id = BlogModel.ALL_ARTICLE_BLOGS_ID,
                        Alias = BlogModel.ALL_ARTICLE_BLOGS_ALIAS,
                        Name = "Tất cả tin tức"
                    };
                }
            }

            return null;
        }
    }
}