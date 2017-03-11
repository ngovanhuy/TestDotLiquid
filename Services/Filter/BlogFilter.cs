using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filter
{
    public class BlogFilter : PaginateFilter
    {
        public string Name { get; set; }
        public List<int> BlogIds { get; set; }
    }
}
