using Jil;
using System;

namespace Services.Domain
{
    public class ThemeAsset
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "theme_id")]
        public int ThemeId { get; set; }

        [JilDirective(Name = "key")]
        public string Key { get; set; }

        [JilDirective(Name = "source_key")]
        public string SourceKey { get; set; }

        [JilDirective(Name = "src")]
        public string Src { get; set; }

        [JilDirective(Name = "public_url")]
        public string PublicUrl { get; set; }

        [JilDirective(Name = "value")]
        public string Value { get; set; }

        [JilDirective(Name = "base64")]
        public string Base64 { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        [JilDirective(Name = "content_type")]
        public string ContentType { get; set; }

        [JilDirective(Name = "size")]
        public int Size { get; set; }
    }
}
