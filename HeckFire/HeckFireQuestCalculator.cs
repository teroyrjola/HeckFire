using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;

namespace HeckFire
{
    class HeckFireQuestCalculator
    {
        public const int DefaultQuestTimePairArrayLength = 24;

        private static readonly DateTime KnownStartOfAQuestCycle =
            new DateTime(2017, 07, 26, 00, 00, 00);

        private static QuestTimePair[] HoursWithQuests;

        private static readonly Quest[] QuestCycle =
        {
            Quest.Construction, Quest.TroopTraining, Quest.MonsterSlaying, Quest.ResourceGathering,
            Quest.Researching, Quest.TroopTraining, Quest.MonsterSlaying, Quest.MightGrowth,
            Quest.ResourceGathering, Quest.TroopTraining, Quest.MonsterSlaying

            //Enum indexes:
            //0 = Construction
            //1 = Troop training
            //2 = Monster slaying
            //3 = Resource gathering
            //4 = Researching
            //5 = Might growth
        };

        private static readonly string[] Times =
        {
            "00", "01", "02", "03", "04", "05",
            "06", "07", "08", "09", "10", "11",
            "12", "13", "14", "15", "16", "17",
            "18", "19", "20", "21", "22", "23"
        };

        /// <summary>
        /// Initializes HoursWithQuests array with optional given hours, or the default value
        /// </summary>
        internal void InitializeQuestListForHours(int hours = DefaultQuestTimePairArrayLength)
        {
            int hoursFromKnownCycle = (DateTime.Now - KnownStartOfAQuestCycle).Hours;

            int offSetInCurrentCycle = hoursFromKnownCycle % QuestCycle.Length;

            HoursWithQuests = new QuestTimePair[hours +1];

            for (int i = 0; i < hours +1; i++)
            {
                int timeIndex = (i + hoursFromKnownCycle) % Times.Length;
                int questIndex = (i + offSetInCurrentCycle) % QuestCycle.Length;

                HoursWithQuests[i] = new QuestTimePair(Times[timeIndex], QuestCycle[questIndex]);
            }
        }

        internal string GetListOfTimesAndQuests()
        {
            if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

            var timesAndQuests = 
                
                HoursWithQuests.Select(pair => pair.Time)
                .Zip
                (HoursWithQuests.Select(pair => pair.Quest),
                
                (time, quest) => $"{time.PrettyQuestDuration()}: {quest.Name()}");

            string result = string.Join("\n", timesAndQuests);
            return result;
        }

        internal Quest GetCurrentQuest()
        {
            if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

            return HoursWithQuests[0].Quest;
        }

        internal Quest GetNextQuest()
            {
                if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

                return HoursWithQuests[1].Quest;
            }

        internal string GetTimeWhenNext(Quest quest)
        {
            if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

            return HoursWithQuests.Skip(1).FirstOrDefault(pair => pair.Quest.Equals(quest)).Time;   //Skip 1st element if the quest happens to be ongoing.
        }

        internal TimeSpan GetTimeUntilNext(Quest quest)
        {
            if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan questStartTime = TimeSpan.Parse(GetTimeWhenNext(quest) + ":05");

            return questStartTime.Subtract(currentTime);
        }

        internal Quest GetQuestAfterHours(double hrs)
        {
            if (HoursWithQuests.IsEmptyNullOrOld())
                InitializeQuestListForHours();

            if (HoursWithQuests.Length <= hrs)
                InitializeQuestListForHours((int)Math.Ceiling(hrs));

            return HoursWithQuests[(int) hrs].Quest;
        }
    }
}