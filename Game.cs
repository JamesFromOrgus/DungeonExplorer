using System;
using System.Collections.Generic;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialize the game with one room and one player

        }
        public void Start()
        {
            Menu choice = new Menu("Pick a word:", new[]
            {
                new Choice("Option A", () =>
                {
                    Console.WriteLine("ok you picked option A");
                    Console.WriteLine("Multiline function lol");
                }, ConsoleColor.Red),
                new Choice( "Option B", () => Console.WriteLine("ok you picked option B"))
            });
            choice.Open();
            // Change the playing logic into true and populate the while loop
            bool playing = false;
            while (playing)
            {
                // Code your playing logic here
            }
        }
    }
}