using System;
using System.Collections.Generic;

namespace WordHighlighter
{
    public class WordHighlighter
    {
        private List<ColoredWord> _coloredWords = new List<ColoredWord>();
        private readonly IOutput _output;

        public interface IOutput
        {
            void Print(string fragment, ConsoleColor color);
            void Print(string fragment);
        }

        public WordHighlighter(IOutput output)
        {
            _output = output;
        }

        public void Add(ColoredWord cw)
        {
            if (cw == null)
                throw new ArgumentNullException("cw");
            if (cw.Word == null)
                throw new ArgumentNullException("cw.Word");
            
            _coloredWords.Add(cw);
        }

        public void Print(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            int letters = 0;
            for (int i = 0; i < text.Length; i++)
            {
                bool isPrinted = false;
                foreach (ColoredWord cw in _coloredWords)
                {
                    if (i + cw.Word.Length > text.Length ||
                        !StringHelpers.Compare(text, cw.Word, i))
                    {
                        continue;
                    }
                    if (letters != 0)
                    {
                        string f = text.Substring(i - letters, letters);
                        _output.Print(f);
                    }
                    _output.Print(cw.Word, cw.Color);
                    letters = 0;
                    isPrinted = true;
                    i += cw.Word.Length - 1;
                    break;
                }
                if (!isPrinted)
                {
                    letters++;
                }
            }

            if (letters != 0)
            {
                _output.Print(text.Substring(text.Length - letters));
            }
        }
    }
}

