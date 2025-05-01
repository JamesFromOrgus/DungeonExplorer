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
        private Inventory _inventory;
        public Inventory Inventory => _inventory;
        
        /// <summary>
        /// Set combatant properties.
        /// </summary>
        public Player(string name, int health) : base(name, health)
        {
            _inventory = new Inventory();
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
        public void PickUpItem(string item, bool silence = false)
        {
            if (!silence) Display.Write($"You obtained '{item}'.");
            _inventory.AddItem(item, 1);
        }
        
        public void PickUpItem(string item, int amount, bool silence = false)
        {
            if (!silence) Display.Write($"You obtained {amount}x '{item}'.");
            _inventory.AddItem(item, amount);
        }
        
        public void RemoveItem(string item, bool silence = false)
        {
            if (!silence) Display.Write($"You lost '{item}'.");
            _inventory.RemoveItem(item, 1);
        }
        
        public void RemoveItem(string item, int amount, bool silence = false)
        {
            if (!silence) Display.Write($"You lost {amount}x '{item}'.");
            _inventory.RemoveItem(item, amount);
        }
        
        /// <summary>
        /// Check whether player owns an item. Could be useful for checking if they have a key for a certain door etc.
        /// </summary>
        public bool OwnsItem(string item)
        {
            return _inventory.Contains(item);
        }
        
        public bool OwnsItem(string item, int amount)
        {
            return _inventory.Contains(item, amount);
        }

        public override void Attack(Combatant target)
        {
            Weapon weapon = _inventory.ChooseWeapon();
            Display.Write($"{Name} tries to attack {target.Name} with {weapon.Name}.");
            bool success = weapon.Attack(target);
            if (!success) Stun();
        }

        public void OpenInventory()
        {
            _inventory.Open();
        }
        
        /// <summary>
        /// Display the player's items in a digestible manner.
        /// </summary>
        // public string InventoryContents()
        // {
        //     return "- "+string.Join("\n- ", _inventory);
        // }
    }
}