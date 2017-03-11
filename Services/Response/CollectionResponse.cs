using Jil;
using Services.Domain;

namespace Services.Response
{
    public class CollectionResponse
    {
        [JilDirective(Name = "collection")]
        public Collection Collection { get; set; }
    }
}
