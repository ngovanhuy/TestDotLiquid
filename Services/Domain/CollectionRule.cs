using Jil;

namespace Services.Domain
{
    public class CollectionRule
    {
        [JilDirective(Name = "collection_id")]
        public int CollectionId { get; set; }

        [JilDirective(Name = "column")]
        public string Column { get; set; }

        [JilDirective(Name = "relation")]
        public string Relation { get; set; }

        [JilDirective(Name = "condition")]
        public string Condition { get; set; }
    }
}
