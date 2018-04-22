using System;

using WitchcraftOS.Witchcraftfx.Textscreen;
using WitchcraftOS.Witchcraftfx.Threading;

namespace WitchcraftOS.Witchcraftfx.Textscreen
{
    public static class BootScreen
    {
        // Startscreen ( Animated! )
        public static void ShowStartScreen()
        {
            Console.Clear();
            Console.CursorTop = (Console.WindowHeight / 2) - 7;
            Output.WriteLine("Witchcraft OS", ConsoleColor.Magenta, true);

            // Animate "Made"
            for (int i = 0; i < ((Console.WindowHeight / 2) - 10); i++ )
            {
                Console.Clear();
                Console.CursorTop = i;
                Output.WriteLine(@"                           _____      ____ ", ConsoleColor.Green, true);
                Console.CursorTop = i;
                Output.WriteLine(@"    /\    /\       /\    ||     \\  ||     ", ConsoleColor.Green, true);
                Output.WriteLine(@"   //\\  //\\     //\\   ||      \\ ||____ ", ConsoleColor.Green, true);
                Console.CursorTop = i;
                Output.WriteLine(@"  //  \\//  \\   //__\\  ||      // ||     ", ConsoleColor.Green, true);
                Console.CursorTop = i;
                Output.WriteLine(@" //          \\ //    \\ ||_____//  ||____ ", ConsoleColor.Green, true);
                Sleep.SleepTicks(800000);
            }

            // Animate "By"
            for (int i = 0; i < ((Console.WindowWidth / 2) - 22); i++)
            {
                Console.Clear();
                Console.CursorTop = ((Console.WindowHeight / 2) - 10);
                Output.WriteLine(@"                           _____      ____ ", ConsoleColor.Green, true);
                Output.WriteLine(@"    /\    /\       /\    ||     \\  ||     ", ConsoleColor.Green, true);
                Output.WriteLine(@"   //\\  //\\     //\\   ||      \\ ||____ ", ConsoleColor.Green, true);
                Output.WriteLine(@"  //  \\//  \\   //__\\  ||      // ||     ", ConsoleColor.Green, true);
                Output.WriteLine(@" //          \\ //    \\ ||_____//  ||____ ", ConsoleColor.Green, true);
                Console.CursorTop++;
                Console.CursorLeft = i;
                Output.WriteLine(@"   ___    _    _ ", ConsoleColor.Cyan, false);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||   \\  \\  // ", ConsoleColor.Cyan, false);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||___//   \\//  ", ConsoleColor.Cyan, false);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||   \\    ||   ", ConsoleColor.Cyan, false);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||___//    ||   ", ConsoleColor.Cyan, false);
                Sleep.SleepTicks(800000);
            }

            // Animate "Marco Quinten"
            for (int i = Console.WindowWidth; i > ((Console.WindowWidth / 2) - (28 / 2)); i--)
            {
                Console.Clear();
                Console.CursorTop = ((Console.WindowHeight / 2) - 10);
                Console.CursorTop = i;
                Output.WriteLine(@"   ___    _    _ ", ConsoleColor.Cyan, true);
                Output.WriteLine(@" ||   \\  \\  // ", ConsoleColor.Cyan, true);
                Output.WriteLine(@" ||___//   \\//  ", ConsoleColor.Cyan, true);
                Output.WriteLine(@" ||   \\    ||   ", ConsoleColor.Cyan, true);
                Output.WriteLine(@" ||___//    ||   ", ConsoleColor.Cyan, true);
                Console.CursorTop++;
                Console.CursorLeft = i;
                Output.WriteLine(@" M A R C O    Q U I N T E N ", ConsoleColor.Yellow);
                Sleep.SleepTicks(500000);
            }
            Console.CursorTop = Console.WindowHeight - 3;
            Random rnd = new Random();
            int seed = rnd.Next(0, 8);

            // Draw Chuck Norris jokes ;)
            if (seed == 0) Output.WriteLine("There is no chin behind Chuck Norris' beard. There is only another fist.", ConsoleColor.White, true);
            else if (seed == 1) Output.WriteLine("Chuck Norris has already been to Mars;\nthat's why there are no signs of life there.", ConsoleColor.White, true);
            else if (seed == 2) Output.WriteLine("Chuck Norris Built Mount Everest with a bucket and a spade.", ConsoleColor.White, true);
            else if (seed == 3) Output.WriteLine("Chuck Norris made Ellen Degeneres straight.", ConsoleColor.White, true);
            else if (seed == 4) Output.WriteLine("Chuck Norris does not sleep. He waits.", ConsoleColor.White, true);
            else if (seed == 5) Output.WriteLine("Chuck Norris kicked Neo out of Zion , now Neo is \"The Two\"", ConsoleColor.White, true);
            else if (seed == 6) Output.WriteLine("Chuck Norris' iPod came with a real charger instead of just a USB cord", ConsoleColor.White, true);
            else if (seed == 7) Output.WriteLine("Chuck Norris is what Willis was talking about", ConsoleColor.White, true);

            // Wait 4 seconds
            Sleep.SleepSeconds(4);

            // Easter-Egg ( 21th december )
            if (Cosmos.Hardware.RTC.Month == 12 && Cosmos.Hardware.RTC.DayOfTheMonth == 21)
            {
                ExecuteDoomsdayBootScreen();
            }

            // Roll out to top
            for (int i = 0; i < (Console.WindowWidth * Console.WindowHeight); i++)
            {
                Output.Write(" ", ConsoleColor.Black);
                Sleep.SleepTicks(20000);
            }
            return;
        }

        // Easter-Egg ( 21th december )
        public static void ExecuteDoomsdayBootScreen()
        {
            Console.Clear();
            Console.CursorTop = (Console.WindowHeight / 2) - 2;
            Output.WriteLine("Witchcraft OS won't work on doomsday", ConsoleColor.Magenta, true);
            Console.CursorTop = (Console.WindowHeight / 2);
            for (int i = 0; i < 100; i++)
            {
                Output.Write("WITCHES!");
                Sleep.SleepTicks(1000000);
            }
            PowerManagement.ACPI.Shutdown();
        }
    }
}
