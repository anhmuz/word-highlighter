using NUnit.Framework;
using System;

namespace WordHighlighter
{
    [TestFixture()]
    public class TestCompare
    {
        [Test()]
        public void TestSimple()
        {
            Assert.AreEqual(true, StringHelpers.Compare("aabbcc", "bb", 2));
            Assert.AreEqual(false, StringHelpers.Compare("aabbcc", "bb", 3));
        }

        [Test()]
        public void TestEmptyValue()
        {
            Assert.AreEqual(true, StringHelpers.Compare("aabbcc", "", 2));
        }

        [Test()]
        public void TestEmptyText()
        {
            Assert.AreEqual(false, StringHelpers.Compare("", "bb", 0));
            Assert.AreEqual(true, StringHelpers.Compare("", "", 0));
        }

        [Test()]
        public void TestInvalidStartIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => StringHelpers.Compare("aabbcc", "bbc", 4));
        }

        [Test()]
        public void TestNullValue()
        {
            Assert.Throws<ArgumentNullException>(
                () => StringHelpers.Compare("aabbcc", null, 2));
        }

        [Test()]
        public void TestNullText()
        {
            Assert.Throws<ArgumentNullException>(
                () => StringHelpers.Compare(null, "bb", 2));
        }
    }
}

