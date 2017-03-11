using Jil;

namespace Services.Response
{
    public class FilterCountResponse
    {
        [JilDirective(Name = "count")]
        public int Count { get; set; }
    }
}
