using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchcraftOS.Witchcraftfx.Textscreen
{
    public static class ShutdownScreen
    {
        // Animated Shutdownscreen =)
        public static void ShowShutdownScreen()
        {
            for (int i = -24; i < Console.WindowWidth - 24; i++)
            {
                Console.Clear();
                Console.CursorTop = ((Console.WindowHeight / 2) - 3);
                Console.CursorLeft = i;
                Output.WriteLine(@"   ___    _    _   ___ ", ConsoleColor.Cyan);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||   \\  \\  // ||    ", ConsoleColor.Cyan);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||___//   \\//  ||___ ", ConsoleColor.Cyan);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||   \\    ||   ||    ", ConsoleColor.Cyan);
                Console.CursorLeft = i;
                Output.WriteLine(@" ||___//    ||   ||___ ", ConsoleColor.Cyan);
                Threading.Sleep.SleepTicks(2000000);
            }
            PowerManagement.ACPI.Shutdown();
        }
    }
}
