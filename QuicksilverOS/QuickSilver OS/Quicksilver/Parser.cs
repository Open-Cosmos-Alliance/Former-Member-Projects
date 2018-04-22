using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using x86= Cosmos.Assembler.x86;

namespace Quicksilver
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
            add(new commandBase("sysinfo", new commandBase.command(commanddel.meminfo)));
            add(new commandBase("file", new commandBase.command(commanddel.files)));
            add(new commandBase("clear", new commandBase.command(delegate(List<string> args) { Console.Clear(); })));
            add(new commandBase("qte", new commandBase.command(delegate(List<string> args) { Kernel.current = new Shells.QTE(); })));
            add(new commandBase("fdisk", new commandBase.command(commanddel.fdisk)));
            add(new commandBase("mkfs", new commandBase.command(delegate(List<string> args) {  })));
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
            commands[9].sethelp("file: Opens file\r\nUsage: file @filename, file @path");
            commands[10].sethelp("clear: Clears the console\r\nUsage: clear");
            commands[11].sethelp("fdisk: Makes partitions\r\nUsage: fdisk");
            commands[12].sethelp("mkfs: Formats partition\r\nUsage: mkfs @disk @label @filesystem");
        }
        public static void add(commandBase com) {
            commands[icommand] = com;
            icommand++;
        }
        public static void Parse(string text) {
            bool didfind = false;
            List<string> tokens = Tokenizer.getTokens(text);
            for (int i = 0; i < commands.Length; i++ )
            {
                if (tokens[0].ToLower() == commands[i].text)
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
            executec(Tokenizer.getTokens(s));
        }
        public string gethelp() {
            return _help;
        }
        public void sethelp(string help) {
            _help = help;
        }
        public command executec;
        public string text;
        public delegate void command(List<string> args);
    }
    public static class commanddel {
        public static void nothing(List<string> args) {
        }
        public static void echo(List<string> args) {
            Console.WriteLine(args[1]);
        }
        public static void tryc(List<string> args) {
            string s = "";
            for(int i = 1; i < args.Count; i++) {
                s += args[i] + " ";
            }
            try { Parser.Parse(s); }
            catch { Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("There was an error with the command " + args[1]); Console.ForegroundColor = ConsoleColor.White; }
        }
        public static void add(List<string> args) {
            Console.WriteLine((int.Parse(args[1]) + int.Parse(args[2])));
        }
        public static void sub(List<string> args)
        {
            Console.WriteLine((int.Parse(args[1]) - int.Parse(args[2])));
        }
        public static void mul(List<string> args)
        {
            Console.WriteLine((int.Parse(args[1]) * int.Parse(args[2])));
        }
        public static void div(List<string> args)
        {
            Console.WriteLine((int.Parse(args[1]) / int.Parse(args[2])));
        }
        public static void meminfo(List<string> args)
        {
            Console.WriteLine((Cosmos.Core.CPU.GetAmountOfRAM() * 1024 * 1024) + " bytes of RAM");
        }
        public static void files(List<string> args)
        {
            if (args[1].Substring(args[1].LastIndexOf('.')) == "exe") { /*new Quicksilver.Executable.PE32(Kernel.cd + "/" + args[1]);*/ }
            FileXT.file(args[1]);
        }
        public static void fdisk(List<string> args)
        {
            new fdisk().Execute(new string[]{""});
        }
        public static void cd(List<string> args) {
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
        public unsafe static void cpuid(List<string> args) {
            //var pi = Quicksilver.cpuid.pi;
            //Console.WriteLine(pi.CoreCount + " cores at " + pi.MaxSpeed + " MHz");
            new Cosmos.Assembler.x86.Push { DestinationReg = x86.Registers.EAX };
            new Cosmos.Assembler.x86.Mov { DestinationReg = Cosmos.Assembler.x86.Registers.EAX, SourceValue = 0 };
            new x86.CpuId { };
            var i = 0U;
            //new x86.Mov { SourceReg = x86.Registers.EAX, DestinationValue = (uint?)(&i) };
            new x86.Pop { DestinationReg = x86.Registers.EAX };
            Console.Write(i);
        }
        public static void help(List<string> args)
        {
            if (args.Count == 1) {
                Console.WriteLine("Quicksilver OS Alpha 1.0.0\r\nCommands: echo, try, add, sub, mul, div, help, cpuid, cd.");
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