using DotLiquid.Extends.Util;
using Services.Repository;
using System.Collections.Generic;

namespace DotLiquid.Extends.Models
{
    public class SettingsModel : BaseModel
    {
        public const string SETTING_FILE_PATH = "configs/settings_data.json";
        private bool _isLoadedSettings;
        private Dictionary<string, object> _loadedSettings;

        public SettingsModel()
        {
            _loadedSettings = new Dictionary<string, object>();
        }

        public override object ToLiquid()
        {
            if (!_isLoadedSettings)
            {
                var settingData = LoadSetting();
                if (settingData != null)
                {
                    _loadedSettings = settingData.Current;
                }

                _isLoadedSettings = true;
            }

            return _loadedSettings;
        }

        //public override object BeforeMethod(string singleKey)
        //{
        //    var single = LoadSingleObject(singleKey);

        //    if (single != null)
        //        return single;

        //    return null;
        //}

        //protected object LoadSingleObject(string alias)
        //{
        //    var settingData = LoadSetting();
        //    return settingData.Current[alias];
        //}

        public SettingData LoadSetting()
        {
            if (!_isLoadedSettings)
            {
                var settings = new SettingsModel();
                var themeAssetService = new ThemeAssetService();
                var settingContent = themeAssetService.GetContent(SETTING_FILE_PATH);
                var settingData = SettingDataParser.Deserialize(settingContent);

                _loadedSettings.Add("settings", settingData);
                _isLoadedSettings = true;
                return settingData;
            }
            else
            {
                return (SettingData)_loadedSettings["settings"];
            }

        }
    }
}