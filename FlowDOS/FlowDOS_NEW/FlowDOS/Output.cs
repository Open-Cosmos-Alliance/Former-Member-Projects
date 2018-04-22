using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    public static class Output
    {
        public static void Write(string text = null, ConsoleColor color = ConsoleColor.White, bool xcenter = false, bool ycenter = false)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            int X = Console.CursorLeft;
            if (xcenter) Console.CursorLeft = ((Console.WindowWidth / 2) - (text.Length / 2));
            int Y = Console.CursorTop;
            if (ycenter) Console.CursorTop = ((Console.WindowHeight / 2) - 1);
            Console.Write(text);
            if (xcenter) Console.CursorLeft = X;
            if (ycenter) Console.CursorTop = Y;
            Console.ForegroundColor = originalColor;
        }
        public static void WriteLine(string text = null, ConsoleColor color = ConsoleColor.White, bool xcenter = false, bool ycenter = false)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            int X = Console.CursorLeft;
            if (xcenter) Console.CursorLeft = ((Console.WindowWidth / 2) - (text.Length / 2));
            int Y = Console.CursorTop;
            if (ycenter) Console.CursorTop = ((Console.WindowHeight / 2) - 1);
            Console.WriteLine(text);
            if (xcenter) Console.CursorLeft = X;
            if (ycenter) Console.CursorTop = Y;
            Console.ForegroundColor = originalColor;
        }
        public static void cls() { Console.Clear(); Console.CursorTop = 2; }
        public static void FilterAndPrintMessage(string unfilteredmessage, bool resetcolor = true)
        {
            ConsoleColor forecolor = Console.ForegroundColor;
            string[] messagepieces = unfilteredmessage.Split('@');
            foreach (string msg in messagepieces)
            {
                if (msg.Contains("@BLACK"))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    msg.Replace("@BLACK", "");
                }
                else if (msg.Contains("@GREEN"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    msg.Replace("@GREEN", "");
                }
                Console.Write(msg);
            }
            if (resetcolor) Console.ForegroundColor = forecolor;
        }
        /*public static void DrawLogoBar()
        {
            Output.WriteLine("Witchcraft OS v" + Core.MainLoop.KernelVersion, ConsoleColor.Magenta, true);
            Output.WriteLine();
        }*/
        // Wipe out the first two lines and draw the logo bar
        /*public static void DrawLogoBarReal()
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            for (int i = 0; i < 2; i++)
            {
                Console.CursorTop = i;
                for (int ix = 0; ix < Console.WindowWidth; ix++) Output.Write(" ");
            }
            Console.CursorTop = 0;
            Output.WriteLine("Witchcraft OS v" + Core.MainLoop.KernelVersion, ConsoleColor.Magenta, true);
            Console.CursorTop = top;
            Console.CursorLeft = left;
        }*/
    }
}
