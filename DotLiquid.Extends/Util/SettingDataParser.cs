using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DotLiquid.Extends.Util
{
    public class SettingDataParser
    {
        public static SettingData Deserialize(string json)
        {
            var settingData = new SettingData();

            try
            {
                var jsonSettingsData = JObject.Parse(json);

                settingData.Current = GetSettingsOfCurrent(jsonSettingsData);
                settingData.Presets = GetSettingsOfPresets(jsonSettingsData);
            }
            catch
            {
            }

            return settingData;
        }

        private static Settings GetSettingsOfCurrent(JObject jsonSettingsData)
        {
            var currentSettings = new Settings();

            var jsonCurrentSettings = (JObject)jsonSettingsData.SelectToken("current");

            if (jsonCurrentSettings != null)
                currentSettings = ConvertJsonToSettings(jsonCurrentSettings);

            return currentSettings;
        }

        private static Settings ConvertJsonToSettings(JObject jsonSettings)
        {
            var settings = new Settings();

            foreach (var jsonSetting in jsonSettings.Children())
            {
                string settingName = ((JProperty)jsonSetting).Name;
                object settingValue = GetSettingValue((JProperty)jsonSetting);

                settings.Add(settingName, settingValue);
            }

            return settings;
        }

        private static object GetSettingValue(JProperty jsonSetting)
        {
            object value = null;

            if (jsonSetting.Value is JValue)
                value = ((JValue)jsonSetting.Value).Value;

            return value;
        }

        private static SettingPresets GetSettingsOfPresets(JObject jsonSettingsData)
        {
            var settingPresets = new SettingPresets();

            var jsonPresets = (JObject)jsonSettingsData.SelectToken("presets");
            if (jsonPresets != null)
                settingPresets = ConvertJsonToSettingPresets(jsonPresets);

            return settingPresets;
        }

        private static SettingPresets ConvertJsonToSettingPresets(JObject jsonPresets)
        {
            var presets = new SettingPresets();

            foreach (var jsonPreset in jsonPresets.Children())
            {
                var presetProperty = (JProperty)jsonPreset;
                string presetName = presetProperty.Name;
                var presetValue = ConvertJsonToSettings((JObject)presetProperty.Value);

                presets.Add(presetName, presetValue);
            }

            return presets;
        }

        public static string Serialize(SettingData settingData)
        {
            var jsonSettingData = new JObject();

            try
            {
                var jsonPresets = ConvertSettingPresetsToJson(settingData.Presets);
                var jsonCurrentSettings = ConvertSettingsToJson(settingData.Current);

                jsonSettingData.Add(new JProperty("presets", jsonPresets));
                jsonSettingData.Add(new JProperty("current", jsonCurrentSettings));
            }
            catch
            {
            }

            return jsonSettingData.ToString();
        }

        private static JObject ConvertSettingPresetsToJson(SettingPresets settingPresets)
        {
            var jsonPresets = new JObject();

            foreach (var settingPreset in settingPresets)
            {
                var jsonSetting = ConvertSettingsToJson(settingPreset.Value);
                var jsonSettingPreset = new JProperty(settingPreset.Key, jsonSetting);

                jsonPresets.Add(jsonSettingPreset);
            }

            return jsonPresets;
        }

        private static JObject ConvertSettingsToJson(Settings settings)
        {
            var jsonSettings = new JObject();

            foreach (var setting in settings)
            {
                var jsonSetting = new JProperty(setting.Key, new JValue(setting.Value));

                jsonSettings.Add(jsonSetting);
            }

            return jsonSettings;
        }
    }
    public class SettingData
    {
        public SettingData()
        {
            Presets = new SettingPresets();
            Current = new Settings();
        }

        public SettingPresets Presets { get; set; }

        public Settings Current { get; set; }

        /// <summary>
        /// Change the Setting in "Current". If it doesn't exist, the setting will be added to SettingData.Current.
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="settingValue"></param>
        public void ChangeSettingInCurrent(string settingName, object settingValue)
        {
            if (Current.ContainsKey(settingName))
                Current[settingName] = settingValue;
            else
                Current.Add(settingName, settingValue);
        }
    }
    public class Settings : Dictionary<string, object>
    {
        public Settings() : base() { }

        public Settings(Dictionary<string, object> settings) : base(settings) { }

        /// <summary>
        /// Normalize all Setting's values if the value if wrong.
        /// </summary>
        public void NormalizeSettingValues()
        {
            var stringArrayKeys = new List<string>();

            foreach (var key in Keys)
            {
                if (this[key] is string[])
                    stringArrayKeys.Add(key);
            }

            foreach (var invalidKey in stringArrayKeys)
            {
                var array = (string[])this[invalidKey];
                if (array.Length == 1)
                    this[invalidKey] = array[0];
            }
        }
    }
    public class SettingPresets : Dictionary<string, Settings> { }

}