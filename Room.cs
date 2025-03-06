using System;

namespace DungeonExplorer
{
    public class Room
    {
        private readonly string _name;
        private readonly string _description;
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Enter()
        {
            Route[] destinations = Route.GetRoutes(this);
            Choice[] choices = new Choice[destinations.Length];
            foreach (Route route in destinations)
            {
                route.TryGetDestination(this, out Room destination);
                choices[0] = new Choice($"Go to {destination.Name}", () =>
                {
                    route.Take(this);
                    Game.DisplayTime(true);
                    destination.Enter();
                });
            }

            string nameText = $"You enter {Name}.";
            Menu roomMenu = new Menu(nameText+"\n"+Description+"\nWhat do you do?", choices);
            
            Console.Clear();
            Display.Write(nameText);
            Display.Write(Description);
            roomMenu.Open();
        }
    }
}