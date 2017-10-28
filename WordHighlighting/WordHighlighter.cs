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
            _coloredWords.Add(cw);
        }

        public void Print(string text)
        {
            int numberOfUncolouredLetters = 0;
            for (int i = 0; i < text.Length; i++)
            {
                bool isPrinted = false;
                foreach (ColoredWord cw in _coloredWords)
                {
                    if (i + cw.Word.Length <= text.Length &&
                        StringHelpers.IndexOf(
                            text, cw.Word, i, cw.Word.Length) != -1)
                    {
                        _output.Print(text.Substring
                            (i - numberOfUncolouredLetters,
                            numberOfUncolouredLetters));
                        _output.Print(cw.Word, cw.Color);
                        numberOfUncolouredLetters = 0;
                        isPrinted = true;
                        i += cw.Word.Length - 1;
                        break;
                    }
                }
                if (!isPrinted)
                {
                    numberOfUncolouredLetters++;
                }
            }

            _output.Print(
                text.Substring(text.Length - numberOfUncolouredLetters));
        }
    }
}

