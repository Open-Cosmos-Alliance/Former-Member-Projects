using System;
using System.Text;

using WitchcraftOS.Witchcraftfx.Textscreen;
using WitchcraftOS.Witchcraftfx.LangManagement;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class ComExec
    {
        public static void Shutdown()
        {
            Console.Clear();
            ShutdownScreen.ShowShutdownScreen();
        }
        public static void EnterDesktop() { GUI.Screen.Initialize(); }
        public static void Print(string text) { Output.WriteLine(text.Remove(0, 5), ConsoleColor.Gray); }
        public static void ClearScreen() { Console.Clear(); Console.CursorTop = 2; }
        public static void PrintLn() { Output.WriteLine(); }
        public static void Help(string parameters = null)
        {
            if (parameters == null)
            {
                Console.Clear();
                Output.DrawLogoBar();
                Output.WriteLine("Witchcraft OS: " + Lang.Get("Quick Command-Reference"), Console.ForegroundColor, true);
                Output.WriteLine();
                PrintHelp("help", Lang.Get("Displays this Command-Reference"));
                PrintHelp("print [text]", Lang.Get("Displays the given text"));
                PrintHelp("print.", Lang.Get("Displays a new line (CrLf)"));
                PrintHelp("shutdown", Lang.Get("Sends the shutdown signal to the computer"));
                PrintHelp("debug", Lang.Get("Displays several debug informations"));
                PrintHelp("run [application]", Lang.Get("Executes an Witchcraft Applet"));
                PrintHelp("run clock", Lang.Get("Displays a clock"));
                Output.WriteLine();
                Output.WriteLine(Lang.Get("Press any key to continue"));
                Console.Read();
                Output.cls();
            }
        }
        public static void PrintHelp(string command, string description)
        {
            int max = 15;
            int descmax = 40;
            Console.CursorLeft = (Console.WindowWidth / 2) - (max / 2) - (descmax / 2);
            Console.Write(command);
            for (int i = 0; i < (max - command.Length); i++) Console.Write(" ");
            Console.Write(" | ");
            Console.Write(description);
            for (int i = 0; i < (descmax - description.Length); i++) Console.Write(" ");
            Console.WriteLine();
        }
        public static void DebugInfo()
        {
            Output.WriteLine("#");
            Output.WriteLine("# " + Lang.Get("Debug Informations"));
            Output.WriteLine("#");
            Output.WriteLine("# " + Lang.Get("Memory") + ": " + Helper.GetMemoryString() + "MB");
            Output.WriteLine("# " + Lang.Get("Language") + ": " + Lang.lang);
            bool acpi = PowerManagement.ACPI.Enable();
            if (acpi) Output.WriteLine("# ACPI: " + Lang.Get("Enabled"));
            else
            {
                Output.WriteLine("# ACPI: " + Lang.Get("Disabled") + " (Error)");
                Output.WriteLine("# ACPI: " + Lang.Get("It may not work to shut down correctly"));
            }
            Output.WriteLine("#");
            string datestring = Cosmos.Hardware.RTC.Month.ToString() + "." + Cosmos.Hardware.RTC.DayOfTheMonth.ToString() + "." + Cosmos.Hardware.RTC.Year.ToString("0000");
            if (Lang.lang == "de-DE") datestring = Cosmos.Hardware.RTC.DayOfTheMonth.ToString() + "." + Cosmos.Hardware.RTC.Month.ToString() + "." + Cosmos.Hardware.RTC.Year.ToString();
            string timestring = Cosmos.Hardware.RTC.Hour.ToString("00") + ":" + Cosmos.Hardware.RTC.Minute.ToString("00");
            Output.WriteLine("# " + Lang.Get("Date") + ": " + datestring);
            Output.WriteLine("# " + Lang.Get("Time") + ": " + timestring);
            Output.WriteLine("#");
        }
    }
}
