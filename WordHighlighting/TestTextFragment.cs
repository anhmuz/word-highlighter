using System;

namespace WordHighlighter
{
    public class TestTextFragment
    {
        public TestTextFragment(string testTextFragment,
            ConsoleColor testTextColor)
        {
            Fragment = testTextFragment;
            Color = testTextColor;
        }

        public string Fragment { get; private set;}

        public ConsoleColor Color { get; private set;}
    }
}

