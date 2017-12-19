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

        [Test()]
        public void TestInvalidOptions()
        {
            var o = new TestOutput(new List<TestTextFragment>());
            var wh = new WordHighlighter(o);
            Assert.Throws<ArgumentException>(
                () => wh.Print("aaa", (WordHighlighter.PrintOptions)200));
        }

        [Test()]
        public void TestHighlightWholeWordsOnly()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a", ConsoleColor.Red),
                    new ColoredWord("b", ConsoleColor.Blue)
                },
                "a,b a.ab?a",
                new List<TestTextFragment>
                {
                    new TestTextFragment("a", ConsoleColor.Red),
                    new TestTextFragment(","),
                    new TestTextFragment("b", ConsoleColor.Blue),
                    new TestTextFragment(" "),
                    new TestTextFragment("a", ConsoleColor.Red),
                    new TestTextFragment(".ab?"),
                    new TestTextFragment("a", ConsoleColor.Red),
                },
                WordHighlighter.PrintOptions.WholeWordsOnly);
        }

        [Test()]
        public void TestWordWithPunctuation()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord("a,a.", ConsoleColor.Red),
                    new ColoredWord("b", ConsoleColor.Blue)
                },
                "a,a. b",
                new List<TestTextFragment>
                {
                    new TestTextFragment("a,a.", ConsoleColor.Red),
                    new TestTextFragment(" "),
                    new TestTextFragment("b", ConsoleColor.Blue)
                },
                WordHighlighter.PrintOptions.WholeWordsOnly);
        }

        [Test()]
        public void TestWordsStartWithPunctuation()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord(" a,a.", ConsoleColor.Red),
                    new ColoredWord(" b,b.", ConsoleColor.Blue)
                },
                " a,a.@ b,b.",
                new List<TestTextFragment>
                {
                    new TestTextFragment(" a,a.", ConsoleColor.Red),
                    new TestTextFragment("@"),
                    new TestTextFragment(" b,b.", ConsoleColor.Blue)
                },
                WordHighlighter.PrintOptions.WholeWordsOnly);
        }

        [Test()]
        public void TestAdjacentWordsStartWithPunctuation()
        {
            Test(new List<ColoredWord>
                {
                    new ColoredWord(" a,a.", ConsoleColor.Red),
                    new ColoredWord("@b,b.", ConsoleColor.Blue)
                },
                " a,a.@b,b.",
                new List<TestTextFragment>
                {
                    new TestTextFragment(" a,a.", ConsoleColor.Red),
                    new TestTextFragment("@b,b.", ConsoleColor.Blue)
                },
                WordHighlighter.PrintOptions.WholeWordsOnly);
        }

        private void Test(List<ColoredWord> coloredWords, string text,
            List<TestTextFragment> expectedTextFragments,
            WordHighlighter.PrintOptions options =
            WordHighlighter.PrintOptions.None)
        {
            var o = new TestOutput(expectedTextFragments);
            var wh = new WordHighlighter(o);
            foreach (ColoredWord cw in coloredWords)
                wh.Add(cw);
            wh.Print(text, options);

            // check that WordHighlighter.Print() printed all expected fragments
            Assert.AreEqual(expectedTextFragments.Count, o.CurrentIndex);
        }
    }
}

