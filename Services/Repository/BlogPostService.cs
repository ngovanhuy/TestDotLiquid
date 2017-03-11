using RestSharp;
using Services.Domain;
using Services.Filter;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class BlogPostService : BaseService
    {
        public List<BlogPost> Filter(BlogPostFilter filter)
        {
            try
            {
                var articles = new List<BlogPost>();

                var request = new RestRequest("/articles", Method.GET);
                request.AddParameter("title", filter.Title);
                request.AddParameter("published", filter.Published);
                request.AddParameter("author_id", filter.AuthorId);
                request.AddParameter("blog_id", filter.BlogId);
                request.AddParameter("sort_by", filter.SortBy);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<BlogPostsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    articles = response.Data.Articles;
                    return articles;
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
