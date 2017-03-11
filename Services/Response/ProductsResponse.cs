using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class ProductsResponse
    {
        [JilDirective(Name = "products")]
        public List<Product> Products { get; set; }
    }
}