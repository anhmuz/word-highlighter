using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WordHighlighter
{
    public class TestOutput: WordHighlighter.IOutput
    {
        private List<TestTextFragment> _expectedTextFragments;
        private int _currentIndex = 0;

        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
        }

        public TestOutput(List<TestTextFragment> expectedTextFragments)
        {
            _expectedTextFragments = expectedTextFragments;
        }

        public void Print(string fragment, ConsoleColor color)
        {
            Assert.Greater(_expectedTextFragments.Count, _currentIndex);
            Assert.AreEqual(_expectedTextFragments[_currentIndex].Fragment,
                fragment);
            Assert.AreEqual(_expectedTextFragments[_currentIndex].Color,
                color);
            ++_currentIndex;
        }

        public void Print(string fragment)
        {
            Assert.Greater(_expectedTextFragments.Count, _currentIndex);
            Assert.AreEqual(_expectedTextFragments[_currentIndex].Fragment,
                fragment);
            Assert.AreEqual(_expectedTextFragments[_currentIndex].Color,
                ConsoleColor.White);
            ++_currentIndex;
        }
    }
}

