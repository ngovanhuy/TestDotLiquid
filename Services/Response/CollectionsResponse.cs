using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class CollectionsResponse
    {
        [JilDirective(Name = "collections")]
        public List<Collection> Collections { get; set; }
    }
}
