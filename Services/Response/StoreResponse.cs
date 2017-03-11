using Jil;
using Services.Domain;

namespace Services.Response
{
    public class StoreResponse
    {
        [JilDirective(Name = "store")]
        public Store Store { get; set; }
    }
}
