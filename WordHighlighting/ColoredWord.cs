using System;

namespace WordHighlighter
{
    public class ColoredWord
    {
        private string _word;
        private ConsoleColor _color;

        public ColoredWord(string word, ConsoleColor color)
        {
            _word = word;
            _color = color;
        }

        public string Word
        {
            get
            {
                return _word;
            }
        }

        public ConsoleColor Color
        {
            get
            {
                return _color;
            }
        }

    }
}

