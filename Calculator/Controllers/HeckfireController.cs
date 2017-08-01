using System;
using System.Web.Mvc;
using Calculator.Models;
using HeckFire;
using Microsoft.Ajax.Utilities;

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
        public string Calculate(MainModel model, string function)
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
                    result = calculator.GetQuestAfterHours(model.Hours).Name();
                    break;

                case "GetQuestList":
                    int listLength = 0;
                    int.TryParse(model.QuestListLength, out listLength);

                    if (listLength > 24)
                        calculator.InitializeQuestListForHours(listLength);

                    result = calculator.GetListOfTimesAndQuests().Replace("\n", "<br/>");

                    return result;

                default:
                    result = "Error";
                    break;
            }

            return result;
        }

        [HttpPost]
        public string Filter(MainModel model, string filterButton)
        {
            model.QuestList = TempData["QuestList"]?.ToString() ?? model.QuestList;
            if (!filterButton.IsNullOrWhiteSpace()) {
                string s = Helpers.RemoveQuestsFromQuestList(model.QuestList, new string[] { filterButton });
                TempData["QuestList"] = s;
                return s;
            }
            else return model.QuestList;
        }
    }
}