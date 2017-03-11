using Jil;
using System;

namespace Services.Domain
{
    public class Theme
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "name")]
        public string Name { get; set; }

        [JilDirective(Name = "role")]
        public string Role { get; set; }

        [JilDirective(Name = "previewable")]
        public bool Previewable { get; set; }

        [JilDirective(Name = "processing")]
        public bool Processing { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }
    }
}
