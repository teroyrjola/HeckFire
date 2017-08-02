using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Calculator.Models
{
    public class QuestFilters
    {
        public bool Construction { get; set; }
        public bool TroopTraining { get; set; }
        public bool MonsterSlaying { get; set; }
        public bool ResourceGathering { get; set; }
        public bool Researching { get; set; }
        public bool MightGrowth { get; set; }

        public QuestFilters()
        {
            this.Construction = true;
            this.TroopTraining = true;
            this.MonsterSlaying = true;
            this.ResourceGathering = true;
            this.Researching = true;
            this.MightGrowth = true;
        }

        public static string[] ReturnFalseProperties(QuestFilters filters)
        {
            List<string> falseProperties = new List<string>();

            object moro = filters;

            foreach (PropertyInfo pi in moro.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(bool))
                {
                    bool booleanValue = (bool)pi.GetValue(filters);

                    if (!booleanValue) falseProperties.Add(pi.Name);
                }
            }

            ConvertQuestNamesToSentenceCase(falseProperties);

            return falseProperties.ToArray();
        }

        private static void ConvertQuestNamesToSentenceCase(List<string> falseProperties)
        {
            for (int i = 0; i < falseProperties.Count; i++)
            {
                switch (falseProperties[i])
                {
                    case "TroopTraining":
                        falseProperties[i] = "Troop training";
                        break;
                    case "MonsterSlaying":
                        falseProperties[i] = "Monster slaying";
                        break;
                    case "ResourceGathering":
                        falseProperties[i] = "Resource gathering";
                        break;
                    case "MightGrowth":
                        falseProperties[i] = "Might growth";
                        break;
                }
            }
        }
    }
}