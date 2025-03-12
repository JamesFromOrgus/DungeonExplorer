using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    /// <summary>
    /// This class acts as a node in a graph data structure, displaying room information and allowing the user to
    /// interact with the room and navigate to others
    /// </summary>
    public class Room
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        private List<Combatant> _enemies = new List<Combatant>();
        private List<string> _items = new List<string>();

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Allows for enemy to be added to room, meaning player will have to face them before entering.
        /// </summary>
        public void AddEnemy(Combatant enemy)
        {
            _enemies.Add(enemy);
        }
        /// <summary>
        /// Allows for item to be picked up in the room. Will show up in the same menu as navigation options.
        /// </summary>
        public void AddItem(string item)
        {
            _items.Add(item);
        }
        
        /// <summary>
        /// Removes item from room, and adds it to player inventory
        /// </summary>
        public void PickUpItem(string item)
        {
            if (!_items.Contains(item)) return;
            _items.Remove(item);
            Game.CurrentPlayer.PickUpItem(item);
        }

        /// <summary>
        /// Allows user to choose between available actions in the room: moving to other rooms, pick up items etc.
        /// </summary>
        public void ShowMenu()
        {
            Route[] destinations = Route.GetRoutes(this);
            List<Choice> choices = new List<Choice>();

            for (int i = 0; i < destinations.Length; i++)
            {
                Route route = destinations[i];
                route.TryGetDestination(this, out Room destination);
                choices.Add(new Choice($"Go to {destination.Name} (takes {route.TimeTaken} minutes)", () =>
                {
                    route.Take(this);
                    Game.DisplayTime(true);
                    destination.Enter();
                }));
            }

            foreach (string item in _items)
            {
                choices.Add(new Choice($"Pick up {item}", () =>
                {
                    PickUpItem(item);
                    ShowMenu();
                }));
            }

            string nameText = $"You enter {Name}.";
            Menu roomMenu = new Menu(nameText+"\n"+Description+"\nWhat do you do?", choices);
            roomMenu.Open();
        }
    
        /// <summary>
        /// Causes player to 'move' to the room, causing them to fight any enemies in the room before interacting with
        /// it
        /// </summary>
        public void Enter()
        {
            if (_enemies.Count > 0)
            {
                while (_enemies.Count > 0)
                {
                    Combatant enemy = _enemies[0];
                    Display.Write($"The path is blocked by {enemy.Name}.");
                    Game.CurrentPlayer.Fight(enemy);
                    if (Game.CurrentPlayer.Dead) Game.Over();
                    _enemies.RemoveAt(0);
                }
            }
            
            string nameText = $"You enter {Name}.";
            Console.Clear();
            Display.Write(nameText);
            Display.Write(Description);
            ShowMenu();
        }
    }
}