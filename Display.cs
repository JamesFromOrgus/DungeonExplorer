using System;
using System.Threading;

namespace DungeonExplorer
{
    public static class Display
    {
        private static int rate = 20;
        private static string currentMessage;
        private static int charsWritten;

        private static void WriteChars(int amount)
        {
            string substring = currentMessage.Substring(charsWritten, amount);
            Console.Write(substring);
            charsWritten += amount;
            if (charsWritten == currentMessage.Length) Console.ReadKey(true);
        }

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