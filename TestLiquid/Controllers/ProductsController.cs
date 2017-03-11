using Services.Filter;
using Services.Repository;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TestLiquid.Controllers
{
    public class ProductsController : BaseController
    {
        private ProductService productService = new ProductService();
        // GET: Products
        public ActionResult Index()
        {
            var liquidContext = new Dictionary<string, object>();
            var productFilter = new ProductFilter
            {
                Page = 1,
                Limit = 10
            };
            var products = productService.Filter(productFilter);
            liquidContext.Add("store", "hung");
            return View(liquidContext);
        }
    }
}