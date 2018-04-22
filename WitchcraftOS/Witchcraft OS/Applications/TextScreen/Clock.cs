using System;

using WitchcraftOS.Witchcraftfx.Textscreen;

namespace WitchcraftOS.Witchcraftfx.Applications.TextScreen
{
    public static class Clock
    {
        public static void Start()
        {
            do
            {
                ConsoleColor c = ConsoleColor.White;
                Console.CursorTop = ((Console.WindowHeight / 2) - 4);
                Output.WriteLine("  _________________________  ", c, true);
                Output.WriteLine("//                         \\", c, true);
                Output.WriteLine("||                         ||", c, true);
                Output.WriteLine("||                         ||", c, true);
                Output.WriteLine("||                         ||", c, true);
                Output.WriteLine("||                         ||", c, true);
                Output.WriteLine("||                         ||", c, true);
                Output.WriteLine("\\_________________________//", c, true);
                Console.CursorTop -= 4;
                Output.WriteLine(Core.Helper.GetTimeString(true), c, true);
                Console.CursorTop += 6;
                Output.WriteLine(LangManagement.Lang.Get("Press Q to close the application"), c, true);
            } while (Cosmos.Hardware.Global.Keyboard.ReadKey() != ConsoleKey.Q);
        }
    }
}
