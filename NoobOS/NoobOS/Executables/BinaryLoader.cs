/* BinaryLoader.cs - Loads raw binaries.
 * Copyright (C) 2012-2013 NoobOS
 *
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPUAll = Cosmos.Assembler;
using CPUx86 = Cosmos.Assembler.x86;
using Sys = Cosmos.System;
using Cosmos.IL2CPU.Plugs;
namespace NoobOS.Executables
{
        public static class BinaryLoader
        {
            public static uint Address;
            public static void CallRaw(byte[] aData)
            {
                unsafe
                {
                    byte* data = (byte*)Cosmos.Core.Heap.MemAlloc((uint)aData.Length);
                    Address = (uint)&data[0];
                    for (int i = 0; i < aData.Length; i++)
                    {
                        data[i] = aData[i];
                    }   
                    Caller call = new Caller();
                    call.CallCode((uint)&data[0]);
                }
            }
            #region Plug
            public class Caller
            {
                [PlugMethod(Assembler = typeof(CallerPlug))]
                public void CallCode(uint address) { } //Plugged
            }
            [Plug(Target = typeof(Caller))]
            public class CallerPlug : AssemblerMethod
            {
                public override void AssembleNew(object aAssembler, object aMethodInfo)
                {
                    new CPUAll.Comment("NoobBinaryLoader. (C) NoobOS 2013. Licensed under the GNU GPL where applicable.");
                    new CPUx86.Mov { SourceReg = CPUx86.Registers.EBP, SourceDisplacement = 8, SourceIsIndirect = true, DestinationReg = CPUx86.Registers.EAX };
                    new CPUx86.Call { DestinationReg = CPUx86.Registers.EAX };
                }
            }
            #endregion
        }
}
