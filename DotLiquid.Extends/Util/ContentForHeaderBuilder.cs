using Newtonsoft.Json;
using Services.Filter;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotLiquid.Extends.Util
{
    public class ContentForHeaderBuilder
    {
        private readonly HttpContext _httpContext;

        public ContentForHeaderBuilder()
        {
            _httpContext = HttpContext.Current;
        }

        public string Render()
        {
            var extraHeaderBuilder = new StringBuilder();
            extraHeaderBuilder.AppendLine(BuildScriptTagsHeader());

            return extraHeaderBuilder.ToString();
        }

        private string GetPermanentDomain()
        {
            return ".bizwebvietnam.net";
        }

        private string BuildScriptTagsHeader()
        {
            var scriptTagsBuilder = new StringBuilder();
            scriptTagsBuilder.AppendLine("<script>");
            scriptTagsBuilder.AppendLine("(function() {");
            scriptTagsBuilder.AppendLine("function asyncLoad() {");
            scriptTagsBuilder.Append("var urls = ");
            scriptTagsBuilder.Append(JsonConvert.SerializeObject(GetOnloadScriptTags()));
            scriptTagsBuilder.Append(";");
            scriptTagsBuilder.AppendLine();
            scriptTagsBuilder.AppendLine("for (var i = 0; i < urls.length; i++) {");
            scriptTagsBuilder.AppendLine("var s = document.createElement('script');");
            scriptTagsBuilder.AppendLine("s.type = 'text/javascript';");
            scriptTagsBuilder.AppendLine("s.async = true;");
            scriptTagsBuilder.AppendLine("s.src = urls[i];");
            scriptTagsBuilder.AppendLine("s.src = urls[i];");
            scriptTagsBuilder.AppendLine("var x = document.getElementsByTagName('script')[0];");
            scriptTagsBuilder.AppendLine("x.parentNode.insertBefore(s, x);");
            scriptTagsBuilder.AppendLine("}");
            scriptTagsBuilder.AppendLine("}");
            scriptTagsBuilder.AppendLine("window.attachEvent ? window.attachEvent('onload', asyncLoad) : window.addEventListener('load', asyncLoad, false);");
            scriptTagsBuilder.AppendLine("})();");
            scriptTagsBuilder.AppendLine("</script>");

            return scriptTagsBuilder.ToString();
        }

        private List<string> GetOnloadScriptTags()
        {
            var _scriptTagService = new ScriptTagService();
            var scriptTags = _scriptTagService.Filter(new ScriptTagFilter { Limit = 0, Page = 0 });
            foreach (var scriptTag in scriptTags)
            {
                scriptTag.Src += (scriptTag.Src.Contains("?") ? "&" : "?") + "store=" + GetPermanentDomain();
            }

            return scriptTags.Where(s => s.Event.ToLower() == "onload").Select(s => s.Src).ToList();
        }
    }

}
