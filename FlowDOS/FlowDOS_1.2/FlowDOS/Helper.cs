using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Helper
    {
        public static void WriteInfo(string value)
        {
            Console.WriteLine("[info] " + value);
        }

        public static void WriteError(string value)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("error");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("] " + value);
        }

        public static string GetString(byte[] value)
        {
            string s = "";
            for (int i = 0; i < value.Length; i++)
            {
                s += (char)value[i];
            }
            return s;
        }

        public static byte[] GetBytes(string value)
        {
            byte[] s = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                s[i] = (byte)value[i];
            }
            return s;
        }

        public static string GetStringFromCharArray(char[] value)
        {
            string s = "";
            for (int i = 0; i < value.Length; i++)
            {
                s += value[i];
            }
            return s;
        }

        public static char[] GetCharArrayFromString(string value)
        {
            char[] s = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                s[i] = value[i];
            }
            return s;
        }
    }
}
