using System;
using System.Collections.Generic;
using System.Media;

namespace DungeonExplorer
{
    /// <summary>
    /// Class for testing basic features individually
    /// </summary>
    public class Testing
    {
        /// <summary>
        /// Begin the testing process, individually calling upon each system in the project
        /// </summary>
        public void Test()
        {
            // Test menu system, create basic menu with two options that print different things.
            Menu choice = new Menu("Pick a word:", new[]
            {
                new Choice("Option A", () =>
                {
                    Display.Write("ok you picked option A");
                    Display.Write("Multiline function lol");
                }, ConsoleColor.Red),
                new Choice( "Option B", () => Console.WriteLine("ok you picked option B"))
            });
            choice.Open();
            
            // Test dialogue system, basic conversation about nothing: also includes cycles
            DialogueNode ping = new DialogueNode("The guy", "Ping");
            DialogueNode nextNode = new DialogueNode("The guy", "I'm glad you wanted to hear more! 'ping' is an expression relating to the popular sport, Table Tennis.\n" +
                "It is commonly referred to as 'Ping-Pong', hence why 'pong' was provided as an option.\nUs going back and forth with 'ping' and 'pong' is reminiscent of\n" +
                "a ping-pong ball oscillating from one side of the table to another.");
            ping.AddResponse("Pong", ping);
            ping.AddResponse("Please elaborate.", nextNode);
            ping.AddChoice("Leave");
            nextNode.AddResponse("Interesting, let's continue.", ping);
            
            ping.Display();

            // Test typewriting effect            
            Display.Write("Here is a test. I am gonna fill this up with words so it takes long enough to write to the" +
                          "\nthe screen as I would like to test some things such as skipping the typewriting effect.");
        }
    }
}