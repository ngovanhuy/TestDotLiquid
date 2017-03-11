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
    public class ProductService : BaseService
    {
        private IDbConnection connect = new SqlConnection(ConnectString);

        public ProductService()
        {
        }

        public List<Product> Filter(ProductFilter filter)
        {
            try
            {
                var products = new List<Product>();
                //string query = string.Format("select * from Products");
                //List<Product> products = connect.Query<Product>(query).ToList<Product>();
                //return products;

                var request = new RestRequest("/products", Method.GET);
                request.AddParameter("query", filter.Query);
                request.AddParameter("product_type", filter.Type);
                request.AddParameter("vendor", filter.Vendor);
                request.AddParameter("published", filter.Published);
                request.AddParameter("collection_id", filter.CollectionId);
                request.AddParameter("sort_by", filter.SortBy);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<ProductsResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    products = response.Data.Products;
                    return products;
                }
            }
            catch 
            {
            }
            return null;
        }
        public int FilterCount(ProductFilter filter)
        {
            try
            {
                var request = new RestRequest("/products/count", Method.GET);
                request.AddParameter("query", filter.Query);
                request.AddParameter("product_type", filter.Type);
                request.AddParameter("vendor", filter.Vendor);
                request.AddParameter("published", filter.Published);
                request.AddParameter("collection_id", filter.CollectionId);
                request.AddParameter("sort_by", filter.SortBy);
                request.AddParameter("page", filter.Page);
                request.AddParameter("limit", filter.Limit);
                var response = ApiClient.Execute<ProductsCountResponse>(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Data.Count;
                }
            }
            catch 
            {
            }
            return 0;
        }
    }
}