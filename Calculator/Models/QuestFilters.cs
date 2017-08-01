using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
    public class QuestFilters
    {
        public bool Construction;
        public bool TroopTraining;
        public bool MonsterSlaying;
        public bool ResourceGathering;
        public bool Researching;
        public bool MightGrowth;

        public QuestFilters()
        {
            this.Construction = true;
            this.TroopTraining = true;
            this.MonsterSlaying = true;
            this.ResourceGathering = true;
            this.Researching = true;
            this.MightGrowth = true;
        }
    }
}