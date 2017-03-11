using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class BlogPostsResponse
    {
        [JilDirective(Name = "articles")]
        public List<BlogPost> Articles { get; set; }
    }
}
