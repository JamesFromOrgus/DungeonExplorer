using System;
using System.Collections.Generic;
using System.Media;

namespace DungeonExplorer
{
    /// <summary>
    /// Keep track of in-game time and instantiate everything necessary for the game to start and continue to function.
    /// This includes the player, and all rooms
    /// </summary>
    public static class Game
    {
        private static Player player = new Player("Link", 100, 30);
        private static Room currentRoom;
        private static int _minutesRemaining = 72 * 60;
        
        public static Player CurrentPlayer { get => player; }

        /// <summary>
        /// Skip through in-game time, meaning the player is closer to running out and failing.
        /// </summary>
        public static bool ElapseTime(int minutes)
        {
            _minutesRemaining = Math.Max(_minutesRemaining - minutes, 0);
            return _minutesRemaining == 0;
        }

        /// <summary>
        /// Display how much in-game time the player has to beat the game. If time is at zero, game over.
        /// </summary>
        public static void DisplayTime(bool showMinutes = false)
        {
            if (showMinutes)
            {
                Display.Write($"You have {_minutesRemaining / 60} hours and " +
                              $"{_minutesRemaining % 60} minutes remaining.");
                return;
            }
            Display.Write($"You have {_minutesRemaining / 60} hours remaining.");
            if (_minutesRemaining == 0) Over();
        }

        /// <summary>
        /// Tell the user they failed to beat the game and quit.
        /// </summary>
        public static void Over()
        {
            Display.Write($"Game over. You have failed...");
            Environment.Exit(0);
        }
        
        /// <summary>
        /// Create every object necessary for the game to run and traverse into the first room, passing control
        /// over to the player.
        /// </summary>
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
            Room test = new Room("South Clocktown", "testing");
            test.AddEnemy(new Ghoul());
            test.AddEnemy(new Shade());
            test.AddItem("Ocarina of Time");
            new Route(room1, room2, 10, true);
            new Route(room1, test, 10);
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