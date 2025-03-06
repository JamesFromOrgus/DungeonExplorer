using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Menu
    {
        private readonly string _prompt;

        private readonly Choice[] _choices;
        private int _choiceIndex;

        private int ChoiceIndex
        {
            get => _choiceIndex;
            set
            {
                int modulo = value % _choices.Length;
                _choiceIndex = modulo >= 0 ? modulo : modulo + _choices.Length;
            }
        }
        
        public Menu(string prompt, Choice[] choices) {
            _prompt = prompt;
            _choices = choices;
        }
        
        public Menu(string prompt, List<Choice> choices)
        {
            _prompt = prompt;
            _choices = choices.ToArray();
        }

        public Menu(Choice[] choices)
        {
            _choices = choices;
        }
        
        public Menu(List<Choice> choices)
        {
            _choices = choices.ToArray();
        }

        public void Open()
        {
            bool chosen = false;
            while (!chosen)
            {
                chosen = Display();
            }
        }

        private bool Display()
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(_prompt))
            {
                Console.WriteLine(_prompt);
            }
            foreach (Choice choice in _choices)
            {
                choice.Display(choice == _choices[ChoiceIndex]);
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    ChoiceIndex--;
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    ChoiceIndex++;
                    break;
                }
                case ConsoleKey.Enter:
                {
                    _choices[ChoiceIndex].Choose();
                    return true;
                }
            }
            return false;
        }
    }
}