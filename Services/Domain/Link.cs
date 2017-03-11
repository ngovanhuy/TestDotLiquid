using Jil;

namespace Services.Domain
{
    public class Link
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "title")]
        public string Title { get; set; }

        [JilDirective(Name = "active")]
        public bool Active { get; set; }

        [JilDirective(Name = "type")]
        public string Type { get; set; }

        [JilDirective(Name = "item_id")]
        public int? ItemId { get; set; }

        [JilDirective(Name = "tags")]
        public string Tags { get; set; }

        [JilDirective(Name = "url")]
        public string Url { get; set; }

        [JilDirective(Name = "position")]
        public int Position { get; set; }
    }
}
