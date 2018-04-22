 /*
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/


/* No. This does not mean you can run DOS COM files, DOS COM files are 16 bit, this
 * a 32 bit OS. Also DOS COM files rely on the DOS API which can only be emulated
 * in real mode because int21h is used for the keyboard.... */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Cosmos.IL2CPU.Plugs;
using CPUx86 = Cosmos.Assembler.x86;

namespace Quicksilver.Executable
{
    public unsafe class COM
    {
        private byte[] code;
        public COM(string file)
        {
            code = Kernel.fs.readFile(file);
        }
        public COM(byte[] src)
        {
            code = src;
        }
        public void Execute()
        {
            /* This might be overwritting something, but since we do not have paging working
             * there is really no 'better' alternative. I have test this though and I have
             * not noticed any bad side effects so I will assume this is somewhat safe...
             */
            byte* ptr = (byte*)0x100;
            for (int i = 0; i < code.Length; i++)
            {
                ptr[i] = code[i];
            }
            Caller c = new Caller();
            c.CallCode(0x100); // Jump!!!!!
        }
    }
    // G-DOS license ends here........
    // This is by Aurora01, a better alternative than the hacks that Grunty OS uses
    // I could have just wrote this by my self, but I was to lazy......
    // NoobBinaryLoader. (C) NoobOS 2013. Licensed under the GNU GPL where applicable.
    public class Caller
    {
        [PlugMethod(Assembler = typeof(CallerPlug))]
        public void CallCode(uint address) { }
    }
    [Plug(Target = typeof(Caller))]
    public class CallerPlug : AssemblerMethod
    {
        public override void AssembleNew(object aAssembler, object aMethodInfo)
        {
            new CPUx86.Mov { SourceReg = CPUx86.Registers.EBP, SourceDisplacement = 8, SourceIsIndirect = true, DestinationReg = CPUx86.Registers.EAX };
            new CPUx86.Call { DestinationReg = CPUx86.Registers.EAX };
        }
    }
}