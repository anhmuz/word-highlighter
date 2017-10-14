using NUnit.Framework;
using System;

namespace WordHighlighter
{
    [TestFixture()]
    public class TestSplit
    {
        [Test()]
        public void TestSimple()
        {
            Assert.AreEqual(new string[] {"Vadym", "Anhelina", "Svynjaka"},
                StringHelpers.Split("Vadym,Anhelina,Svynjaka"));
        }

        [Test()]
        public void TestEmptyText()
        {
            Assert.AreEqual(new string[] {""},
                StringHelpers.Split(""));
        }

        [Test()]
        public void TestWithoutDelimiters()
        {
            Assert.AreEqual(new string[] {"VadymAnhelinaSvynjaka"},
                StringHelpers.Split("VadymAnhelinaSvynjaka"));
        }

        [Test()]
        public void TestAdjacentDelimiters()
        {
            Assert.AreEqual(new string[] {"aaa", "", "bb"},
                StringHelpers.Split("aaa,,bb"));
        }

        [Test()]
        public void TestFrontAndTrailingDelimiters()
        {
            Assert.AreEqual(new string[] {"", "aaabb"},
                StringHelpers.Split(",aaabb"));
            Assert.AreEqual(new string[] {"aaabb", ""},
                StringHelpers.Split("aaabb,"));
        }

        [Test()]
        public void TestNullText()
        {
            Assert.Throws<ArgumentNullException>(
                () => StringHelpers.Split(null));
        }

        [Test()]
        public void TestRemoveEmptyEntries()
        {
            Assert.AreEqual(new string[] {"Vadym", "Anhelina", "Svyniaka"},
                StringHelpers.Split("Vadym,,,,Anhelina,Svyniaka",
                    StringHelpers.StringSplitOptions.RemoveEmptyEntries));
        }

        [Test()]
        public void TestEmptyOrNullDelimitersArray()
        {
            Assert.AreEqual(new string[] {"Vadym", "Anhelina", "Svynjaka"},
                StringHelpers.Split("Vadym Anhelina Svynjaka", new char[0]));
            Assert.AreEqual(new string[] {"Vadym", "Anhelina", "Svynjaka"},
                StringHelpers.Split("Vadym Anhelina Svynjaka", null));
        }

        [Test()]
        public void TestAlphabetDelimiters()
        {
            char[] delimiters = new char['z' - 'a' + 1];
            for (char i = 'a'; i <= 'z'; i++)
            {
                delimiters[i - 'a'] = i;
            }
            Assert.AreEqual(new string[] {"", "@", "#", "$", ""},
                StringHelpers.Split("w@f#m$o", delimiters));
        }

        [Test()]
        public void TestInvalidOptions()
        {
            Assert.Throws<ArgumentException>(
                () => StringHelpers.Split("VadymAnhelinaSvynjaka",
                    (StringHelpers.StringSplitOptions)200));
        }

        [Test()]
        public void TestFrontAndTrailingEmptyEntries()
        {
            Assert.AreEqual(new string[] {"aaabb"},
                StringHelpers.Split(",aaabb",
                    StringHelpers.StringSplitOptions.RemoveEmptyEntries));
            Assert.AreEqual(new string[] {"aaabb"},
                StringHelpers.Split("aaabb,",
                    StringHelpers.StringSplitOptions.RemoveEmptyEntries));
        }
    }
}

