public enum Quest
{
    Construction,
    TroopTraining,
    MonsterSlaying,
    ResourceGathering,
    Researching,
    MightGrowth,
}

namespace HeckFire
{

    public static class QuestInfo
    {
        public static string Name(this Quest quest)
        {
            switch (quest)
            {
                case Quest.Construction: return "Construction";
                case Quest.TroopTraining: return "Troop training";
                case Quest.MonsterSlaying: return "Monster slaying";
                case Quest.ResourceGathering: return "Resource gathering";
                case Quest.Researching: return "Researching";
                case Quest.MightGrowth: return "Might growth";
                default: return "Error";
            }
        }
    }
}