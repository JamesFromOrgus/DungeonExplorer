using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public static class Statistics
    {
        const double Base = 1.5;
        public static int Kills { get; set; }
        public static int Level => CalculateLevel(Kills);
        public static void RegisterKill()
        {
            Kills++;
            if (Level > CalculateLevel(Kills-1)) Display.Write($"You have reached level {Level}.");
        }

        private static int CalculateLevel(int kills)
        {
            return (int)Math.Log(kills + 1, Base)+1;
        }

        public static void View()
        {
            Display.Write($"[{Game.CurrentPlayer.Name.ToUpper()}'S STATS]\n" +
                          $"You have killed {Kills} enemies.\n" +
                          $"You are currently level {Level}.\n" +
                          $"You have collected {Game.CurrentPlayer.Inventory.Count()} items.");
        }
    }
}
