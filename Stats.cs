using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Keep track of kills and player level.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Base of the logarithmic function which describes the level progression.
        /// </summary>
        const double Base = 1.5;
        public static int Kills { get; set; }
        public static int Level => CalculateLevel(Kills);
        public static void RegisterKill()
        {
            Kills++;
            if (Level > CalculateLevel(Kills-1)) Display.Write($"You have reached level {Level}.");
        }

        /// <summary>
        /// Function that calculates level given kills.
        /// </summary>
        private static int CalculateLevel(int kills)
        {
            return (int)Math.Log(kills + 1, Base)+1;
        }

        /// <summary>
        /// Display stats to the player.
        /// </summary>
        public static void View()
        {
            Display.Write($"[{Game.CurrentPlayer.Name.ToUpper()}'S STATS]\n" +
                          $"You have killed {Kills} enemies.\n" +
                          $"You are currently level {Level}.\n" +
                          $"You have collected {Game.CurrentPlayer.Inventory.Count()} items.");
        }
    }
}
