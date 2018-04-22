using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class BSOD
    {
        public static void Panic(string error)
        {
            /* Old code
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine();
            Extensions.Write2("FlowDOS", ConsoleColor.White, true, false);
            Console.WriteLine();
            Extensions.Write2("An error occured and FlowDOS has been shut down to prevent damage to your files.", ConsoleColor.White, true, false);
            Console.WriteLine();
            Extensions.Write2(error, ConsoleColor.White, true, false);
            Console.WriteLine();*/

            // From G-DOS
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            // The cosmos console class sucks, I should have rewritten it like in Grunty OS
            // but using a different class for output would create confusion so I decieded
            // not too
            for (int i = 0; i < (80 * 26); i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.WriteLine("A problem has been detected and FlowDOS has been shut down to prevent damage to your computer.\n");

            Console.WriteLine(error);
            Console.WriteLine(@"
If this is the first time you've seen this Stop error screen, 
restart you computer. If this screen appears again follow
these steps:

Check to make sure any new hardware is properly installed.
If this is a new installation, check your hardware to see if it is 
compatible with your computer's BIOS.

If problems continue, disable or remove any newly installed hardware. 
Disable BIOS memory options such as caching or shadowing.");
            while (true) ;
        }
    }
}
