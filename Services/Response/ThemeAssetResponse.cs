using Jil;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Response
{
   public class ThemeAssetResponse
    {
        [JilDirective(Name = "asset")]
        public ThemeAsset Asset { get; set; }
    }
}
