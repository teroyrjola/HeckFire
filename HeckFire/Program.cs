using System;
using System.Text;
using System.Threading.Tasks;
using static HeckFire.HeckFireQuestCalculator;

namespace HeckFire
{
    class Program
    {
        static void Main(string[] args)
        {
            HeckFireQuestCalculator calculator = new HeckFireQuestCalculator();
            calculator.InitializeQuestListForHours(24);

            Console.WriteLine(calculator.GetListOfTimesAndQuests());

            Console.WriteLine(calculator.GetCurrentQuest().Name());

            Console.WriteLine(calculator.GetNextQuest().Name());

            //Console.WriteLine(calculator.GetQuestAfterHours(4).Name());

            //Console.WriteLine(calculator.GetTimeWhenNext(Quest.MonsterSlaying).PrettyStartTime());

            //Console.WriteLine(calculator.GetTimeUntil(Quest.MonsterSlaying).PrettyTimeSpan());

            Console.ReadKey();
        }
    }
}
