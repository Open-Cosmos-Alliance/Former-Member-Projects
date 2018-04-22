using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Memory
{
    class MemAlloc
    {
        public static void Alloc(uint size)
        {
            Cosmos.Core.Heap.MemAlloc(size);
        }
    }
}
