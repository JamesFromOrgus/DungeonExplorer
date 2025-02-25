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

            DialogueNode ping = new DialogueNode("Ping");
            DialogueNode nextNode = new DialogueNode("I'm glad you wanted to hear more! 'ping' is an expression relating to the popular sport, Table Tennis.\n" +
                "It is commonly referred to as 'Ping-Pong', hence why 'pong' was provided as an option.\nUs going back and forth with 'ping' and 'pong' is reminiscent of\n" +
                "a ping-pong ball oscillating from one side of the table to another.");
            ping.AddResponse("Pong", ping);
            ping.AddResponse("Please elaborate.", nextNode);
            ping.AddChoice("Leave");
            nextNode.AddResponse("Interesting, let's continue.", ping);

            ping.Display();

            // Change the playing logic into true and populate the while loop
            bool playing = false;
            while (playing)
            {
                // Code your playing logic here
            }
        }
    }
}