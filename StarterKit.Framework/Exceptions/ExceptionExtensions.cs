using System;
using System.Collections.Generic;

namespace StarterKit.Framework.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string ExceptionPropertyPrefix = "mx";
        public static string ExceptionReferenceIdKey = "referenceid";

        public static Guid GetReferenceId(this Exception exception)
        {
            var propertyValue = exception.GetCustomProperty(ExceptionReferenceIdKey);
            if (propertyValue != null)
            {
                return (Guid) propertyValue;
            }

            return Guid.Empty;
        }

        public static bool HasReferenceId(this Exception exception)
        {
            return exception.HasCustomProperty(ExceptionReferenceIdKey);
        }

        public static Guid AddReferenceId(this Exception exception)
        {
            var refernceId = exception.GetReferenceId();
            if (refernceId.Equals(Guid.Empty))
            {
                refernceId = Guid.NewGuid();

                exception.AddCustomProperty(ExceptionReferenceIdKey, refernceId);
            }

            return refernceId;
        }

        public static bool HasCustomProperty(this Exception exception, string propertyName)
        {
            var key = GetKey(propertyName);

            return exception.Data.Contains(key);
        }

        public static void AddCustomProperty(this Exception exception, string propertyName, object value)
        {
            var key = GetKey(propertyName);

            exception.Data[key] = value;
        }

        public static object GetCustomProperty(this Exception exception, string propertyName)
        {
            var key = GetKey(propertyName);

            return exception.Data.Contains(key) ? exception.Data[key] : null;
        }

        public static IDictionary<string, object> GetCustomProperties(this Exception exception)
        {
            var properties = new Dictionary<string, object>();
            foreach (var key in exception.Data.Keys)
            {
                var keyName = key.ToString().ToLowerInvariant();
                if (keyName.StartsWith("ls:"))
                {
                    var propertyName = keyName.Remove(0, 3).Trim().ToLowerInvariant();

                    properties[propertyName] = exception.Data[key];
                }
            }

            return properties;
        }

        private static string GetKey(string propertyName)
        {
            return string.Format("{0}:{1}", ExceptionPropertyPrefix, propertyName).ToLowerInvariant();
        }
    }
}
