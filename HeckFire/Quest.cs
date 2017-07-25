namespace HeckFire
{

    public static class QuestInfo
    {
        public static string Name(this Quest quest)
        {
            switch (quest)
            {
                case Quest.MonsterSlaying: return "Monster slaying";
                case Quest.MightGrowth: return "Might growth";
                case Quest.ResourceGathering: return "Resource gathering";
                case Quest.TroopTraining: return "Troop training";
                case Quest.Construction: return "Construction";
                case Quest.Researching: return "Researching";
                default: return "Error";
            }
        }
    }

    public enum Quest
    {
        MonsterSlaying, MightGrowth, ResourceGathering, TroopTraining, Construction, Researching
    }
}