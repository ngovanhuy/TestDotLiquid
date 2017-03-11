using Jil;
using System.Collections.Generic;

namespace Services.Domain
{
    public class Linklist
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "title")]
        public string Title { get; set; }

        [JilDirective(Name = "alias")]
        public string Alias { get; set; }

        [JilDirective(Name = "links")]
        public List<Link> Links { get; set; }
    }
}
