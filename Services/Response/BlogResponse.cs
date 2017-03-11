using Jil;
using Services.Domain;

namespace Services.Response
{
    public class BlogResponse
    {
        [JilDirective(Name = "blog")]
        public Blog Blog { get; set; }
    }
}
