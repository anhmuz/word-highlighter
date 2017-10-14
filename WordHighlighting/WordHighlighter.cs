using System;
using System.Collections.Generic;

namespace WordHighlighter
{
    public class WordHighlighter
    {
        private List<ColoredWord> _coloredWords = new List<ColoredWord>();

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
                        Console.Write(text.Substring
                            (i - numberOfUncolouredLetters,
                            numberOfUncolouredLetters));
                        Console.ForegroundColor = cw.Color;
                        Console.Write(cw.Word);
                        Console.ResetColor();
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

            Console.WriteLine(
                text.Substring(text.Length - numberOfUncolouredLetters));
        }
    }
}

