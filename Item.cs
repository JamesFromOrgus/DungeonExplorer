using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Item
    {
        private string _name;
        private string _description;
        public string Name => _name;
        public string Description => _description;
        public abstract void Use();
        public Item(string name, string description)
        {
            _name = name;
            _description = description;
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
        public Weapon(string name, string description, int damage) : base(name, description)
        {
            _damage = damage;
        }
        public override void Use()
        {
            Display.Write("You can only use this item whilst in combat.");
        }
        public virtual bool Attack(Combatant enemy)
        {
            Display.Write($"You try to attack {enemy.Name} with {Name}.");
            return enemy.TakeDamage(_damage);
        }
    }


    public class HealthPotion : Item
    {
        public int _healAmount;
        public HealthPotion(string name, int healAmount): base(name, $"Heals you for {healAmount}hp.")
        {
            _healAmount = healAmount;
        }

        public override void Use()
        {
            Game.CurrentPlayer.Heal(_healAmount);
        }
    }
}
