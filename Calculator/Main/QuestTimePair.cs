namespace HeckFire
{
    public class QuestTimePair
    {
        public string Time { get; set; }
        public Quest Quest { get; set; }

        public QuestTimePair(string time, Quest quest)
        {
            Time = time;
            Quest = quest;
        }
    }
}