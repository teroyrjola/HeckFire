using System;
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
        public ActionResult Main()
        {
            return View("Main");
        }
        
        public string CurrentQuest()
        {
            return calculator.GetCurrentQuest().Name();
        }

        public ActionResult NextQuest()
        {
            return Content("CalculationResult", new CalculationResult(calculator.GetNextQuest()).ResultString);
        }

        [HttpGet]
        public ActionResult GetTimeWhenNext(MainModel model)
        {
            int questId = 0;
            if (int.TryParse(model.QuestId, out questId)){

                if (questId < 0 || 5 < questId) return View("Error");

                Quest quest = (Quest) questId;


                return View("CalculationResult", new CalculationResult(calculator.GetTimeWhenNext(quest)));
            }

            return Content(model.QuestId);
        }
    }
}