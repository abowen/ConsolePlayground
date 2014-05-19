using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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

            var items = new[]
            {
                new MenuItem { Action = ExitConsole, Key= ConsoleKey.D0, Title = "Exit" },
                new MenuItem { Action = StartAsyncProgress, Key= ConsoleKey.D1, Title = "Progress Title" },                
            };

            var drawComponents = new List<Component>();
            var blueComponent = new Component
            {
                X = 10,
                Y = 10,
                Height = 20,
                Width = 10,
                BackgroundColor = ConsoleColor.DarkBlue,
                Title = "WOW BLUE"
            };
            var redComponent = new Component
            {
                X = 30,
                Y = 20,
                Height = 15,
                Width = 15,
                BackgroundColor = ConsoleColor.DarkRed,
                Title = "SO RED"
            };
            drawComponents.Add(blueComponent);
            drawComponents.Add(redComponent);

            while (true)
            {                
                PrintMenu(items);

                var x = Console.CursorLeft;
                var y = Console.CursorTop;
                foreach (var component in drawComponents)
                {
                    component.Draw();
                }
                Console.CursorLeft = x;
                Console.CursorTop = y;

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
                System.Threading.Thread.Sleep(15);
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

        private static async void StartAsyncProgress()
        {
            await Task.Run(() =>
            {
                var value = 0;
                while (value <= 100)
                {
                    Console.Title = value.ToString(CultureInfo.InvariantCulture);
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

    internal class Component
    {
        public string Title { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public void Draw()
        {            
            
            Console.BackgroundColor = BackgroundColor;
            var stringBuilder = new StringBuilder();

            var centerY = Height/2;
            if (Width < Title.Length)
            {
                Width = Title.Length;
            }
            var xOffset = (Width - Title.Length) / 2;

            for (var y = 0; y < Height; y++)
            {
                Console.CursorTop = Y + y;
                for (var x = 0; x < Width; x++)
                {
                    var character = ' ';
                    if (y == centerY 
                        && x >= xOffset 
                        && (x < xOffset + Title.Length))
                    {
                        character = Title[x - xOffset];
                    }
                    Console.CursorLeft = X + x;
                    Console.Write(character);
                }                                                
                stringBuilder.Append(Environment.NewLine);
            }
            
            Console.BackgroundColor = ConsoleColor.Black;
            
        }

    }
}
