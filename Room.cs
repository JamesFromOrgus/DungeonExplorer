namespace DungeonExplorer
{
    public class Room
    {
        private readonly string _description;

        public Room(string description)
        {
            _description = description;
        }

        public string GetDescription()
        {
            return _description;
        }
    }
}