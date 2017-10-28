using System;

namespace WordHighlighter
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter words: ");
            string listOfWords = Console.ReadLine();
            const char delimiter = ' ';
            string[] words = listOfWords.Split(delimiter);
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
            string text = Console.ReadLine();

            wh.Print(text);
            Console.WriteLine();
            Console.WriteLine("The end");
        }
    }
}
