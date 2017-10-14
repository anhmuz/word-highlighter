using NUnit.Framework;
using System;

namespace WordHighlighter
{
    [TestFixture()]
    public class TestIndexOf
    {
        [Test()]
        public void TestSimple()
        {
            Assert.AreEqual(2, StringHelpers.IndexOf("aabbcc", "bb", 0, 6));
        }

        [Test()]
        public void TestNegative()
        {
            Assert.AreEqual(-1, StringHelpers.IndexOf("aabbcc", "xx", 0, 6));
        }

        [Test()]
        public void TestBeginning()
        {
            Assert.AreEqual(0, StringHelpers.IndexOf("aabbcc", "aa", 0, 6));
        }

        [Test()]
        public void TestEnding()
        {
            Assert.AreEqual(4, StringHelpers.IndexOf("aabbcc", "cc", 0, 6));
        }

        [Test()]
        public void TestInvalidValueLength()
        {
            Assert.AreEqual(-1,
                StringHelpers.IndexOf("aabbcc", "xxxxxxx", 0, 6));
        }

        [Test()]
        public void TestValuePartiallyExists()
        {
            Assert.AreEqual(-1,
                StringHelpers.IndexOf("aabbcc", "ccaa", 0, 6));
        }

        [Test()]
        public void TestNegativeIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.IndexOf("aabbcc", "ccaa", 2, -3));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.IndexOf("aabbcc", "ccaa", -2, 3));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.IndexOf("aabbcc", "ccaa", -2, -3));
        }

        [Test()]
        public void TestZeroCount()
        {
            Assert.AreEqual(-1,
                StringHelpers.IndexOf("aabbcc", "bb", 2, 0));
            Assert.DoesNotThrow(
                () => StringHelpers.IndexOf("aabbcc", "bb", 2, 0));
            Assert.DoesNotThrow(
                () => "aabbcc".IndexOf("ccaa", 2, 0));
        }

        [Test()]
        public void TestEmptyValue()
        {
            Assert.AreEqual(2, StringHelpers.IndexOf("aabbcc", "", 2, 4));
        }

        [Test()]
        public void TestEmptyText()
        {
            Assert.AreEqual(-1, StringHelpers.IndexOf("", "bb", 0, 0));
            Assert.AreEqual(0, StringHelpers.IndexOf("", "", 0, 0));
            Assert.AreEqual(0, "".IndexOf("", 0, 0));
        }

        [Test()]
        public void TestInvalidStartIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.IndexOf("aabbcc", "bb", 7, 3));
        }

        [Test()]
        public void TestInvalidCount()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.IndexOf("aabbcc", "bb", 2, 5));
        }

        [Test()]
        public void TestNullValue()
        {
            Assert.Throws<ArgumentNullException>(
                () => StringHelpers.IndexOf("aabbcc", null, 2, 3));
        }

        [Test()]
        public void TestNullText()
        {
            Assert.Throws<ArgumentNullException>(
                () => StringHelpers.IndexOf(null, "bb", 2, 3));
        }
    }
}
