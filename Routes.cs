using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a path between two nodes in the room graph. Can be unidirectional or bidirectional.
    /// Also, can take a certain amount of time to traverse.
    /// </summary>
    public class Route
    {
        static readonly List<Route> Routes = new List<Route>();
        private readonly Room _origin;
        private readonly Room _destination;
        private readonly int _timeTaken = 0;
        private readonly bool _oneWay = false;
        public int TimeTaken
        {
            get => _timeTaken;
        }

        public Route(Room origin, Room destination, int timeTaken, bool oneWay = false)
        {
            _origin = origin;
            _destination = destination;
            _timeTaken = timeTaken;
            _oneWay = oneWay;
            Routes.Add(this);
        }

        public bool TryGetDestination(Room room, out Room destination)
        {
            if (room == _origin)
            {
                destination = _destination;
                return true;
            }
            if (room == _destination && !_oneWay)
            {
                destination = _origin;
                return true;
            }
            destination = null;
            return false;
        }

        public Room Take(Room origin)
        {
            TryGetDestination(origin, out Room destination);
            Game.ElapseTime(_timeTaken);
            return destination;
        }

        public static Route[] GetRoutes(Room room)
        {
            List<Route> destinations = new List<Route>();
            foreach (Route route in Routes)
            {
                if (route.TryGetDestination(room, out Room destination))
                {
                    destinations.Add(route);
                }
            }
            return destinations.ToArray();
        }
    }
}