using System;
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

        protected override CombatDecision GetDecision()
        {
            CombatDecision finalChoice = CombatDecision.Attack;
            Choice attackChoice = new Choice("Attack", () => finalChoice = CombatDecision.Attack);
            Choice defendChoice = new Choice("Defend", () => finalChoice = CombatDecision.Defend);
            Menu combatMenu = new Menu("What do you do?", new[] { attackChoice, defendChoice });
            combatMenu.Open();
            return finalChoice;
        }
        
        public void PickUpItem(string item)
        {
            Display.Write($"You obtained '{item}'.");
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