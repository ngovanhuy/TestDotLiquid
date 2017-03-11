using RestSharp;

namespace Services.Repository
{
    public class BaseService
    {
        public RestClient ApiClient;
        public static string ConnectString = @"Data Source=192.168.13.248;Initial Catalog=BizwebStores;Persist Security Info=True;User ID=bizweb;Password=bizweb@dkt123;MultipleActiveResultSets=True;Application Name=EntityFramework";
        //public int CurrentThemeId
        //{
        //    get
        //    {

        //    }
        //}
        public BaseService()
        {
            ApiClient = new RestClient("http://192.168.13.245:8765/admin");
            ApiClient.AddDefaultHeader("Authorization", "Basic Yml6d2ViOnJ0UXFFOWNqR3EzRDNCZUoyR3FG");
            ApiClient.AddDefaultHeader("X-Bizweb-StoreId", "1");
        }
    }
}