using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filter
{
    public class ProductFilter : PaginateFilter
    {
        public string Query { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public List<string> TagAliases { get; set; }
        public bool? Published { get; set; }
        public List<string> Tags { get; set; }
        public string Alias { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
        public int CollectionId { get; set; }
        public string SortBy { get; set; }
    }
}
