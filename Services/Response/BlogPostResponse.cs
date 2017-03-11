using Jil;
using Services.Domain;

namespace Services.Response
{
    public class BlogPostResponse
    {
        [JilDirective(Name = "article")]
        public BlogPost Article { get; set; }
    }
}
