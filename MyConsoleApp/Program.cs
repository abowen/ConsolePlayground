using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Measured in characters & rows, not pixels.
            Console.SetWindowSize(64, 48);
            Console.SetBufferSize(64, 48);

            var items = new []
            {
                new MenuItem { Action = ExitConsole, Key= ConsoleKey.D0, Title = "Exit" },
                new MenuItem { Action = StartAsync, Key= ConsoleKey.D1, Title = "Progress Title" },                
            };
            
            while (true)
            {
                PrintMenu(items);
                var keyInfo = Console.ReadKey(true);
                var key = keyInfo.Key;
                Console.WriteLine("Key Pressed: {0}", key);
                foreach (var item in items)
                {
                    if (item.Key == key)
                    {
                        item.Action();
                    }
                }
                System.Threading.Thread.Sleep(100);
                Console.Clear();                
            }
        }

        public static void PrintMenu(MenuItem[] items)
        {
            foreach (var item in items)
            {                
                Console.WriteLine("{0}. {1}", item.Key, item.Title);
            }
        }

        private static async void StartAsync()
        {
            await Task.Run(() =>
            {
                var value = 0;
                while (value <= 100)
                {                    
                    Console.Title = value.ToString();
                    System.Threading.Thread.Sleep(50);
                    value++;
                }
            });
        }

        private static void ExitConsole()
        {
            Environment.Exit(0);
        }

    }

    internal class MenuItem
    {
        public ConsoleKey Key;
        public Action Action;
        public string Title;
    }
}
