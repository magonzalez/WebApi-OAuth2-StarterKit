namespace StarterKit.Framework.Extensions
{
    public static class StringExtensions
    {
        public static bool ToBoolean(this string strValue, bool defaultValue)
        {
            if (strValue == null)
                return defaultValue;

            bool value;

            return bool.TryParse(strValue, out value)
                ? value
                : defaultValue;
        }
        public static int ToInt32(this string strValue, int defaultValue)
        {
            if (strValue == null)
                return defaultValue;

            int value;

            return int.TryParse(strValue, out value)
                ? value
                : defaultValue;
        }

        public static decimal ToDecimal(this string strValue, decimal defaultValue)
        {
            if (strValue == null)
                return defaultValue;

            decimal value;

            return decimal.TryParse(strValue, out value)
                ? value
                : defaultValue;
        }
    }
}
