using DotLiquid.ViewEngine;
using System.Web.Mvc;
using System.Web.Routing;
using TestLiquid.App_Start;

namespace TestLiquid
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Add(new DotLiquidViewEngine());
            LiquidRegistStartupTask.Execute();
            //ViewEngines.Engines.Add(new MyViewEngine());
            AutoMapperConfig.RegisterMappings();
        }
    }
}
