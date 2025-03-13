using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        /// <summary>
        /// Program entry-point: provides option to play the game or test the individual features.
        /// </summary>
        static void Main(string[] args)
        {
            Display.Write("These messages can be skipped using the 'enter' key.");
            Display.Write("This game features navigable menus. Use the arrow keys to select an option and enter to confirm.");
            Choice startChoice = new Choice("Play game", () =>
            {
                Game.Start();
            });
            Choice testChoice = new Choice("Testing", (() =>
            {
                new Testing().Test();
            }));
            new Menu("Dungeon Explorer!\nOptions:", new[] { startChoice, testChoice }).Open();
            // Console.WriteLine("Waiting for your Implementation");
            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
        }
    }
}
