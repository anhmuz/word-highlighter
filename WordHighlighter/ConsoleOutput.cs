using System;

namespace WordHighlighter
{
    public class ConsoleOutput: WordHighlighter.IOutput
    {
        public void Print(string fragment, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(fragment);
            Console.ResetColor();
        }

        public void Print(string fragment)
        {
            Console.Write(fragment);
        }
    }
}

