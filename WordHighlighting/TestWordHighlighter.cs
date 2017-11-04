﻿using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace WordHighlighter
{
    [TestFixture()]
    public class TestWordHighlighter
    {
        [Test()]
        public void TestSimple()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("Vadym", ConsoleColor.Red),
                    new ColoredWord("Anhelina", ConsoleColor.Blue)
                },
                "aaVadymaaAnhelinaaa",
                new List<TestTextFragment>
                {
                    new TestTextFragment("aa", ConsoleColor.White),
                    new TestTextFragment("Vadym", ConsoleColor.Red),
                    new TestTextFragment("aa", ConsoleColor.White),
                    new TestTextFragment("Anhelina", ConsoleColor.Blue),
                    new TestTextFragment("aa", ConsoleColor.White)
                }
            );
        }

        private void Test(List<ColoredWord> coloredWords, string text,
            List<TestTextFragment> expectedTextFragments)
        {
            var o = new TestOutput(expectedTextFragments);
            var wh = new WordHighlighter(o);
            foreach (ColoredWord cw in coloredWords)
                wh.Add(cw);
            wh.Print(text);

            Assert.AreEqual(expectedTextFragments.Count, o.CurrentIndex);
        }
    }
}

