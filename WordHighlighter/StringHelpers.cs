using System;
using System.Collections.Generic;
using System.Linq;

namespace WordHighlighter
{
    public static class StringHelpers
    {
        public enum StringSplitOptions
        {
            None = 0,
            RemoveEmptyEntries = 1
        }

        public static bool Compare(string text, string value, int startIndex)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (text == null)
                throw new ArgumentNullException("text");
            if (text == "" && value != "" && startIndex == 0)
                return false;
            if (text.Length - startIndex < value.Length)
                throw new ArgumentOutOfRangeException("startIndex");
            
            for (int i = startIndex; i < startIndex + value.Length; i++)
            {
                if (text[i] != value[i - startIndex])
                    return false;
            }

            return true;
        }

        public static int IndexOf(string text,string value,
            int startIndex, int count)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (text == null)
                throw new ArgumentNullException("text");
            if (count < 0 || count > text.Length - startIndex)
                throw new ArgumentOutOfRangeException("count");
            if (startIndex < 0 || startIndex > text.Length)
                throw new ArgumentOutOfRangeException("startIndex");
            
            if (text == "" && value == "" && startIndex == 0 && count == 0)
                return 0;
            
            for (int i = startIndex;
                i + value.Length <= startIndex + count;
                i++)
            {
                if (Compare(text, value, i))
                    return i;
            }

            return -1;
        }

        public static string[] Split(string text, char[] delimiters)
        {
            return Split(text, StringSplitOptions.None, delimiters);
        }

        public static string[] Split(string text,
            StringSplitOptions options = StringSplitOptions.None)
        {
            return Split(text, options, new char[] {','});
        }

        public static string[] Split(string text, StringSplitOptions options,
                                     char[] delimiters)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if (!Enum.IsDefined(typeof(StringSplitOptions), options))
            {
                throw new ArgumentException("options");
            }

            if (delimiters == null || delimiters.Length == 0)
            {
                delimiters = new char[] {' '};
            }

            var words = new List<string>();
            int startIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (delimiters.Contains(text[i]))
                {
                    if (i - startIndex > 0 ||
                        options == StringSplitOptions.None)
                    {
                        words.Add(text.Substring(startIndex, i - startIndex));
                    }
                    startIndex = i + 1;
                }
            }

            if (options == StringSplitOptions.None || startIndex != text.Length)
                words.Add(text.Substring(startIndex));
            
            return words.ToArray();
        }

    }
}

