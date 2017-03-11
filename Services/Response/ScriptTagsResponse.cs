using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public  class ScriptTagsResponse
    {
        [JilDirective(Name = "script_tags")]
        public List<ScriptTag> ScriptTags { get; set; }
    }
}
