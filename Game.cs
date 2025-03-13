using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace DungeonExplorer
{
    /// <summary>
    /// Keep track of in-game time and instantiate everything necessary for the game to start and continue to function.
    /// This includes the player, and all rooms
    /// </summary>
    public static class Game
    {
        private static Player player;
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
        /// Tell the user they won the game and quit.
        /// </summary>
        public static void Win()
        {
            Display.Write($"You have successfully escaped Clocktown!");
            Display.Write($"The moon will not crash down on you, {player.Name}.");
            Display.Write("Well done!");
            Environment.Exit(0);
        }
    
        /// <summary>
        /// Prompt user to enter a name, until a valid one is entered.
        /// </summary>
        public static string GetName()
        {
            string name;
            while (true)
            {
                Console.Clear();
                Console.Write("What is your name?\n> ");
                name = Console.ReadLine();
                bool valid = name.All(Char.IsLetter) && !string.IsNullOrWhiteSpace(name);
                if (valid) break;
                Display.Write("Please only use letters.");
            }
            return name;
        }
        
        /// <summary>
        /// Create every object necessary for the game to run and traverse into the first room, passing control
        /// over to the player.
        /// </summary>
        public static void Start()
        {
            // Instantiate player
            player = new Player(GetName(), 100, 30);
            // Create observatory room, add required options to it
            Room observatory = new Room("Astral Observatory", "You walk through the door to be greeted " +
                                                        "by a huge telescope overlooking the night sky.");
            observatory.AddChoice("Read ominous sign", () =>
            {
                Display.Write("The sign reads: 'I have foreseen that the moon will fall " +
                    "from the sky. 3 days marks the calamity...'");
            });
            observatory.AddChoice("Look through telescope", () =>
            {
                Display.Write("You look through the telescope.");
                Display.Write("The moon looks bigger than usual...");
            });

            // Create East Clocktown, an empty room bridging others together
            Room east = new Room("East Clocktown", "The once bustling high-street is almost empty. " +
                                                   "The moon must have scared everyone away...");
            // Connect observatory and east
            new Route(observatory, east, 1);
            
            // Create North Clocktown and add enemy encounters, connect to east
            Room north = new Room("North Clocktown", "You walk through the gates to be greeted by " +
                                                     "empty carnival stalls, and a campfire; it seems somebody was\n" +
                                                     "camping here before it was overrun by monsters...");
            north.AddEnemy(new Ghoul());
            north.AddEnemy(new Shade());
            north.AddChoice("Rest at campfire", () =>
            {
                Display.Write("You sit down at the campfire, enjoying the temporary respite.");
                player.Heal();
            });
            new Route(east, north, 30);

            // Create South Clocktown, add dialogue option and key
            Room south = new Room("South Clocktown", "Rows of slum housing line the streets. This " +
                                                     "doesn't seem like a nice place to live, even if the moon's " +
                                                     "minions weren't everywhere.");
            new Route(east, south, 40);
            DialogueNode start = new DialogueNode("Sketchy Old Man", "This here key... it can take you " +
                                                                     "out of this damned place...");
            DialogueNode elaborate = new DialogueNode("Sketchy Old Man", "Ye' must take it te the " +
                                                                         "West, young'un! Now begone!");
            start.AddResponse("Uh, what does it open?", elaborate);
            start.AddChoice("Ignore him");
            Room shack = new Room("Rundown Shack", "You open the door to a broken-down shack. There's " +
                                                   "a shelf in the corner of the room. On it, lies a golden key.\n" +
                                                   "A suspicious old man glares at you from across the room.");
            shack.AddItem("Gate Key");
            shack.AddDialogue(start, "Talk to old man");
            new Route(south, shack, 1);
            Room west = new Room("West Clocktown", "The gates of Clocktown stand in front of you. " +
                                                   "This is where you can make your escape and save yourself\nfrom " +
                                                   "the moon.");

            // Create West Clocktown and method of winning
            west.AddChoice("Escape through the gate", () =>
            {
                if (player.OwnsItem("Gate Key"))
                {
                    Game.Win();
                };
                Display.Write("You don't have the key.");
            });
            west.AddItem("Rupee");
            new Route(south, west, 60);
            new Route(north, west, 45);

            // Enter the spawn room to begin the game
            observatory.Enter();
        }
    }
}