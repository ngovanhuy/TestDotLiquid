using Jil;
using Services.Domain;
using System.Collections.Generic;

namespace Services.Response
{
    public class ThemesResponse
    {
        [JilDirective(Name = "themes")]
        public List<Theme> Themes { get; set; }
    }
}
