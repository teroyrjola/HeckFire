using System.ComponentModel.DataAnnotations;
using HeckFire;
using Microsoft.Ajax.Utilities;

namespace Calculator.Models
{
    public class MainModel
    {
        [Display(Name = "Select quest: ")]
        public Quest Quest { get; set; }

        [Display(Name = "Enter hours: ")]
        public string Hours { get; set; }
        [Display(Name = "Enter hours of a quest list: ")]
        public string QuestListLength { get; set; }

        private string questList;

        public string QuestList
        {
            get
            {
                if (questList.IsNullOrWhiteSpace()) return HeckFireQuestCalculator.StaticGetListOfTimesAndQuests();
                return questList;
            }
            set { questList = value; }
        }

        public string Result { get; set; }
        private QuestFilters questFilters;
        [Display(Name = "Filters:")]
        public QuestFilters QuestFilters
        {
            get
            {
                if (questFilters == null) return new QuestFilters();
                return questFilters;
            }
            set { questFilters = value; }
        }
    }
}
