using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class LinklistsResponse
    {
        [JilDirective(Name = "linklists")]
        public List<Linklist> LinkLists { get; set; }
    }
}
