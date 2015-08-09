using System;
using System.Linq;
using System.Net;

namespace StarterKit.Framework.Extensions
{
    public static class NetworkExtensions
    {
        public static IPAddress ToIpAddress(this string value)
        {
            IPAddress ipAddress;

            if (IPAddress.TryParse(value, out ipAddress))
            {
                return ipAddress;
                
            }

            return null;
        }

        public static string PadIPv4Octects(this string value, int padding)
        {
            var octects = value.Split('.');
            if (octects.Length < 4)
            {
                for (var i = 0; i < 3 - octects.Length; i++)
                {
                    value += "." + padding;
                }
            }

            return value;
        }

        public static uint ToUInt32(this IPAddress ip)
        {
            var bytes = ip.GetAddressBytes().Reverse().ToArray();
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
