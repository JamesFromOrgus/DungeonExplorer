using System;
using System.Threading;

namespace DungeonExplorer
{
    /// <summary>
    /// Provide more beautiful-looking messages with type-writing effect.
    /// </summary>
    public static class Display
    {
        private static int rate = 20;
        private static string currentMessage;
        private static int charsWritten;
        
        /// <summary>
        /// Write x amount of characters from the message to the screen.
        /// </summary>
        private static void WriteChars(int amount)
        {
            string substring = currentMessage.Substring(charsWritten, amount);
            Console.Write(substring);
            charsWritten += amount;
            if (charsWritten == currentMessage.Length) Console.ReadKey(true);
        }
        
        /// <summary>
        /// Write out message slowly, preventing keyboard input as it would disrupt the message.
        /// Allows use to skip the effect with the enter key.
        /// </summary>
        public static void Write(string message, bool newLine = true)
        {
            if (newLine)
            {
                message += "\n";
            }
            currentMessage = message;
            charsWritten = 0;
            for (int i = 0; i < message.Length; i++)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    WriteChars(message.Length - charsWritten);
                    break;
                }
                WriteChars(1);
                Thread.Sleep(1000/rate);
            }
        }
    }
}