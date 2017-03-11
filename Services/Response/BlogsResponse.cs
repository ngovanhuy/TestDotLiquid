using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class BlogsResponse
    {
        [JilDirective(Name = "blogs")]
        public List<Blog> Blogs { get; set; }
    }
}