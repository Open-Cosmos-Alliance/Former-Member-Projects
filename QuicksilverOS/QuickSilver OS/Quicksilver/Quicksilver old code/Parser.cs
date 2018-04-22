using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver2013
{
    static class Parser
    {
        static commandBase[] commands = new commandBase[24];
        static int icommand = 0;
        public static void Init()
        {
            add(new commandBase("echo", new commandBase.command(commanddel.echo)));
            add(new commandBase("try", new commandBase.command(commanddel.tryc)));
            add(new commandBase("add", new commandBase.command(commanddel.add)));
            add(new commandBase("help", new commandBase.command(commanddel.help)));
            add(new commandBase("sub", new commandBase.command(commanddel.sub)));
            add(new commandBase("mul", new commandBase.command(commanddel.mul)));
            add(new commandBase("div", new commandBase.command(commanddel.div)));
            add(new commandBase("cpuid", new commandBase.command(commanddel.cpuid)));
            //add(new commandBase("cd", new commandBase.command(commanddel.cd)));
            add(new commandBase("sysinfo", new commandBase.command(commanddel.meminfo)));
            add(new commandBase("file", new commandBase.command(commanddel.files)));
            commands[0].sethelp("echo: Prints a string to the console\r\nUsage: echo @s");
            commands[1].sethelp("try: Catches errors in commands\r\nUsage: try @command @args");
            commands[2].sethelp("add: Adds two numbers together\r\nUsage: add @n @n");
            commands[3].sethelp("help: Gives help topics on a command or the os\r\nUsage: help, help @command");
            commands[4].sethelp("sub: Gets the difference of two numbers\r\nUsage: sub @n @n");
            commands[5].sethelp("mul: Multiplies two numbers together\r\nUsage: mul @n @n");
            commands[6].sethelp("div: Divides one number by another\r\nUsage: div @n @n");
            commands[7].sethelp("cpuid: Gives info about the cpu\r\nUsage: cpuid");
            //commands[8].sethelp("cd: Changes the current directory\r\nUsage: cd @directory, cd @path");
            commands[8].sethelp("sysinfo: Gives info about the system\r\nUsage: sysinfo");
        }
        public static void add(commandBase com) {
            commands[icommand] = com;
            icommand++;
        }
        public static void Parse(string text) {
            bool didfind = false;
            for (int i = 0; i < commands.Length; i++ )
            {
                if (text.Split(' ')[0].ToLower() == commands[i].text)
                {
                    commands[i].execute(text);
                    didfind = true;
                    break;
                }
            }
            if(!didfind) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Command not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static commandBase getCommand(string text) {
            for (int i = 0; i < commands.Length; i++) {
                if (text.Split(' ')[0].ToLower() == commands[i].text) {
                    return commands[i];
                }
            }
            return new commandBase("null", new commandBase.command(commanddel.nothing));
        }
        public static string getCommandHelp(string text) {
            for (int i = 0; i < commands.Length; i++) {
                if (text.Split(' ')[0].ToLower() == commands[i].text) {
                    return commands[i].gethelp();
                }
            }
            return "Command not found";
        }
    }
    class commandBase
    {
        string _help = "";
        public commandBase(string _text, command _command) {
            executec = _command;
            text = _text;
        }
        public void execute(string s){
            executec(s.Split(' '));
        }
        public string gethelp() {
            return _help;
        }
        public void sethelp(string help) {
            _help = help;
        }
        public command executec;
        public string text;
        public delegate void command(string[] args);
    }
    public static class commanddel {
        public static void nothing(string[] args) {
        }
        public static void echo(string[] args) {
            Console.WriteLine(args[1]);
        }
        public static void tryc(string[] args) {
            string s = "";
            for(int i = 1; i < args.Length; i++) {
                s += args[i] + " ";
            }
            try { Parser.Parse(s); }
            catch { Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("There was an error with the command " + args[1]); Console.ForegroundColor = ConsoleColor.White; }
        }
        public static void add(string[] args) {
            Console.WriteLine((int.Parse(args[1]) + int.Parse(args[2])));
        }
        public static void sub(string[] args)
        {
            Console.WriteLine((int.Parse(args[1]) - int.Parse(args[2])));
        }
        public static void mul(string[] args)
        {
            Console.WriteLine((int.Parse(args[1]) * int.Parse(args[2])));
        }
        public static void div(string[] args)
        {
            Console.WriteLine((int.Parse(args[1]) / int.Parse(args[2])));
        }
        public static void meminfo(string[] args)
        {
            Console.WriteLine((Cosmos.Core.CPU.GetAmountOfRAM() * 1024 * 1024) + " bytes of RAM");
        }
        public static void files(string[] args)
        {
            if (args[1].Substring(args[1].LastIndexOf('.')) == "exe") { new Quicksilver2013.Executable.PE32(Kernel.cd + "/" + args[1]); }
            FileXT.file(args[1]);
        }
        public static void cd(string[] args) {
            try
            {
                //if (args[1][0] == '/') Kernel.cd = args[1];
                //if (args[1][0] != '/')/* if (Kernel.FileSystem.ListDirectories(Kernel.cd).Contains(args[1]))*/ Kernel.cd = GruntyOS.String.Util.cleanName(Kernel.cd) + "/" + args[1];
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid Args");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void cpuid(string[] args) {
            var pi = Quicksilver2013.cpuid.pi;
            Console.WriteLine(pi.CoreCount + " cores at " + pi.MaxSpeed + " MHz");
        }
        public static void help(string[] args)
        {
            if (args.Length == 1) {
                Console.WriteLine("Quicksilver OS Alpha 1.0.0.23\r\nCommands: echo, try, add, sub, mul, div, help, cpuid, cd.");
            }
            else {
                string help = Parser.getCommandHelp(args[1]);
                if(help == "Command not found") {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                Console.WriteLine(help);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}