using Jil;
using System;

namespace Services.Domain
{
    public class MediaFile
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "base64")]
        public string Base64 { get; set; }

        [JilDirective(Name = "file_name")]
        public string FileName { get; set; }

        [JilDirective(Name = "src")]
        public string Src { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        [JilDirective(Name = "path")]
        public string Path { get; set; }

        [JilDirective(Name = "extension")]
        public string Extension { get; set; }

        [JilDirective(Name = "name")]
        public string Name { get; set; }

        [JilDirective(Name = "type")]
        public string Type { get; set; }
    }
}
