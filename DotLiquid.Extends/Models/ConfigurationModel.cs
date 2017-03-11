using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class ConfigurationModel : BaseModel
    {
        public string AssetsPath { get; set; }
        public string FilesPath { get; set; }
        public string CheckoutPath { get; set; }
        public string BaseDomain { get; set; }
        public string MediaDomain { get; set; }
        public string ThumbDomain { get; set; }
        public string MoneyFormat { get; set; }
        public string MoneyWithCurrencyFormat { get; set; }
        public string AssetsVersion { get; set; }
        public string GlobalVersion { get; set; }
        public bool InDesignMode { get; set; }
        public List<string> TemplateResource { get; set; }

        public ConfigurationModel()
        {
            TemplateResource = new List<string>();
        }
    }
}
