using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{

    /*
NoobOS, Copyright (C) 2012-2013 NoobOS
NoobOS and its tools come with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/

    static class Extensions
    {
        #region Console Utils
        public static void Write2(string C, ConsoleColor c1)
        {
            Console.ForegroundColor = c1;
            Console.Write(C);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteLine2(string C, ConsoleColor c1)
        {
            Console.ForegroundColor = c1;
            Console.WriteLine(C);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static int indent = 0;

        /// <summary>
        /// Write Method
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="color">The color of the text</param>
        /// <param name="xcenter">Horizontal centered?</param>
        /// <param name="ycenter">Vertical centered?</param>
        public static void Write2(string text = "", ConsoleColor color = ConsoleColor.White, bool xcenter = false, bool ycenter = false)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            int X = Console.CursorLeft + indent;
            if (xcenter) Console.CursorLeft = ((Console.WindowWidth / 2) - (text.Length / 2));
            int Y = Console.CursorTop;
            if (ycenter) Console.CursorTop = ((Console.WindowHeight / 2) - 1);
            System.Console.Write(text);
            if (xcenter) Console.CursorLeft = X;
            if (ycenter) Console.CursorTop = Y;
            Console.ForegroundColor = originalColor;
        }
        #endregion

        #region String Utils

        /// <summary>
        /// Determinate if the string starts with the specified character.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool StartsWithEx(this string str, char ch)
        {
            if (str[0] == ch)
                return true;
            else
                return false;
        }

        public static bool StartsWithEx(this string str, string stri)
        {
            if (str[0] == 'a')
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determinate if the string ends with the specified character.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool EndsWithEx(this string str, char ch)
        {
            if (str[str.Length] == ch)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Splits the string at the index where first white space is found.
        /// </summary>
        /// <param name="source">the source string</param>
        /// <returns>splitted string array.</returns>
        public static String[] SplitAtFirstSpace(this String source)
        {
            if (String.IsNullOrEmpty(source)) return new String[] { };

            var index = source.IndexOf(' ');

            if (index == -1) return new String[] { source };

            return new String[] { source.Substring(0, index), source.Substring(index + 1) };
        }

        /// <summary>
        /// Compares two instances of string.
        /// </summary>
        /// <param name="source">the source string</param>
        /// <param name="target">the other instance.</param>
        /// <returns>true if the two instances are considered equal, false otherwise.</returns>
        public static bool IsEqual(this String source, String target)
        {
            if (source == null && target == null) return true; // both instances are null.

            if (String.IsNullOrEmpty(source) && String.IsNullOrEmpty(target)) return true; // both instances are empty.

            return source.ToLower() == target.ToLower();
        }

        public static bool _StartsWith(this string __str, string __expression)
        {
            string str = "";
            for (int i = 0; i < (__expression.Length); i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }
        public static bool _EndsWith(this string __str, string __expression)
        {
            string str = "";
            for (int i = ((__str.Length - 1) - (__expression.Length - 1)); i == (__str.Length - 1); i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }

        #endregion

        #region Collection Utils

        /// <summary>
        /// Applies the specified action for every element in the source collection.
        /// </summary>
        /// <typeparam name="T">type of element collection</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="action">action to be invoked on each element</param>
        public static void ForEeach(this Array source, Action<Object> action)
        {
            var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                action.Invoke(enumerator.Current);
            }
        }

        #endregion

        #region Numbers utils
        public static uint MsToHz(this int ms)
        {
            return (uint)(1000 / ms);
        }
        public static uint MsToHz(this uint ms)
        {
            return (uint)(1000 / ms);
        }
        #endregion
    }
}
