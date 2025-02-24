using System;

namespace DungeonExplorer
{
    public class Choice
    {
        const string SelectedPrefix = ">";
        private readonly string _option;
        private readonly Action _action;
        private readonly ConsoleColor _color = ConsoleColor.White;
        
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

        public void Choose()
        {
            Console.Clear();
            _action();
        }

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