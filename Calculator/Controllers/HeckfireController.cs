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

                default:
                    result = "Error";
                    break;
            }

            return result;
        }

        [HttpGet]
        public string GetQuestList(MainModel model)
        {
            int listLength = 0;
            int.TryParse(model.QuestListLength, out listLength);

            if (listLength > 24)
                calculator.InitializeQuestListForHours(listLength);

            mainModel.QuestListLength = listLength < 24 ? "24" : listLength.ToString();

            string result = calculator.GetListOfTimesAndQuests().Replace("\n", "<br/>");

            string filteredResult = Helpers.RemoveQuestsFromQuestList(result, mainModel.QuestFilters);
            mainModel.QuestList = filteredResult;

            return filteredResult;
        }

        [HttpGet]
        public string Filter(string filterButton)
        {
            if (!filterButton.IsNullOrWhiteSpace())
            {
                if (mainModel.QuestList.Contains(filterButton) == false) {                                  //If the questlist is filtered
                    calculator.InitializeQuestListForHours(Convert.ToInt32(mainModel.QuestListLength));     //and doesn't contain the currently
                    mainModel.QuestList = calculator.GetListOfTimesAndQuests();                             //clicked quest, repopulate.
                }

                mainModel.QuestFilters = Helpers.ChangeBoolean(mainModel.QuestFilters, filterButton);
            }
            return Helpers.RemoveQuestsFromQuestList(mainModel.QuestList, mainModel.QuestFilters);

        }
    }
}