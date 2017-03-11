using Services.Domain;
using Services.Repository;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class ThemeModel : BaseModel
    {
        private Theme theme { get; set; }

        private Dictionary<string, object> _loadedModel;

        public ThemeModel()
        {
            _loadedModel = new Dictionary<string, object>();
        }
        public int Id
        {
            get
            {
                if (!_loadedModel.ContainsKey("theme"))
                {
                    var themeService = new ThemeService();
                    var theme = themeService.GetThemeActive();
                    _loadedModel.Add("theme", theme);
                    return theme.Id;
                }
                else
                {
                    var them = (Theme)(_loadedModel["theme"]);
                    if (theme == null)
                    {
                        return 0;
                    }
                    return theme.Id;
                }
            }
        }
        public string Name
        {
            get
            {
                if (!_loadedModel.ContainsKey("theme"))
                {
                    var themeService = new ThemeService();
                    var theme = themeService.GetThemeActive();
                    _loadedModel.Add("theme", theme);
                    return theme.Name;
                }
                else
                {
                    var theme = (Theme)(_loadedModel["theme"]);
                    if (theme == null)
                    {
                        return "";
                    }
                    return theme.Name;
                }
            }
        }
    }
}