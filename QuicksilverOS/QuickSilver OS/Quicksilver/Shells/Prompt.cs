using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Shells
{
    public class Prompt : Shell
    {
        string command;
        public override void Run()
        {
            QuicksilverNEXT.Console.Write(UserService.user + "@" + Kernel.cd + "# ");
            QuicksilverNEXT.Console.Flush();
            command = Console.ReadLine();
            Parser.Parse(command);
        }
    }
}
