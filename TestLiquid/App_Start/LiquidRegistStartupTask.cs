using DotLiquid;
using DotLiquid.Extends.Filters;

namespace TestLiquid.App_Start
{
    public class LiquidRegistStartupTask
    {
        public static void Execute()
        {
            Template.RegisterFilter(typeof(ArrayFilters));
            Template.RegisterFilter(typeof(HtmlFilters));
            Template.RegisterFilter(typeof(MathFilters));
            Template.RegisterFilter(typeof(StringFilters));
            Template.RegisterFilter(typeof(UrlFilters));
        }
    }
}