using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory : IOpenable
    {
        private Dictionary<string, int> _items = new Dictionary<string, int>();
        public Dictionary<string, int> Items => _items;

        /// <summary>
        /// Denotes whether player owns an item of the given name.
        /// </summary>
        public bool Contains(string name)
        {
            return _items.ContainsKey(name);
        }

        public bool Contains(string name, int amount)
        {
            if (!_items.ContainsKey(name)) return false;
            return _items[name] >= amount;
        }
        
        /// <summary>
        /// Add x amount of item to the inventory.
        /// </summary>
        public void AddItem(string name, int amount)
        {
            if (_items.ContainsKey(name))
            {
                _items[name] = Math.Min(_items[name] + amount, Item.GetItem(name).Limit);
                return;
            }
            _items.Add(name, Math.Min(amount, Item.GetItem(name).Limit));
        }

        /// <summary>
        /// Delete x amount of item from inventory. Useful for consumables etc.
        /// </summary>
        public void RemoveItem(string name, int amount)
        {
            if (!_items.ContainsKey(name)) return;
            _items[name] -= amount;
            if (_items[name] <= 0)
            {
                _items.Remove(name);
            }
        }
        
        /// <summary>
        /// Lets user choose from all available weapons, sorted in order of descending damage using LINQ.
        /// </summary>
        public Weapon ChooseWeapon()
        {
            Weapon chosen = null;
            List<Choice> choices = new List<Choice>();
            foreach (var nameAndAmount in _items
                         .Where((pair => Item.GetItem(pair.Key) is Weapon))
                         .OrderByDescending(pair => (Item.GetItem(pair.Key) as Weapon).Damage))
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

        /// <summary>
        /// Return total item count.
        /// </summary>
        public int Count()
        {
            int total = 0;
            foreach (var kvp in _items)
            {
                total += kvp.Value;
            }
            return total;
        }

        /// <summary>
        /// Lets user interact with their items and filter them for easier location.
        /// </summary>
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