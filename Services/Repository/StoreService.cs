using RestSharp;
using Services.Domain;
using Services.Response;
using System.Net;

namespace Services.Repository
{
    public class StoreService : BaseService
    {
        public Store GetStore()
        {
            Store store = null;

            var request = new RestRequest("/store/get_by_id", Method.GET);

            var response = ApiClient.Execute<StoreResponse>(request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    store = response.Data.Store;
                    break;
            }

            return store;
        }

    }
}
