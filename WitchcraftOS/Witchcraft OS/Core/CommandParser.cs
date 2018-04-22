using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bootscreen = WitchcraftOS.Witchcraftfx.Textscreen.BootScreen;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class CommandParser
    {
        public static void ParseCommand(string com)
        {
            com = com.Trim();
            string lcom = com.ToLower();

            if (lcom == "help") ComExec.Help();
            else if (lcom == "shutdown") ComExec.Shutdown();
            else if (lcom == "debug") ComExec.DebugInfo();
            else if (lcom == "print.") ComExec.PrintLn();
            else if (lcom._StartsWith("print ")) ComExec.Print(com);
            else if (lcom == "clear") ComExec.ClearScreen();
            else if (lcom == "desktop") ComExec.EnterDesktop();
            else if (lcom == "doomsday") { bootscreen.ShowStartScreen(); bootscreen.ExecuteDoomsdayBootScreen(); }
            else if (lcom == "run clock") Applications.TextScreen.Clock.Start();
        }
    }
}
