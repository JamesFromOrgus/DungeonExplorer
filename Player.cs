using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player: Combatant
    {
        private List<string> _inventory = new List<string>();

        public Player(string name, int health, int damage) 
        {
            _name = name;
            _maxHealth = health;
            _health = _maxHealth;
            _damage = damage;
        }
        public void PickUpItem(string item)
        {
            _inventory.Add(item);
        }

        public bool OwnsItem(string item)
        {
            return _inventory.Contains(item);
        }
        
        public string InventoryContents()
        {
            return string.Join(", ", _inventory);
        }
    }
}