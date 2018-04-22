using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Core;

namespace FlowDOS.Computer
{
    class Memory
    {
        public static uint AvailableMemory { get { return CPU.GetAmountOfRAM() + 2; } }
    }
}
