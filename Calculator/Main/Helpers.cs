using System;
using System.Collections.Generic;
using System.Linq;

namespace HeckFire
{
    public static class Helpers
    {
        public static string PrettyTimeSpan(this TimeSpan timespan)
        {
            if (timespan.Hours > 0)
                return $"{timespan.Hours} hours, {timespan.Minutes} minutes and {timespan.Seconds} seconds.";

            if (timespan.Minutes > 0)
                return $"{timespan.Minutes} minutes and {timespan.Seconds} seconds.";

            if (timespan.Seconds > 0)
                return $"{timespan.Seconds} seconds.";

            return "Now.";
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

        public static bool IsInvalid(this QuestTimePair[] questTimeArray)
        {
            return questTimeArray == null || questTimeArray.Length < 12 ||
                   questTimeArray[0].Time != DateTime.UtcNow.AddHours(3).Hour.ToString("00");
        }

        public static string RemoveQuestsFromQuestList(string questListString, string[] questsToFilter)
        {
            List<string> questListList = questListString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

            for (int i = questListList.Count - 1; i >= 0; i--)
            {
                foreach (string quest in questsToFilter)
                {
                    if (questListList[i].Contains(quest))
                    {
                        questListList.RemoveAt(i);
                        break;
                    }
                }
            }

            return String.Join("\n", questListList);
        }
    }
}