using System;
using sys = Cosmos.System;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class StartupHelper
    {
        // Returns the amount of available Memory
        public static int GetMemory() { return (int)Cosmos.Core.CPU.GetAmountOfRAM() + 1; }
        // Returns the amount of available Memory as string
        public static string GetMemoryString() { return GetMemory().ToString(); }
        // Displays the Startscreen
    }
}
