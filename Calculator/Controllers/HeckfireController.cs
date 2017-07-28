using System;
using System.Globalization;
using System.Web.Mvc;
using Calculator.Models;
using HeckFire;

namespace Calculator.Controllers
{
    public class HeckFireController : Controller
    {
        private readonly HeckFireQuestCalculator calculator;

        public HeckFireController()
        {
            this.calculator = new HeckFireQuestCalculator();
        }
        // GET: Result
        public ActionResult Main(MainModel model)
        {
            return View(model);
        }

        public ActionResult Calculate(MainModel model, string function)
        {
            string result;
            Quest quest;

            object tempResult;
            object tempList;

            if(TempData.TryGetValue("Result", out tempResult))
            {
                model.Result = (string) tempResult;
                TempData["Result"] = tempResult;
            }

            if(TempData.TryGetValue("QuestList", out tempList))
            {
                model.QuestList = (string) tempList;
                TempData["QuestList"] = tempList;
            }



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
                    result = calculator.GetTimeWhenNext(quest);
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

                    TempData["QuesTList"] = result;
                    model.QuestList = result;

                    return View("Main", model);

                default:
                    result = "Error";
                    break;
            }

            TempData["Result"] = result;
            model.Result = result;

            return View("Main",model);
        }
    }
}