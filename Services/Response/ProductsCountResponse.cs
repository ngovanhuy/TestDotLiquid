using Jil;

namespace Services.Response
{
    public class ProductsCountResponse
    {
        [JilDirective(Name = "count")]
        public int Count { get; set; }
    }
}