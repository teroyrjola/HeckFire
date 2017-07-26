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

            calculator.InitializeQuestListForHours();

            Console.WriteLine(
            calculator.GetListOfTimesAndQuests()
            );
            Console.ReadKey();

        }
    }
}
