using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
            Console.WriteLine("Waiting for your Implementation");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
