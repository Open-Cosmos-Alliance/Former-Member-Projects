using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
   class Funcs
    {
        public static void Wait(uint milliseconds)
        {
            Cosmos.Hardware.PIT pit = new Cosmos.Hardware.PIT();
            pit.Wait(milliseconds);
        }

        public static void WaitSecs(int seconds)
        {
            Wait((uint)seconds * 1000);
        }
    }
}
