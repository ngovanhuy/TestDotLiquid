using RestSharp;
using Services.Domain;
using Services.Response;
using System.Collections.Generic;
using System.Net;

namespace Services.Repository
{
    public class LinkedListService : BaseService
    {
        public List<Linklist> GetAll()
        {
            try
            {
                var products = new List<Linklist>();

                var request = new RestRequest("/linklists", Method.GET);
                var response = ApiClient.Execute<LinklistsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    products = response.Data.LinkLists;
                    return products;
                }
            }
            catch
            {
            }
            return null;
        }

        public Linklist GetByAlias(string alias)
        {
            Linklist blog = null;

            var request = new RestRequest("/linklists/get_by_alias", Method.GET);
            request.AddParameter("alias", alias);

            var response = ApiClient.Execute<LinklistResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    blog = response.Data.Linklist;
                    break;
            }

            return blog;
        }
        public Linklist GetById(int linklistId)
        {
            Linklist collection = null;

            var request = new RestRequest("/linklists/{linklistId}", Method.GET);
            request.AddUrlSegment("linklistId", linklistId.ToString());

            var response = ApiClient.Execute<LinklistResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    collection = response.Data.Linklist;
                    break;
            }

            return collection;
        }
    }
}
