using System;

namespace WordHighlighter
{
    public class ColoredWord
    {
        public ColoredWord(string word, ConsoleColor color)
        {
            Word = word;
            Color = color;
        }

        public string Word { get; private set;}

        public ConsoleColor Color { get; private set;}
    }
}

