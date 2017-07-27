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
            return View("Main", model);
        }

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
                    result = calculator.GetTimeWhenNext(quest);
                    break;

                case "GetTimeUntilNext":
                    quest = model.Quest;
                    result = calculator.GetTimeUntilNext(quest).PrettyTimeSpan();
                    break;

                case "GetQuestAfterHours":
                    result = calculator.GetQuestAfterHours(Convert.ToDouble(model.Hours)).Name();
                    break;

                default:
                    result = "Error";
                    break;
            }

            model.Result = result;

            return View("Main", model);
        }

        public ActionResult Result()
        {
            return View();
        }
    }
}