/*
 * Written by: Aman Priyadarshi
 * Updated on: 03-04-2013
 */
/*
DO NOT REMOVE THIS WITHOUT PERMISSION

Copyright (c) 2013, Atom OS Team
All rights reserved.
Redistribution and use in source and binary forms, 
with or without modification, are permitted provided that the following conditions are met:

=> Redistributions of source code must retain the above copyright notice, 
   this list of conditions and the following disclaimer.

=> Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation and/or 
   other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware.BlockDevice;
using Atom_File_System.Core.lib;

namespace Atom_File_System.Core
{
    public class EntryBlock
    {
        public Partition CurrPart;
        public bool IsDeleted;
        public Entry EntryType;
        public UInt32 StartByte;
        public UInt32 Size;
        public byte[] BlockData;
        public string EntryName;

        public enum Entry
        {
            File,
            Directory,
            None
        };

        public EntryBlock(Partition partition, Entry EntryTypeX, UInt32 StartPos, string Name, byte[] Data)
        {
            IsDeleted = false;
            this.CurrPart = partition;
            this.EntryType = EntryTypeX;
            this.StartByte = StartPos;
            this.Size = (uint)Data.Length + 54;
            this.EntryName = Name.Substring(0, 48);
            this.BlockData = Data;
            CleanBlock();
            WriteHeader();
        }

        public EntryBlock(Partition partition, Entry EntryTypeX, UInt32 StartPos, string Name)
        {
            IsDeleted = false;
            this.CurrPart = partition;
            this.EntryType = EntryTypeX;
            this.StartByte = StartPos;
            this.Size = 54;
            this.EntryName = Name.Substring(0, 48);
            CleanBlock();
            WriteHeader();
        }

        public void WriteData()
        {
            int Address = Block.GetAddressInBlock(StartByte);
            uint SizeX = Block.GetBlockSize(Size);
            UInt32 NodeBlock = Block.GetNodeBlock(StartByte);

            byte[] EntryBlock = this.CurrPart.NewBlockArray(SizeX);
            this.CurrPart.ReadBlock(NodeBlock, SizeX, EntryBlock);

            for (int i = Address + 54; i < Address + 54 + BlockData.Length; i++)
            {
                EntryBlock[i] = BlockData[i - 54 - Address];
            }

            this.CurrPart.WriteBlock(NodeBlock, SizeX, EntryBlock);
        }

        public void WriteHeader()
        {
            int Address = Block.GetAddressInBlock(StartByte);
            UInt32 NodeBlock = Block.GetNodeBlock(StartByte);

            byte[] EntryBlock = this.CurrPart.NewBlockArray(1);
            this.CurrPart.ReadBlock(NodeBlock, 1, EntryBlock);

            if (IsDeleted)
                EntryBlock[Address] = 0x80;
            else
                EntryBlock[Address] = 0x50;

            if (EntryType == Entry.Directory)
                EntryBlock[Address + 1] = 0x0D;
            else if (EntryType == Entry.File)
                EntryBlock[Address + 1] = 0x0F;

            Byte[] labelData = Other.StringToByte(EntryName);

            for (int i = Address + 2; i < Address + 2 + labelData.Length; i++)
            {
                EntryBlock[i] = labelData[i - 2 - Address];//File Name
            }

            for (int i = Address + 50; i < Address + 54; i++)
            {
                EntryBlock[i] = BitConverter.GetBytes((UInt32)BlockData.Length)[i - 50 - Address];
            }

            this.CurrPart.WriteBlock(NodeBlock, 1, EntryBlock);
        }

        public void CleanBlock()
        {
            int Address = Block.GetAddressInBlock(StartByte);
            uint SizeX = (uint)Block.GetBlockSize(Size);
            UInt32 NodeBlock = Block.GetNodeBlock(StartByte);

            byte[] EntryBlock = this.CurrPart.NewBlockArray(SizeX);
            this.CurrPart.ReadBlock(NodeBlock, SizeX, EntryBlock);

            for (int i = Address; i < (int)(Size + Address); i++)
            {
                EntryBlock[i] = 0x00;
            }

            this.CurrPart.WriteBlock(NodeBlock, SizeX, EntryBlock);
        }
    }
}

