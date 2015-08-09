using System;

namespace StarterKit.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        public static TimeSpan TimeElapsedUtc(this DateTime startTime)
        {
            return DateTime.UtcNow.Subtract(startTime);
        }

        public static TimeSpan CalculateTimeElapsed(this DateTime startTime)
        {
            return DateTime.Now.Subtract(startTime);
        }

        public static double SecondsElapsedUtc(this DateTime startTime)
        {
            return startTime.TimeElapsedUtc().TotalSeconds;
        }

        public static double SecondsElapsed(this DateTime startTime)
        {
            return startTime.CalculateTimeElapsed().TotalSeconds;
        }
    }
}
