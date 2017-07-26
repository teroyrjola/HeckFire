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
        public ActionResult CurrentQuest()
        {
            return View("CalculationResult", new CalculationResult(calculator.GetCurrentQuest()));
        }

        public ActionResult NextQuest()
        {
            return View("CalculationResult", new CalculationResult(calculator.GetNextQuest()));
        }

        public ActionResult GetTimeWhenNext(string questNumber)
        {
            int questId = 0;
            if (int.TryParse(questNumber, out questId)){

                if (questId < 0 || 5 < questId) return View("Error");

                Quest quest = (Quest) questId;

                return View("CalculationResult", new CalculationResult(calculator.GetTimeWhenNext(quest)));
            }

            return Content(questNumber);
        }
    }
}