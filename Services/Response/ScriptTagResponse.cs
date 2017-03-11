using Jil;
using Services.Domain;

namespace Services.Response
{
    public class ScriptTagResponse
    {
        [JilDirective(Name = "script_tag")]
        public ScriptTag ScriptTag { get; set; }
    }
}
