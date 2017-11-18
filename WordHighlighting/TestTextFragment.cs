using System;

namespace WordHighlighter
{
    public class TestTextFragment
    {
        public TestTextFragment(string fragment, ConsoleColor color)
        {
            Fragment = fragment;
            Color = color;
        }

        public TestTextFragment(string fragment)
        {
            Fragment = fragment;
            Color = null;
        }

        public string Fragment { get; private set;}

        public ConsoleColor? Color { get; private set;}
    }
}

