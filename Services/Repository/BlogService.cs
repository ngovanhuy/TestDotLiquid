using RestSharp;
using Services.Domain;
using Services.Filter;
using Services.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace Services.Repository
{
    public class BlogService : BaseService
    {
        public BlogService()
        {
        }

        public List<Blog> Filter(BlogFilter filter)
        {
            try
            {
                var products = new List<Blog>();

                var request = new RestRequest("/blogs", Method.GET);
                request.AddParameter("name", filter.Name);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<BlogsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    products = response.Data.Blogs;
                    return products;
                }
            }
            catch
            {
            }
            return null;
        }

        public Blog GetByAlias(string alias)
        {
            Blog blog = null;

            var request = new RestRequest("/blogs/get_by_alias", Method.GET);
            request.AddParameter("alias", alias);

            var response = ApiClient.Execute<BlogResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    blog = response.Data.Blog;
                    break;
            }

            return blog;
        }
    }
}