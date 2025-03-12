using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a choice that can be picked inside a menu, mapping it to a function or 'Action'.
    /// </summary>
    public class Choice
    {
        const string SelectedPrefix = ">";
        private readonly string _option;
        private readonly Action _action;
        private readonly ConsoleColor _color = ConsoleColor.White;
        
        /// <summary>
        /// Multiple constructors allowing for customisation: choose colour of option when displayed if you so wish.
        /// </summary>
        public Choice(string option, Action action)
        {
            _option = option;
            _action = action;
        }

        public Choice(string option, Action action, ConsoleColor color)
        {
            _option = option;
            _action = action;
            _color = color;
        }
        
        /// <summary>
        /// Perform the action associated to the option.
        /// </summary>
        public void Choose()
        {
            Console.Clear();
            _action();
        }
        
        /// <summary>
        /// Display the choice, applying any necessary effects to display whether it is currently selected
        /// + its colour.
        /// </summary>
        public void Display(bool selected)
        {
            Console.ResetColor();
            if (selected)
            {
                Console.BackgroundColor = _color;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            string prefix = selected ? SelectedPrefix : " ";
            Console.WriteLine($"{prefix} {_option}");
            Console.ResetColor();
        }
    }
}