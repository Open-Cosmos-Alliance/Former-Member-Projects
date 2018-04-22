using System;

using Lang = WitchcraftOS.Witchcraftfx.LangManagement.Lang;

namespace WitchcraftOS.Witchcraftfx.Textscreen
{
    public static class LanguageSelector
    {
        public static void ShowLanguageDialog()
        {
            int choice = 0;
            bool _break = false;
            do
            {
                Console.Clear();
                Output.DrawLogoBar();
                Output.WriteLine("--------------- Witchcraft Language Selector ---------------", Console.ForegroundColor, true);
                Output.WriteLine("Navigate using the arrow keys", Console.ForegroundColor, true);
                Output.WriteLine();
                if (choice == 0)
                {
                    Output.WriteLine("[en-US] English", ConsoleColor.Blue, true);
                    Output.WriteLine("[de-DE] Deutsch", ConsoleColor.White, true);
                }
                if (choice == 1)
                {
                    Output.WriteLine("[en-US] English", ConsoleColor.White, true);
                    Output.WriteLine("[de-DE] Deutsch", ConsoleColor.Blue, true);
                }
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    if (choice == 0) Lang.lang = "en-US";
                    else if (choice == 1) Lang.lang = "de-DE";
                    Console.Clear();
                    _break = true;
                }
                if (key == ConsoleKey.DownArrow || key == ConsoleKey.UpArrow)
                {
                    if (choice == 0) choice = 1;
                    else if (choice == 1) choice = 0;
                }
            } while (!_break);
            return;
        }
    }
}
