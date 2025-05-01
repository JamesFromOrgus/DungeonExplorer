using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Base class, hold metadata like name and desc. Provide default "use" behaviour.
    /// </summary>
    public abstract class Item : IInspectable, IInteractable
    {
        private static Dictionary<string, Item> _nameMap = new Dictionary<string, Item>();
        private string _name;
        private string _description;
        protected int _limit = 64;
        public int Limit => _limit;
        public string Name => _name;
        public string Description => _description;
        public abstract void Use();
        protected Item(string name, string description)
        {
            _name = name;
            _description = description;
            _nameMap[name] = this;
        }

        /// <summary>
        /// Return an item class from the item's name.
        /// </summary>
        public static Item GetItem(string name)
        {
            Debug.Assert(_nameMap.ContainsKey(name), $"Item {name} not found");
            return _nameMap[name];
        }
        
        /// <summary>
        /// Provide the player with the item's description.
        /// </summary>
        public void Inspect()
        {
            Display.Write($"You inspect {Name}.");
            Display.Write(Description);
        }

        /// <summary>
        /// Give user options of what to do with the item.
        /// </summary>
        public void Interact()
        {
            Choice inspect = new Choice("Inspect", Inspect);
            Choice use = new Choice("Use", Use);
            Choice nothing = new Choice("Nothing", () => { });
            Menu interactMenu = new Menu($"What would you like to do with {Name}?",
                new []{inspect, use, nothing});
            interactMenu.Open();
        }
    }
    
    /// <summary>
    /// Placeholder items such as keys.
    /// </summary>
    public class Miscellaneous : Item
    {
        public Miscellaneous(string name, string description) : base(name, description) { }

        public override void Use()
        {
            Display.Write("It looks like this item doesn't work here...");
        }
    }

    /// <summary>
    /// Class for weapons that deal damage with no additional behaviour.
    /// </summary>
    public class Weapon : Item, IOffensive
    {
        private int _damage;
        public int Damage => _damage;
        public Weapon(string name, string description, int damage) : base(name, description)
        {
            _damage = damage;
            _limit = 1;
        }
        public override void Use()
        {
            Display.Write("You can only use this item whilst in combat.");
        }
        
        /// <summary>
        /// Deal damage to enemy.
        /// </summary>
        public virtual bool Attack(Combatant enemy)
        {
            //Display.Write($"You try to attack {enemy.Name} with {Name}.");
            return enemy.TakeDamage(_damage);
        }
    }

    /// <summary>
    /// Items that heal the user.
    /// </summary>
    public class HealingItem : Item
    {
        private readonly int _healAmount;
        public int HealAmount => _healAmount;
        public HealingItem(string name, int healAmount): base(name, $"Heals you for {healAmount}hp.")
        {
            _healAmount = healAmount;
        }
        
        public HealingItem(string name, string description, int healAmount): base(name, description+$"\nHeals you for {healAmount}hp.")
        {
            _healAmount = healAmount;
        }
        
        /// <summary>
        /// Heal for the specified amount.
        /// </summary>
        public override void Use()
        {
            Display.Write($"You consume {Name}.");
            Game.CurrentPlayer.Heal(_healAmount);
            Game.CurrentPlayer.RemoveItem(Name, true);
        }
    }
}
