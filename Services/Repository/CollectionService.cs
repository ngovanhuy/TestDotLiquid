using RestSharp;
using Services.Domain;
using Services.Filter;
using Services.Response;
using System.Collections.Generic;
using System.Net;

namespace Services.Repository
{
    public class CollectionService : BaseService
    {
        public List<Collection> Filter(CollectionFilter filter)
        {
            try
            {
                var collections = new List<Collection>();

                var request = new RestRequest("/collections", Method.GET);
                request.AddParameter("product_id", filter.ProductId);
                request.AddParameter("name", filter.Name);
                request.AddParameter("collection_type", filter.CollectionType);
                request.AddParameter("published", filter.Published);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<CollectionsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    collections = response.Data.Collections;
                    return collections;
                }
            }
            catch
            {
            }
            return null;
        }

        public Collection GetByAlias(string alias)
        {
            Collection collection = null;

            var request = new RestRequest("/collections/get_by_alias", Method.GET);
            request.AddParameter("alias", alias);

            var response = ApiClient.Execute<CollectionResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    collection = response.Data.Collection;
                    break;
            }

            return collection;
        }

        public Collection GetById(int collectionId)
        {
            Collection collection = null;

            var request = new RestRequest("/collections/{collectionId}", Method.GET);
            request.AddUrlSegment("collectionId", collectionId.ToString());

            var response = ApiClient.Execute<CollectionResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    collection = response.Data.Collection;
                    break;
            }

            return collection;
        }
    }
}
