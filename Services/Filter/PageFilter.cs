using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filter
{
    public class PageFilter: PaginateFilter
    {
        public string Title { get; set; }
        public bool? Published { get; set; }
    }
}
