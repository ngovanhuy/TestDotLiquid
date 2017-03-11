using Jil;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Response
{
    class ThemeAssetsResponse
    {
        [JilDirective(Name = "assets")]
        public List<ThemeAsset> Assets { get; set; }
    }
}
