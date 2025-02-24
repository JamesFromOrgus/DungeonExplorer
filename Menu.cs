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
                Console.WriteLine(modulo);
                _choiceIndex = modulo >= 0 ? modulo : modulo + _choices.Length;
            }
        }
        
        public Menu(string prompt, Choice[] choices) {
            _prompt = prompt;
            _choices = choices;
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(_prompt);
            foreach (Choice choice in _choices)
            {
                choice.Display(choice == _choices[ChoiceIndex]);
            }
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    ChoiceIndex--;
                    Display();
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    ChoiceIndex++;
                    Display();
                    break;
                }
                case ConsoleKey.Enter:
                {
                    _choices[ChoiceIndex].Choose();
                    break;
                }
                default:
                {
                    Display();
                    break;
                }
            }
        }
    }
}