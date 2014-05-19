using System;
using System.Linq;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Measured in characters & rows, not pixels.
            Console.SetWindowSize(64, 48);
            Console.SetBufferSize(64, 48);

            var items = new[]
            {
                "Exit",
                "Progress Bar"
            };

            var key = ConsoleKey.Spacebar;

            while (!Console.KeyAvailable && key != ConsoleKey.D0)
            {
                PrintMenu(items);
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                Console.WriteLine("Key Pressed: {0}", key);
            }

        }

        public static void PrintMenu(string[] items)
        {
            for (var i = 0; i < items.Count(); i++)
            {
                Console.WriteLine("{0}. {1}", i, items[i]);
            }
        }
    }
}
