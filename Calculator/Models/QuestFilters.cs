using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Calculator.Models
{
    public class QuestFilters : Dictionary<string, bool>
    {
        public static Dictionary<string, bool> filters = new Dictionary<string, bool>();

        public QuestFilters()
        {
            this.Add("Construction", true);
            this.Add("Troop training", true);
            this.Add("Monster slaying", true);
            this.Add("Resource gathering", true);
            this.Add("Researching", true);
            this.Add("Might growth", true);
        }
    }
}