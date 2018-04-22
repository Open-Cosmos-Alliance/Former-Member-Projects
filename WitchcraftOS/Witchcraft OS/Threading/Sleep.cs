using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchcraftOS.Witchcraftfx.Threading
{
    public static class Sleep
    {
        public static void SleepTicks(int value)
        {
            for (int i = 0; i < value; i++) { ;}
            return;
        }
        public static void SleepSeconds(int value)
        {
            int start = Cosmos.Hardware.RTC.Second; int end;
            if (start + value > 59) end = 0;
            else end = start + value;
            while (Cosmos.Hardware.RTC.Second != end) { ;}
        }
    }
}
