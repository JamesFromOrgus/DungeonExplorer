using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer
{
    /// <summary>
    /// Provides interface to select from a finite amount of options, performing an action associated to whichever
    /// 'Choice' was chosen. This means there is little chance for error: no opportunity for erroneous input like
    /// there is with Console.ReadLine
    /// </summary>
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
        
        /// <summary>
        /// Multiple constructors to allow for ease of use: may not always know how many options there will be before
        /// generating them -> easier to use list rather than array in certain cases.
        /// </summary>
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
    
        /// <summary>
        /// Keep displaying menu until a choice is made.
        /// </summary>
        public void Open()
        {
            bool chosen = false;
            while (!chosen)
            {
                chosen = Display();
            }
        }

        /// <summary>
        /// Render the menu. Update current choice pointer if up/down is pressed on keyboard.
        /// </summary>
        private bool Display()
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(_prompt))
            {
                Console.WriteLine(_prompt);
            }
            
            Debug.Assert(_choices.Length > 0, "Menu with no choices, fix required.");
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
                    try
                    {
                        _choices[ChoiceIndex].Choose();
                        return true;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("No option was chosen. This is probably the result of a bug.");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}