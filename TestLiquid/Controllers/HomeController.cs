using System.Web.Mvc;

namespace TestLiquid.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewKey = "templates/index.bwt";
            return View(ViewKey);
        }
    }
}