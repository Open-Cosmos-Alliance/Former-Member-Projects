using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.IL2CPU.Plugs;
using Assembler = Cosmos.Assembler.Assembler;
using CPUx86 = Cosmos.Assembler.X86;

namespace Optimus
{
    public abstract class BusIO
    {
        //TODO: Reads and writes can use this to get port instead of argument
        static protected void Write8(UInt16 aPort, byte aData) { } // Plugged
        static protected void Write16(UInt16 aPort, UInt16 aData) { } // Plugged
        static protected void Write32(UInt16 aPort, UInt32 aData) { } // Plugged

        static protected byte Read8(UInt16 aPort) { return 0; } // Plugged
        static protected UInt16 Read16(UInt16 aPort) { return 0; } // Plugged
        static protected UInt32 Read32(UInt16 aPort) { return 0; } // Plugged
    }


}
