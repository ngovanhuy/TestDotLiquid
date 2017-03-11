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
    public class ScriptTagService : BaseService
    {
        public List<ScriptTag> Filter(ScriptTagFilter filter)
        {
            var scriptTags = new List<ScriptTag>();

            var request = new RestRequest("/script_tags", Method.GET);
            request.AddParameter("src", filter.Src);
            request.AddParameter("page", filter.Page);
            request.AddParameter("limit", filter.Limit);

            var response = ApiClient.Execute<ScriptTagsResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    scriptTags = response.Data.ScriptTags;
                    break;
                default:
                    break;
            }

            return scriptTags;
        }

        public int FilterCount(ScriptTagFilter filter)
        {
            var count = 0;

            var request = new RestRequest("/script_tags/count", Method.GET);
            request.AddParameter("src", filter.Src);

            var response = ApiClient.Execute<FilterCountResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    count = response.Data.Count;
                    break;
                default: break;
            }

            return count;
        }

        public ScriptTag GetById(int id, int storeId)
        {
            ScriptTag scriptTag = null;

            var request = new RestRequest("/script_tags/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());

            var response = ApiClient.Execute<ScriptTagResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    scriptTag = response.Data.ScriptTag;
                    break;
                default: break;
            }

            return scriptTag;
        }
    }
}
