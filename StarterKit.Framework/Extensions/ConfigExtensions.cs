using System.Configuration;

namespace StarterKit.Framework.Extensions
{
    public static class ConfigExtensions
    {
        public static string GetAppSettingsStringValue(string name, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[name];
            return !string.IsNullOrWhiteSpace(value)
                ? value
                : defaultValue;
        }

        public static bool GetAppSettingsBooleanValue(string name, bool defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[name];
            return configValue.ToBoolean(defaultValue);
        }

        public static int GetAppSettingsIntValue(string name, int defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[name];
            return configValue.ToInt32(defaultValue);
        }

        public static decimal GetAppSettingsDecimalValue(string name, decimal defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[name];
            return configValue.ToDecimal(defaultValue);
        }
    }
}
