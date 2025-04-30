using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory : IOpenable
    {
        private Dictionary<string, int> _items = new Dictionary<string, int>();

        public bool Contains(string name)
        {
            return _items.ContainsKey(name);
        }

        public bool Contains(string name, int amount)
        {
            if (!_items.ContainsKey(name)) return false;
            return _items[name] >= amount;
        }
        
        public void AddItem(string name, int amount)
        {
            if (_items.ContainsKey(name))
            {
                _items[name] += amount;
                return;
            }
            _items.Add(name, amount);
        }

        public void RemoveItem(string name, int amount)
        {
            if (!_items.ContainsKey(name)) return;
            _items[name] -= amount;
            if (_items[name] <= 0)
            {
                _items.Remove(name);
            }
        }

        public Weapon ChooseWeapon()
        {
            Weapon chosen = null;
            List<Choice> choices = new List<Choice>();
            foreach (var nameAndAmount in _items
                         .Where((pair => Item.GetItem(pair.Key) is Weapon))
                         .OrderBy(pair => (Item.GetItem(pair.Key) as Weapon).Damage))
            {
                string name = nameAndAmount.Key;
                choices.Add(new Choice(name, () =>
                {
                    chosen = Item.GetItem(name) as Weapon;
                }));
            }
            Menu weaponsMenu = new Menu("Pick an item to attack with:", choices);
            weaponsMenu.Open();
            return chosen;
        }

        public void Open()
        {
            Func<Item, bool> filter = (x) => true;
            Choice allItems = new Choice("All items", () => { });
            Choice healingItems = new Choice("Healing items", () => { filter = (x) => x is HealingItem; });
            Choice weapons = new Choice("Weapons", () => { filter = (x) => x is IOffensive; });
            Menu filterMenu = new Menu("How would you like to filter items?",
                new[] { allItems, healingItems, weapons });
            filterMenu.Open();
                
            List<Choice> choices = new List<Choice>();
            foreach (var nameAndAmount in _items.Where((pair => filter(Item.GetItem(pair.Key)))))
            {
                string name = nameAndAmount.Key;
                int amount = nameAndAmount.Value;
                string label = amount == 1 ? name : $"{name} ({amount}x)";
                choices.Add(new Choice(label, () =>
                {
                    Item.GetItem(name).Interact();
                }));
            }
            choices.Add(new Choice("Return", () => {}));

            Menu itemsMenu = new Menu("Inventory:", choices);
            itemsMenu.Open();
        }
    }
}