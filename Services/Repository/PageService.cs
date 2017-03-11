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
    public class PageService : BaseService
    {
        public List<StaticPage> Filter(PageFilter filter)
        {
            try
            {
                var pages = new List<StaticPage>();

                var request = new RestRequest("/pages", Method.GET);
                request.AddParameter("title", filter.Title);
                request.AddParameter("visibility", filter.Published);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<StaticPagesResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    pages = response.Data.Pages;
                    return pages;
                }
            }
            catch
            {
            }
            return null;
        }

        public StaticPage GetByAlias(string alias)
        {
            StaticPage page = null;

            var request = new RestRequest("/pages/get_by_alias", Method.GET);
            request.AddParameter("alias", alias);

            var response = ApiClient.Execute<StaticPageResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    page = response.Data.Page;
                    break;
            }

            return page;
        }

        public StaticPage GetById(int collectionId)
        {
            StaticPage page = null;

            var request = new RestRequest("/pages/{pageId}", Method.GET);
            request.AddUrlSegment("pageId", collectionId.ToString());

            var response = ApiClient.Execute<StaticPageResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    page = response.Data.Page;
                    break;
            }

            return page;
        }
    }
}
