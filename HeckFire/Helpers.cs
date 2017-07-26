using System;

namespace HeckFire
{
    public static class Helpers
    {
        public static string PrettyTimeSpan(this TimeSpan timespan)
        {
            return $"{timespan.Hours} hours, {timespan.Minutes} minutes and {timespan.Seconds} seconds.";
        }

        public static string PrettyStartTime(this string questStartTime)
        {
            return $"{questStartTime}:05";
        }

        public static string PrettyQuestDuration(this string questStartTime)
        {
            int nextHour = Convert.ToInt32(questStartTime) + 1;
            return $"{questStartTime}:05 - {nextHour:00}:00";
        }
    }
}