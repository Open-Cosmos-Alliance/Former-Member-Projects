using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Commands.Commands
{
    class HelpCommand : CommandBase
    {
        public override string name()
        {
            return "help";
        }
        public override string shortname()
        {
            return "help";
        }
        public override string help()
        {
            return "Shows the help";
        }
        public override void execute(string[] args)
        {
            //hlp:
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("FlowDOS Help : Page 1/1");
            Console.WriteLine("-----------------------");
            Console.WriteLine("help: show this help");
            //Console.WriteLine("help <command>: shows help of a command");
            //Console.WriteLine("app <command>: run an app");
            Console.WriteLine("showgui: show graphical interface");
            //Console.WriteLine("beep: throw a beep");
            //Console.WriteLine("reboot: restart the computer");
            //Console.WriteLine("shutdown: shut down the computer");
            //Console.WriteLine("break, brk: do a CPU halt");
            Console.WriteLine("ls: list root directory");
            //Console.WriteLine("textedit: shows text editor");
            //Console.WriteLine("lolz: just types lolz for infinite if you get bored");
            //Console.WriteLine("lolz: WARNING IT WILL PROBABLY BURN OUT YOUR CPU!");
            Console.WriteLine("debughlp: show the development commands");
            Console.WriteLine("cls: clear screen");
            Console.WriteLine("info: show system informations");
            //Console.WriteLine("cbc <color name>: change background color(see 'clist')");
            //Console.WriteLine("cfc <color name>: change foreground color(see 'clist')");
            //Console.WriteLine("clist: list of colors(see 'cbc' & 'cfc')");
            Console.WriteLine("txedit: launch text editor");
            Console.WriteLine("txedit: type #end# to exit");
            Console.WriteLine("txedit: type #save# to save and exit");
            Console.BackgroundColor = ConsoleColor.Black;
            //client.Send(Encoding.Unicode.GetBytes("Hello world!"));
            // client.Close();
        }
    }
}
