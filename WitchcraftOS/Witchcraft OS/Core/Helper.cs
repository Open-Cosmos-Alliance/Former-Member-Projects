using System;
using sys = Cosmos.System;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class Helper
    {
        // Returns the amount of available Memory
        public static int GetMemory() { return (int)Cosmos.Core.CPU.GetAmountOfRAM() + 1; }

        // Returns the amount of available Memory as string
        public static string GetMemoryString() { return GetMemory().ToString(); }

        // Returns the current Time as string
        public static string GetTimeString(bool displaySeconds)
        {
            string ret;
            byte h = Cosmos.Hardware.RTC.Hour;
            byte m = Cosmos.Hardware.RTC.Minute;
            byte s = Cosmos.Hardware.RTC.Second;
            string hstr = string.Empty;
            string mstr = string.Empty;
            string sstr = string.Empty;
            if (h < 10) hstr = "0";
            hstr += h.ToString();
            if (m < 10) mstr = "0";
            mstr += m.ToString();
            if (s < 10) sstr = "0";
            sstr += s.ToString();
            ret = hstr + ":" + mstr;
            if (displaySeconds) ret += ":" + sstr;
            return ret;
        }
    }
}
