using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuicksilverNEXT
{
    class Console
    {
        static string buffer = "";
        public static void Write(string par0) {
            buffer += par0;
        }
        public static void WriteLine(string par0) {
            buffer += par0 + "\r\n";
        }
        public static void Flush() {
            System.Console.Write(buffer);
            buffer = "";
        }
    }
}
