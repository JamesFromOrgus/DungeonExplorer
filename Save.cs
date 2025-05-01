using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DungeonExplorer
{
    public class Save
    {
        private readonly string _room = "Astral Observatory";
        private Dictionary<string, int> _items = new Dictionary<string, int>();
        private readonly int _kills = 0;
        public string StartRoom => _room;
        public int Kills => _kills;
        public Dictionary<string, int> Items => _items;
        /// <summary>
        /// Decodes save file in form of (current room; inventory; stats)
        /// </summary>
        public Save(string name)
        {
            string filename = GetFileName(name);
            if (!File.Exists(filename))
            {
                _items.Add("Kokiri Sword", 1);
                return;
            };
            try
            {
                var text = File.ReadAllText(filename);
                string[] fields = text.Split('\n');
                _room = fields[0];
                string[] items = fields[1].Split(',');
                foreach (string item in items)
                {
                    string[] itemParts = item.Split(':');
                    _items.Add(itemParts[0], int.Parse(itemParts[1]));
                }
                _kills = int.Parse(fields[2]);
            }
            catch (Exception e)
            {
                Display.Write("Detected save file is corrupted.");
            }
        }

        /// <summary>
        /// Writes current game state to a file.
        /// </summary>
        public static void WriteFile()
        {
            string data = "";
            data += Game.CurrentRoom.Name + "\n";
            foreach (KeyValuePair<string, int> kvp in Game.CurrentPlayer.Inventory.Items)
            {
                data += $"{kvp.Key}:{kvp.Value},";
            }
            data = data.TrimEnd(',') + "\n";
            data += Statistics.Kills.ToString();
            File.WriteAllText(GetFileName(Game.CurrentPlayer.Name), data);
            Display.Write("Game state saved.");
        }

        public static void DeleteFile()
        {
            string filename = GetFileName(Game.CurrentPlayer.Name);
            if (!File.Exists(filename)) return;
            File.Delete(filename);
        }

        private static string GetFileName(string characterName)
        {
            return "save_" + characterName + ".txt";
        }
    }
}