/* Copyright (C) 2013 GruntXProductions
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


// Well I just noticed something, I need to switch back to real mode if I want this to 'truely'
// emulate DOS programs..... Maybe make a new 'MZ32'? nah... I will get around to that at some
// point but a PE loader would probally be better......

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Cosmos.IL2CPU.Plugs;
using CPUx86 = Cosmos.Assembler.x86;
namespace Quicksilver.Executable
{

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    unsafe struct MZ_Header
    {
        [FieldOffset(0)]
        public ushort Magic;
        [FieldOffset(2)]
        public ushort ExtraBytes;
        [FieldOffset(4)]
        public ushort Pages;
        [FieldOffset(6)]
        public ushort RelocationItems;
        [FieldOffset(8)]
        public ushort HeaderSize;
        [FieldOffset(10)]
        public ushort MinAlloc;
        [FieldOffset(12)]
        public ushort MaxAlloc;
        [FieldOffset(14)]
        public ushort InitialSS;
        [FieldOffset(16)]
        public ushort InitialSP;
        [FieldOffset(18)]
        public ushort Checksum;
        [FieldOffset(20)]
        public ushort Entry;
        [FieldOffset(22)]
        public ushort IntialCP;
        [FieldOffset(24)]
        public ushort RelocationTab;

    }
    class RelocationInfo
    {
        public uint Offest;
        public uint Segment;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    struct RelocationStruct
    {
        [FieldOffset(0)]
        public uint Offest;
        [FieldOffset(4)]
        public uint Segment;
    }
    public unsafe class MZ_EXE
    {
        private byte[] code;
        private MZ_Header* header;
        private byte* rel_ptr;
        private List<RelocationInfo> symbolsToRelocate = new List<RelocationInfo>();
        public MZ_EXE(string file)
        {
            code = Kernel.fs.readFile(file);

            fixed (byte* ptr = code)
            {
                header = (MZ_Header*)ptr;
                rel_ptr = (byte*)((uint)ptr + (uint)header->RelocationTab);
            }
            for (int i = 0; i < header->RelocationItems; i++)
            {
                RelocationStruct* rs = (RelocationStruct*)(i * 8);
                RelocationInfo ri = new RelocationInfo();
                ri.Offest = rs->Offest;
                ri.Segment = rs->Segment;
                symbolsToRelocate.Add(ri);
            }

        }
        private void Relocate(uint offest)
        {
            GruntyOS.IO.BinaryWriter bw = new GruntyOS.IO.BinaryWriter(new GruntyOS.IO.MemoryStream(code));
            GruntyOS.IO.BinaryReader br = new GruntyOS.IO.BinaryReader(new GruntyOS.IO.MemoryStream(code));
            for (int i = 0; i < symbolsToRelocate.Count; i++)
            {
                br.BaseStream.Position = (int)symbolsToRelocate[i].Offest;
                ushort val = BitConverter.ToUInt16(new byte[] { br.ReadByte(), br.ReadByte() }, 0);
            }
        }
        public void Execute()
        {
            Caller c = new Caller();
            c.CallCode(header->Entry); // Jump the start!

        }
    }
    // G-DOS license ends here........
}
