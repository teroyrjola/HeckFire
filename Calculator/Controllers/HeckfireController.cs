using System;
using System.Web.Mvc;
using Calculator.Models;
using HeckFire;

namespace Calculator.Controllers
{
    public class HeckFireController : Controller
    {
        private readonly HeckFireQuestCalculator calculator;
        private static readonly MainModel mainModel = new MainModel();

        public HeckFireController()
        {
            this.calculator = new HeckFireQuestCalculator();
        }

        public ActionResult Main(MainModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Calculate(MainModel model, string function)
        {
            string result;
            Quest quest;

            switch (function)
            {
                case "GetCurrentQuest":
                    result = calculator.GetCurrentQuest().Name();
                    break;

                case "GetNextQuest":
                    result = calculator.GetNextQuest().Name();
                    break;

                case "GetTimeWhenNext":
                    quest = model.Quest;
                    result = calculator.GetTimeWhenNext(quest).PrettyStartTime();
                    break;

                case "GetTimeUntilNext":
                    quest = model.Quest;
                    result = calculator.GetTimeUntilNext(quest).PrettyTimeSpan();
                    break;

                case "GetQuestAfterHours":
                    result = calculator.GetQuestAfterHours(Convert.ToDouble(model.Hours)).Name();
                    break;

                case "GetQuestList":
                    int listLength = Convert.ToInt32(model.QuestListLength);

                    if (listLength > 24)
                        calculator.InitializeQuestListForHours(listLength);

                    result = calculator.GetListOfTimesAndQuests();

                    mainModel.QuestList = result;
                    return View("Main", mainModel);

                default:
                    result = "Error";
                    break;
            }

            mainModel.Result = result;
            return View("Main", mainModel);
        }
    }
}