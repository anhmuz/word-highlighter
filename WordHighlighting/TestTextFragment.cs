using System;

namespace WordHighlighter
{
    public class TestTextFragment
    {
        private string _fragment;
        private ConsoleColor _color;

        public TestTextFragment(string testTextFragment,
            ConsoleColor testTextColor)
        {
            _fragment = testTextFragment;
            _color = testTextColor;
        }

        public string Fragment
        {
            get
            {
                return _fragment;
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

