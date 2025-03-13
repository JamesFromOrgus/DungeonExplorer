using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    /// <summary>
    /// Hold player's inventory and their current attributes such as health. Inherit from combatant, enabling the
    /// player to fight enemies.
    /// </summary>
    public class Player: Combatant
    {
        private List<string> _inventory = new List<string>();
        
        /// <summary>
        /// Set combatant properties.
        /// </summary>
        public Player(string name, int health, int damage) 
        {
            _name = name;
            _maxHealth = health;
            _health = _maxHealth;
            _damage = damage;
        }
        
        /// <summary>
        /// Override default behaviour of GetDecision, making it so player can make their own choice in combat.
        /// </summary>
        protected override CombatDecision GetDecision()
        {
            CombatDecision finalChoice = CombatDecision.Attack;
            Choice attackChoice = new Choice("Attack", () => finalChoice = CombatDecision.Attack);
            Choice defendChoice = new Choice("Defend", () => finalChoice = CombatDecision.Defend);
            Menu combatMenu = new Menu("What do you do?", new[] { attackChoice, defendChoice });
            combatMenu.Open();
            return finalChoice;
        }
        
        /// <summary>
        /// Add an item to player's inventory and display a message to inform them of the new item.
        /// </summary>
        public void PickUpItem(string item)
        {
            Display.Write($"You obtained '{item}'.");
            _inventory.Add(item);
        }
        
        /// <summary>
        /// Check whether player owns an item. Could be useful for checking if they have a key for a certain door etc.
        /// </summary>
        public bool OwnsItem(string item)
        {
            return _inventory.Contains(item);
        }
        
        /// <summary>
        /// Display the player's items in a digestible manner.
        /// </summary>
        public string InventoryContents()
        {
            return "- "+string.Join("\n- ", _inventory);
        }
    }
}