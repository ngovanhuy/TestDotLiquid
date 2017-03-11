using Jil;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Response
{
    public class ThemeResponse
    {
        [JilDirective(Name = "theme")]
        public Theme Theme { get; set; }
    }
}
