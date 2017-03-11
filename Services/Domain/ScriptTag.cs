using Jil;
using System;

namespace Services.Domain
{
    public class ScriptTag
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "event")]
        public string Event { get; set; }

        [JilDirective(Name = "src")]
        public string Src { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }
    }
}
