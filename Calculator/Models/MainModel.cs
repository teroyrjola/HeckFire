using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
    public class MainModel
    {
        [Display(Name = "Select quest: ")]
        public Quest Quest { get; set; }

        [Display(Name = "Enter hours: ")]
        public string Hours { get; set; }
        [Display(Name = "Enter length of a quest list: ")]
        public string QuestListLength { get; set; }
        public string QuestList { get; set; }

        public string Result { get; set; }
    }
}