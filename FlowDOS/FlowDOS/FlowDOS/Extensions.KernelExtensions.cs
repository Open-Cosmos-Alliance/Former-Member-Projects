using System;

namespace FlowDOS
{
    /// <summary>
    /// Useful kernel extensions
    /// </summary>
    public static class KernelExtensions
    {
        //public static void Reboot(this Cosmos.System.Kernel krnl) { Core.ACPI.Reboot(); }
        //public static void Shutdown(this Cosmos.System.Kernel krnl) { Core.ACPI.Shutdown(); }
        //public static void SleepTicks(this Cosmos.System.Kernel krnl, int amount) { Time.SleepTicks(amount); }
        public static void SleepSeconds(this Cosmos.System.Kernel krnl, uint amount) { Time.Wait((int)amount); }
        public static uint GetMemory(this Cosmos.System.Kernel krnl) { return Cosmos.Core.CPU.GetAmountOfRAM() + 1; }
        //public static void ShowBootscreen(this Cosmos.System.Kernel krnl, string OSname, Bootscreen.Effect efx,
        //    ConsoleColor color, int ticks = 10000000) { Bootscreen.Show(OSname, efx, color, ticks); }
        public static void AllocMemory(this Cosmos.System.Kernel krnl, uint aLength) { Cosmos.Core.Heap.MemAlloc(aLength); }
    }
}
