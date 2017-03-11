namespace DotLiquid.Extends.Filters
{
    public class HtmlFilters
    {
        public static string ScriptTag(string input)
        {
            return "<script src='" + input + "' type='text/javascript'></script>";
        }

        public static string StylesheetTag(string input)
        {
            return "<link href='" + input + "' rel='stylesheet' type='text/css' />";
        }

        public static string ImgTag(string input, string imgAlt = null, string imgClass = null)
        {
            if (imgAlt != null && imgClass != null)
                return "<img src='" + input + "' alt='" + imgAlt + "' class='" + imgClass + "' />";

            if (imgAlt != null)
                return "<img src='" + input + "' alt='" + imgAlt + "' />";

            if (imgClass != null)
                return "<img src='" + input + "' class='" + imgClass + "' />";

            return "<img src='" + input + "' />";
        }
    }
}
