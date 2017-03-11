using Jil;
using Services.Domain;

namespace Services.Response
{
    public class StaticPageResponse
    {
        [JilDirective(Name = "page")]
        public StaticPage Page { get; set; }
    }
}
