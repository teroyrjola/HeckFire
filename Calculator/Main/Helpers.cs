using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Calculator.Models;

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

        public static string RemoveQuestsFromQuestList(string questListString, QuestFilters filters)
        {
            List<string> questListList = questListString.Split(new string[] { "\r\n", "\n", "<br/>" }, StringSplitOptions.None).ToList();

            string[] questsToBeFiltered = QuestFilters.ReturnFalseProperties(filters);

            for (int i = questListList.Count - 1; i >= 0; i--)
            {
                foreach (string quest in questsToBeFiltered)
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

        public static QuestFilters ChangeBoolean(QuestFilters filters, string questName)
        {
            if (filters.Construction && filters.MightGrowth && filters.MonsterSlaying &&
                filters.Researching && filters.ResourceGathering && filters.TroopTraining)
                switch (questName)
                {
                    case "Construction":
                        filters.TroopTraining = !filters.TroopTraining;
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        filters.ResourceGathering = !filters.ResourceGathering;
                        filters.Researching = !filters.Researching;
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                    case "Troop training":
                        filters.Construction = !filters.Construction;
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        filters.ResourceGathering = !filters.ResourceGathering;
                        filters.Researching = !filters.Researching;
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                    case "Monster slaying":
                        filters.Construction = !filters.Construction;
                        filters.TroopTraining = !filters.TroopTraining;
                        filters.ResourceGathering = !filters.ResourceGathering;
                        filters.Researching = !filters.Researching;
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                    case "Resource gathering":
                        filters.Construction = !filters.Construction;
                        filters.TroopTraining = !filters.TroopTraining;
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        filters.Researching = !filters.Researching;
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                    case "Researching":
                        filters.Construction = !filters.Construction;
                        filters.TroopTraining = !filters.TroopTraining;
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        filters.ResourceGathering = !filters.ResourceGathering;
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                    case "Might growth":
                        filters.Construction = !filters.Construction;
                        filters.TroopTraining = !filters.TroopTraining;
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        filters.ResourceGathering = !filters.ResourceGathering;
                        break;
                }

            else
                switch (questName)
                {
                    case "Construction":
                        filters.Construction = !filters.Construction;
                        break;
                    case "Troop training":
                        filters.TroopTraining = !filters.TroopTraining;
                        break;
                    case "Monster slaying":
                        filters.MonsterSlaying = !filters.MonsterSlaying;
                        break;
                    case "Resource gathering":
                        filters.ResourceGathering = !filters.ResourceGathering;
                        break;
                    case "Researching":
                        filters.Researching = !filters.Researching;
                        break;
                    case "Might growth":
                        filters.MightGrowth = !filters.MightGrowth;
                        break;
                }

            return filters;
        }

        public static string AddDate(string hoursWithQuests)
        {
            hoursWithQuests =  hoursWithQuests.Replace("00:05 - 01:00:", "-------------------------------------\n00:05 - 01:00:");
            return hoursWithQuests;
        }
    }
}