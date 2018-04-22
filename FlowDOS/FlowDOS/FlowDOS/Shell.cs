using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;

namespace FlowDOS
{

    /*
NoobOS, Copyright (C) 2012-2013 NoobOS
NoobOS and its tools come with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/

    class Shell
    {


        /// <summary>
        /// Starts a new infinite loop that represents a simple shell
        /// </summary>
        public static void StartShell()
        {
            Console.WriteLine("Starting Shell", ConsoleColor.Green);
            String cmd = "";
            do
            {
                /*Account u = new Account(GlobalEnvironment.Current["USER"]);
                String h = GlobalEnvironment.Current["HOST"];
                NoobDirectory dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);*/
                String consoleline = "root" + "@" + "flowdos" + ":" + "/sys/" + "# ";
                //Console.WriteLine("" + Heap.GetFreeMemory());
                Console.Write(consoleline);

                cmd = Console.ReadLine();
                String[] t = cmd.SplitAtFirstSpace();
                String c = t[0]; // Command
                String a = t.Length > 1 ? t[1] : String.Empty; // Argument(s)

                //Boolean p = FlowDOS.Commands.CommandsManager.ProcessCommand(c, a);

                //if (!p) Console.WriteLine("No such command found!");

             
            }
            while (true);
        }
    }
}
