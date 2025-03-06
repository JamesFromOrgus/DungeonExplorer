using System;
using System.Collections.Generic;
using System.Media;

namespace DungeonExplorer
{
    public static class Game
    {
        private static Player player;
        private static Room currentRoom;
        private static int _minutesRemaining = 72 * 60;

        public static bool ElapseTime(int minutes)
        {
            _minutesRemaining = Math.Max(_minutesRemaining - minutes, 0);
            return _minutesRemaining == 0;
        }

        public static void DisplayTime(bool showMinutes = false)
        {
            if (showMinutes)
            {
                Display.Write($"You have {_minutesRemaining / 60} hours and " +
                              $"{_minutesRemaining % 60} minutes remaining.");
                return;
            }
            Display.Write($"You have {_minutesRemaining / 60} hours remaining.");
        }
        
        public static void Start()
        {
            // Menu choice = new Menu("Pick a word:", new[]
            // {
            //     new Choice("Option A", () =>
            //     {
            //         Console.WriteLine("ok you picked option A");
            //         Console.WriteLine("Multiline function lol");
            //     }, ConsoleColor.Red),
            //     new Choice( "Option B", () => Console.WriteLine("ok you picked option B"))
            // });
            // choice.Open();
            //
            // DialogueNode ping = new DialogueNode("The guy", "Ping");
            // DialogueNode nextNode = new DialogueNode("The guy", "I'm glad you wanted to hear more! 'ping' is an expression relating to the popular sport, Table Tennis.\n" +
            //     "It is commonly referred to as 'Ping-Pong', hence why 'pong' was provided as an option.\nUs going back and forth with 'ping' and 'pong' is reminiscent of\n" +
            //     "a ping-pong ball oscillating from one side of the table to another.");
            // ping.AddResponse("Pong", ping);
            // ping.AddResponse("Please elaborate.", nextNode);
            // ping.AddChoice("Leave");
            // nextNode.AddResponse("Interesting, let's continue.", ping);
            //
            // ping.Display();

            Display.Write("Here is a test. I am gonna fill this up with words so it takes long enough to write to the" +
                          "\nthe screen as I would like to test some things such as skipping the typewriting effect.");

            Room room1 = new Room("Astral Observatory", "You walk through the door to be greeted" +
                                                        "by a huge telescope overlooking the night sky.");
            Room room2 = new Room("North Clocktown", "As you come through the gate you notice a" +
                                                     " sizeable balloon hovering in the sky.");
            new Route(room1, room2, 10);
            room1.Enter();

            // Change the playing logic into true and populate the while loop
            bool playing = false;
            while (playing)
            {
                // Code your playing logic here
            }
        }
    }
}