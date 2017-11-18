using System;
using System.IO;

namespace WordHighlighter
{
    class Program
    {
        public static void Main(string[] args)
        {
            string text = File.ReadAllText(args[0]);
            string[] words = StringHelpers.Split(text,
                StringHelpers.StringSplitOptions.RemoveEmptyEntries);
            ConsoleColor[] colors = {ConsoleColor.Blue, ConsoleColor.Cyan,
                ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Magenta,
                ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow,
                ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan};
            var output = new ConsoleOutput();
            WordHighlighter wh = new WordHighlighter(output);
            for (int i = 0; i < words.Length; i++)
            {
                ColoredWord cw = new ColoredWord(words[i],
                    colors[i % colors.Length]);
                wh.Add(cw);
            }
            Console.WriteLine("Enter text: ");
            string line = Console.ReadLine();
            wh.Print(line);
            Console.WriteLine();
            Console.WriteLine("The end");
        }
    }
}
