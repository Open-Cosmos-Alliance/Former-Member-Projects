/*Copyright (C) 2013 GruntXProductions
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

// PE is the executable format used by windows... 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Quicksilver.Executable
{
    public unsafe class PE32
    {
        public byte[] text;
        public byte[] data;
        private static List<Section> sections = new List<Section>();
        public class Section
        {
            public uint Address, Size, RelocationPtr, RelocationCount;
            public string Name;
        }
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        unsafe struct PeHeader
        {

            [FieldOffset(0)]
            public uint mMagic;
            [FieldOffset(4)]
            public ushort mMachine;
            [FieldOffset(6)]
            public ushort mNumberOfSections;
            [FieldOffset(8)]
            public uint mTimeDateStamp;
            [FieldOffset(12)]
            public uint mPointerToSymbolTable;
            [FieldOffset(16)]
            public uint mNumberOfSymbols;
            [FieldOffset(20)]
            public ushort mSizeOfOptionalHeader;
            [FieldOffset(22)]
            public ushort mCharacteristics;

        }
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        unsafe struct Pe32OptionalHeader
        {
            [FieldOffset(0)]
            public ushort mMagic; // 0x010b - PE32, 0x020b - PE32+ (64 bit)
            [FieldOffset(2)]
            public byte mMajorLinkerVersion;
            [FieldOffset(3)]
            public byte mMinorLinkerVersion;
            [FieldOffset(4)]
            public uint mSizeOfCode;
            [FieldOffset(8)]
            public uint mSizeOfInitializedData;
            [FieldOffset(12)]
            public uint mSizeOfUninitializedData;
            [FieldOffset(16)]
            public uint mAddressOfEntryPoint;
            [FieldOffset(20)]
            public uint mBaseOfCode;
            [FieldOffset(24)]
            public uint mBaseOfData;
            [FieldOffset(28)]
            public uint mImageBase;
            [FieldOffset(32)]
            public uint mSectionAlignment;
            [FieldOffset(36)]
            public uint mFileAlignment;
            [FieldOffset(40)]
            public ushort mMajorOperatingSystemVersion;
            [FieldOffset(42)]
            public ushort mMinorOperatingSystemVersion;
            [FieldOffset(44)]
            public ushort mMajorImageVersion;
            [FieldOffset(46)]
            public ushort mMinorImageVersion;
            [FieldOffset(48)]
            public ushort mMajorSubsystemVersion;
            [FieldOffset(50)]
            public ushort mMinorSubsystemVersion;
            [FieldOffset(52)]
            public uint mWin32VersionValue;
            [FieldOffset(56)]
            public uint mSizeOfImage;
            [FieldOffset(60)]
            public uint mSizeOfHeaders;
            [FieldOffset(64)]
            public uint mCheckSum;
            [FieldOffset(68)]
            public ushort mSubsystem;
            [FieldOffset(70)]
            public ushort mDllCharacteristics;
            [FieldOffset(72)]
            public uint mSizeOfStackReserve;
            [FieldOffset(76)]
            public uint mSizeOfStackCommit;
            [FieldOffset(80)]
            public uint mSizeOfHeapReserve;
            [FieldOffset(84)]
            public uint mSizeOfHeapCommit;
            [FieldOffset(88)]
            public uint mLoaderFlags;
            [FieldOffset(92)]
            public uint mNumberOfRvaAndSizes;
        };
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        struct SectionHeader
        {
            [FieldOffset(0)]
            public fixed byte Name[8];
            [FieldOffset(8)]
            public uint PhysicalAddress;
            [FieldOffset(12)]
            public uint VirtualAddress;
            [FieldOffset(16)]
            public uint SizeOfRawData;
            [FieldOffset(20)]
            public uint PointerToRawData;
            [FieldOffset(24)]
            public uint PointerToRelocations;
            [FieldOffset(28)]
            public uint PointerToLinenumbers;
            [FieldOffset(32)]
            public ushort NumberOfRelocations;
            [FieldOffset(34)]
            public ushort NumberOfLinenumbers;
            [FieldOffset(36)]
            public uint Characteristics;
        }
        public PE32(string path)
        {
            GruntyOS.IO.BinaryReader br = new GruntyOS.IO.BinaryReader(new GruntyOS.IO.FileStream(path, "r"));
            int p = 0;
            uint address = 0;
            uint data_addr = 0;
            uint ib = 0;
            for (int i = 0; i < (int)br.BaseStream.Data.Length; i++)
            {
                p = br.BaseStream.Position;
                if (br.ReadByte() == (byte)'P' && br.ReadByte() == (byte)'E')
                    break;
            }
            br.BaseStream.Position = p;
            Console.WriteLine("Start: " + p.ToString());
            byte[] hdr = new byte[(sizeof(PeHeader))];
            for (int i = 0; i < sizeof(PeHeader); i++)
            {
                hdr[i] = br.ReadByte();
            }
            fixed (byte* ptr = hdr)
            {
                PeHeader* header = (PeHeader*)ptr;
                Console.WriteLine(header->mMachine.ToString());
                byte[] ohdr = new byte[header->mSizeOfOptionalHeader];

                for (int i = 0; i < header->mSizeOfOptionalHeader; i++)
                {
                    ohdr[i] = br.ReadByte();
                }
                fixed (byte* ptr2 = ohdr)
                {
                    Pe32OptionalHeader* opt = (Pe32OptionalHeader*)ptr2;
                    Console.WriteLine(opt->mBaseOfCode.ToString());
                    byte[] tmp = new byte[40];
                    address = opt->mBaseOfCode;
                    data_addr = opt->mBaseOfData;
                    ib = opt->mImageBase;
                    for (int s = 0; s < header->mNumberOfSections; s++)
                    {

                        fixed (byte* ptr3 = tmp)
                        {
                            for (int i = 0; i < 40; i++)
                            {
                                tmp[i] = br.ReadByte();
                            }
                            SectionHeader* sec = (SectionHeader*)ptr3;
                            string name = "";
                            for (int c = 0; sec->Name[c] != 0; c++)
                                name += ((char)sec->Name[c]).ToString();
                            Section section = new Section();
                            section.Name = name;
                            section.Address = (uint)sec->PointerToRawData;
                            section.RelocationCount = (uint)sec->NumberOfRelocations;
                            section.RelocationPtr = (uint)sec->PointerToRelocations;
                            section.Size = (uint)sec->SizeOfRawData;
                            Console.WriteLine(((int)(uint)sec->VirtualAddress).ToString());
                            sections.Add(section);
                        }
                    }
                }
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].Name == ".text")
                    {
                        text = new byte[sections[i].Size];
                        br.BaseStream.Position = (int)(uint)sections[i].Address;
                        for (int b = 0; b < (int)(uint)sections[i].Size; b++)
                        {
                            text[b] = br.ReadByte();
                        }
                    }
                    else if (sections[i].Name == ".data")
                    {
                        data = new byte[sections[i].Size];
                        br.BaseStream.Position = (int)(uint)sections[i].Address;
                        for (int b = 0; b < (int)(uint)sections[i].Size; b++)
                        {
                            data[b] = br.ReadByte();
                        }
                    }
                }
            }
            // We do not have paging working and I an to lazy to relocate this
            // so we are just loading this were the PE header tells us to
            // may be bad, because we 'could' be overwritting something
            // in RAM. Im not sure.... Lets hope not
            byte* dptr = (byte*)ib + address;
            for (int i = 0; i < text.Length; i++)
            {
                dptr[i] = text[i];
            }
            dptr = (byte*)ib + data_addr;
            for (int i = 0; i < data.Length; i++)
            {
                dptr[i] = data[i];
            }
            Caller cl = new Caller();
            cl.CallCode(ib + address); // Jump!!!!!

        }
    }

}