using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
    public class MainModel
    {
        [Display(Name = "Select quest:")]
        public Quest Quest { get; set; }
        [Display(Name = "Enter hours:")]
        public string Hours { get; set; }
        public string QuestId { get; set; }
        public string Result { get; set; }
    }
}