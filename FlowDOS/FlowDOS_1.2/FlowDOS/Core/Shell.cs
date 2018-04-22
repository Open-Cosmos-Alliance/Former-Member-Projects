using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Core
{
    class Shell
    {
        public void Init()
        {
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Global.CurrentUser.Name);
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(Global.CurrentDirectory);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" $ ");
                Console.ForegroundColor = ConsoleColor.White;
                string a = Console.ReadLine();
                if (a == "hibernate")
                {
                    Power.Hibernate();
                }
                else
                {
                    //string[] args = null;
                    string command;
                    command = a.Split(' ')[0];
                    //args = (a.Replace(command + ' ', "")).Split(' ');
                    //Console.WriteLine(command); <-|-That's debug things ^^ 
                    //Console.WriteLine(args[0]); <-|
                    Console.WriteLine(command);
                    Applications.Manager.Run(command, null, Global.CurrentUser);
                }

            }
        }
    }
}
