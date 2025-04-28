using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public class Miscellaneous : Item
    {
        public Miscellaneous(string name, string description) : base(name, description) { }

        public override void Use()
        {
            Display.Write("This item won't work here.");
        }
    }

    public class Weapon : Item
    {
        public Weapon(string name, string description) : base(name, description) { }
        public override void Use()
        {
            Display.Write("You can only use this item whilst in combat.");
        }
        public virtual bool Attack(Combatant enemy)
        {
            Display.Write($"You try to attack {enemy.Name} with {Name}.");
            return enemy.TakeDamage(10);
        }
    }

    // TODO: implement
    public class DamageWeapon : Weapon
    {

    }


    public class Consumable : Item
    {

    }
}
