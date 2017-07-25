using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HeckFire
{
    class HeckFireQuestCalculator
    {
        private static Dictionary<string, Quest> HoursWithQuests;

        private static readonly Quest[] Quests = {
            (Quest)0, (Quest)1, (Quest)2, (Quest)3, //0 = Monster slaying
            (Quest)4, (Quest)5, (Quest)0, (Quest)1, //1 = Might growth
            (Quest)2, (Quest)3, (Quest)4, (Quest)5, //2 = Resource gathering
            (Quest)0, (Quest)1, (Quest)2, (Quest)3, //3 = Troop training
            (Quest)4, (Quest)5, (Quest)0, (Quest)1, //4 = Construction
            (Quest)2, (Quest)3, (Quest)4, (Quest)5  //5 = Researching
        };

        private static readonly string[] Times = {
            "00", "01", "02", "03", "04", "05",
            "06", "07", "08", "09", "10", "11",
            "12", "13", "14", "15", "16", "17",
            "18", "19", "20", "21", "22", "23"
        };

        internal void InitializeHoursWithQuestsDictionary()
        {
            HoursWithQuests = new Dictionary<string, Quest>();

            if (Quests.Length != Times.Length) throw new ArgumentException("Quests and Times need to be the same length!");

            for (int i = 0; i < Quests.Length; i++)
            {
                HoursWithQuests.Add(Times[i], Quests[i]);
            }
        }

        //internal Func<string> GetListOfTimesAndQuests = ()
        //    => $"{string.Join(" ", Times.Zip(Quests, (time, quest) => $"{time}: {quest.Name()}").ToArray())}";

        internal string GetListOfTimesAndQuests()
        {
            var timesAndQuests = Times.Zip(Quests, (time, quest) => $"{time.PrettyQuestDuration()}: {quest.Name()}");
            string result = string.Join("\n", timesAndQuests);
            return result;
        }

        internal Quest GetCurrentQuest()
        {
            Quest currentQuest;
            string currentHour = DateTime.Now.Hour.ToString("00");

            if (HoursWithQuests.TryGetValue(currentHour, out currentQuest))
                return currentQuest;

            throw new Exception($"Quest not found for time {currentHour}!");
        }

        internal Quest GetNextQuest()
        {
            Quest nextQuest;
            string nextHour = (DateTime.Now.Hour + 1).ToString("00");

            if (HoursWithQuests.TryGetValue(nextHour, out nextQuest))
                return nextQuest;

            throw new Exception($"Quest not found for time {nextHour}!");
        }

        internal string GetTimeWhenNext(Quest quest)
        {
            int currentHour = DateTime.Now.Hour;

            int[] questTimes = HoursWithQuests.Where(q => q.Value == quest)
                .Select(q => Convert.ToInt32(q.Key))
                .ToArray();

            return questTimes.Where(q => q > currentHour)
                .DefaultIfEmpty(questTimes[0]).First().ToString("00");
        }

        internal TimeSpan GetTimeUntil(Quest quest)
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan nextQuestTime = TimeSpan.Parse(GetTimeWhenNext(quest) + ":05");

            if (nextQuestTime < currentTime) nextQuestTime += TimeSpan.FromHours(24);
            TimeSpan difference = nextQuestTime - currentTime;

            return difference;
        }

        internal Quest GetQuestAfterHours(double hrs)
        {
            double hoursToMove = hrs;

            if (hoursToMove < 0)
            {             
                hoursToMove %= Times.Length;
                hoursToMove += Times.Length;
            }
            else hoursToMove %= Times.Length;

            TimeSpan queryTimeSpan = TimeSpan.FromHours(hoursToMove);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            int resultHour = currentTime.Add(queryTimeSpan).Hours;

            string keyHourString = resultHour.ToString("00");

           return HoursWithQuests[keyHourString];
        }
    }
}