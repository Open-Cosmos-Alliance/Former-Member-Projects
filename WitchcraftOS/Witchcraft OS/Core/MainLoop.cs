using System;

using WitchcraftOS.Witchcraftfx;
using WitchcraftOS.Witchcraftfx.Textscreen;
using WitchcraftOS.Witchcraftfx.LangManagement;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class MainLoop
    {
        public static bool showshell = true;
        public static string KernelVersion = "0.36a";
        // Boot
        public static void Start()
        {
            BootScreen.ShowStartScreen();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            LanguageSelector.ShowLanguageDialog();
            Console.Clear();
        }
        // Main
        public static void Main()
        {
            Console.CursorTop = 2;
            while (true)
            {
                Output.DrawLogoBarReal();
                if (showshell) Output.Write("Witchcraft$> ");
                CommandParser.ParseCommand(Console.ReadLine());
            }
        }
    }
}