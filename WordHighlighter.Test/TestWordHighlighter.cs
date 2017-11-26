using NUnit.Framework;
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
                    new TestTextFragment("aa"),
                    new TestTextFragment("Vadym", ConsoleColor.Red),
                    new TestTextFragment("aa"),
                    new TestTextFragment("Anhelina", ConsoleColor.Blue),
                    new TestTextFragment("aa")
                }
            );
        }

        [Test()]
        public void TestTrailingColoredWord()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("Vadym", ConsoleColor.Red),
                    new ColoredWord("Anhelina", ConsoleColor.Blue),
                    new ColoredWord("Tux", ConsoleColor.Yellow)
                },
                "aaVadymaaAnhelinaaaTux",
                new List<TestTextFragment>
                {
                    new TestTextFragment("aa"),
                    new TestTextFragment("Vadym", ConsoleColor.Red),
                    new TestTextFragment("aa"),
                    new TestTextFragment("Anhelina", ConsoleColor.Blue),
                    new TestTextFragment("aa"),
                    new TestTextFragment("Tux", ConsoleColor.Yellow)
                }
            );
        }

        [Test()]
        public void TestEmptyText()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a", ConsoleColor.Red),
                    new ColoredWord("b", ConsoleColor.Blue)
                },
                string.Empty, 
                new List<TestTextFragment>());
        }

        [Test()]
        public void TestFrontColoredWord()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a", ConsoleColor.Red),
                    new ColoredWord("b", ConsoleColor.Blue)
                },
                "ac",
                new List<TestTextFragment>
                {
                    new TestTextFragment("a", ConsoleColor.Red),
                    new TestTextFragment("c")
                });
        }

        [Test()]
        public void TestAdjacentColoredWords()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a", ConsoleColor.Red),
                    new ColoredWord("b", ConsoleColor.Blue)
                },
                "cabc",
                new List<TestTextFragment>
                {
                    new TestTextFragment("c"),
                    new TestTextFragment("a", ConsoleColor.Red),
                    new TestTextFragment("b", ConsoleColor.Blue),
                    new TestTextFragment("c")
                });
        }

        [Test()]
        public void TestEqualWords()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a", ConsoleColor.Red),
                    new ColoredWord("a", ConsoleColor.Blue)
                },
                "aa",
                new List<TestTextFragment>
                {
                    new TestTextFragment("a", ConsoleColor.Red),
                    new TestTextFragment("a", ConsoleColor.Red)
                }
            );
        }

        [Test()]
        public void TestNullText()
        {
            var o = new TestOutput(new List<TestTextFragment>());
            var wh = new WordHighlighter(o);
            Assert.Throws<ArgumentNullException>(() => wh.Print(null));
        }

        [Test()]
        public void TestNullColoredWord()
        {
            var o = new TestOutput(new List<TestTextFragment>());
            var wh = new WordHighlighter(o);
            Assert.Throws<ArgumentNullException>(() => wh.Add(null));
        }

        [Test()]
        public void TestNullWord()
        {
            var o = new TestOutput(new List<TestTextFragment>());
            var wh = new WordHighlighter(o);
            Assert.Throws<ArgumentNullException>(
                () => wh.Add(new ColoredWord(null, ConsoleColor.Red)));
        }

        private void Test(List<ColoredWord> coloredWords, string text,
            List<TestTextFragment> expectedTextFragments)
        {
            var o = new TestOutput(expectedTextFragments);
            var wh = new WordHighlighter(o);
            foreach (ColoredWord cw in coloredWords)
                wh.Add(cw);
            wh.Print(text);

            // check that WordHighlighter.Print() printed all expected fragments
            Assert.AreEqual(expectedTextFragments.Count, o.CurrentIndex);
        }
    }
}

