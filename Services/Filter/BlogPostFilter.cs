using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filter
{
    public class BlogPostFilter : PaginateFilter
    {

        public string Title { get; set; }
        public bool? Published { get; set; }
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
        public List<string> TagAliases { get; set; }
        public string SortBy { get; set; }
    }
}
