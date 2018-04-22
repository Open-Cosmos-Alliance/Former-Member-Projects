using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Cons
    {
        public static void Write(object value, ConsoleColor Fore = ConsoleColor.White, ConsoleColor Back = ConsoleColor.Black)
        {
            Console.ForegroundColor = Fore;
            Console.BackgroundColor = Back;
            Console.Write(value);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void WriteLine(object value, ConsoleColor Fore = ConsoleColor.White, ConsoleColor Back = ConsoleColor.Black)
        {
            Console.ForegroundColor = Fore;
            Console.BackgroundColor = Back;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
