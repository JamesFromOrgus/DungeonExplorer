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
            
        }
        public string InventoryContents()
        {
            return string.Join(", ", _inventory);
        }
    }
}