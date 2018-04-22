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
using rtc = Cosmos.Hardware.RTC;

namespace Atom_File_System.Core
{
    public class AFS : FileSystem
    {

        public Partition CurrentPartition;

        public string VolumeLabel;
        public UInt32 WriteAddress;//33th byte
        public UInt32 DirectoryCode;//37th byte

        public AFS(Partition partitionX)
        {
            CurrentPartition = partitionX;
        }

        public bool IsAFS()
        {
            Byte[] FSConfig = this.CurrentPartition.NewBlockArray(1);
            this.CurrentPartition.ReadBlock(1UL, 1U, FSConfig);

            Byte[] Label = new Byte[32];
            for (int i = 1; i < 33; i++)
            {
                Label[i - 1] = FSConfig[i];
            }

            if (FSConfig[0] == 0xAF && BitConverter.ToUInt32(FSConfig, 33) != 0)
            {
                Init();
                return true;
            }
            return false;
        }

        public override void Init()
        {
            Byte[] FSConfig = this.CurrentPartition.NewBlockArray(1);
            this.CurrentPartition.ReadBlock(1UL, 1U, FSConfig);

            Byte[] Label = new Byte[32];
            for (int i = 1; i < 33; i++)
            {
                Label[i - 1] = FSConfig[i];
            }

            VolumeLabel = Encoding.ASCII.GetString(Label);
            WriteAddress = BitConverter.ToUInt32(FSConfig, 33);
            DirectoryCode = BitConverter.ToUInt32(FSConfig, 37);
        }
        public override UInt32 WriteAdd()
        {
            return WriteAddress;
        }
        public override void Format(string Label)
        {            
            CleanPartition(this.CurrentPartition.BlockCount);

            Byte[] aData = this.CurrentPartition.NewBlockArray(1);
            Byte[] bData = Other.StringToByte(Label);

            aData[0] = 0xAF;
            for (int i = 0; i < bData.Length; i++)
            {
                aData[i + 1] = bData[i];
            }

            for (int i = 33; i < 37; i++)
            {
                aData[i] = BitConverter.GetBytes((UInt32)1024)[i - 33];
            }

            for (int i = 37; i < 41; i++)
            {
                aData[i] = BitConverter.GetBytes((UInt32)1)[i - 37];
            }
            
            this.CurrentPartition.WriteBlock(1UL, 1U, aData);
        }

        public UInt32 GetNewDirectoryCode()
        {
            DirectoryCode++;
            Byte[] aData = this.CurrentPartition.NewBlockArray(1);
            this.CurrentPartition.ReadBlock(1UL, 1U, aData);
            for (int i = 37; i < 41; i++)
            {
                aData[i] = BitConverter.GetBytes((UInt32)DirectoryCode)[i - 37];
            }
            this.CurrentPartition.WriteBlock(1UL, 1U, aData);
            return DirectoryCode;
        }

        public UInt32 GetWriteAddress(uint Increament)
        {
            UInt32 j = WriteAddress;
            WriteAddress += Increament;

            Byte[] aData = this.CurrentPartition.NewBlockArray(1);
            this.CurrentPartition.ReadBlock(1UL, 1U, aData);
            for (int i = 33; i < 37; i++)
            {
                aData[i] = BitConverter.GetBytes((UInt32)WriteAddress)[i - 33];
            }
            this.CurrentPartition.WriteBlock(1UL, 1U, aData);
            return j;
        }

        public void CreatEntry(string EntryName, byte[] xData, EntryBlock.Entry EntryType)
        {

            EntryBlock AFSBlock = new EntryBlock(CurrentPartition,
                                                EntryType,
                                                GetWriteAddress((uint)((uint)xData.Length + 54)),
                                                EntryName, xData);

            AFSBlock.WriteData();

            /*======= For Directory Entry =======
             * xData[0] = Attrib
             * [1 - 5) = Previous Directory Code(PDC)
             * [5 - 9) = Current Directory Code(CDC)New Directory Code
             * [9 - 14) = Date Created
             * [14 - 19) = Date Modified
             * -->No User data
             *======= For File Entry =======
             * xData[0] = Attrib
             * [1 - 5) = Previous Directory Code(PDC)
             * [5 - 10) = Date Created
             * [10 - 15) = Date Modified
             * [15 - Infinity) = User Data <Infinity: Joke :)>
             */
        }
        private byte[] GetCDC(string Name, byte[] PDC)
        {
            Entries[] SystemEntries = GetEntries();
            for (int i = 0; i < SystemEntries.Length; i++)
            {
                if (SystemEntries[i].EntryName == Name && SystemEntries[i].EntryData[1] == PDC[0] && SystemEntries[i].EntryData[2] == PDC[1] && SystemEntries[i].EntryData[3] == PDC[2] && SystemEntries[i].EntryData[4] == PDC[3])
                {
                    return new byte[] { SystemEntries[i].EntryData[5], SystemEntries[i].EntryData[6], SystemEntries[i].EntryData[7], SystemEntries[i].EntryData[8] };
                }
            }
            throw new Exception("File Not Found (CDC) :(");
        }
        public override byte[] GetFileData(string Dir, string FileName)
        {
            Entries[] DirEntries = GetEntries(Dir);
            for (int i = 0; i < DirEntries.Length; i++)
            {
                if (DirEntries[i].EntryName == FileName)
                {
                    byte[] FileDataX = new byte[(int)(DirEntries[i].EntryData.Length - 15)];

                    for (int j = 15; j < DirEntries[i].EntryData.Length; j++)
                    {
                        FileDataX[j - 15] = DirEntries[i].EntryData[j];
                    }
                    return FileDataX;
                }
            }
            throw new Exception("No Such File Found :(");
        }
        public override void CreateFile(string Filepath, byte[] yData, byte Attrib)
        {
            byte[] xData = new byte[yData.Length + 15];

            string FileName;
            string[] dirs = Filepath.Split('/');

            if (dirs[0] != ":")
            {
                throw new Exception("Wrong Parameter");
            }

            FileName = dirs[dirs.Length - 1];
            byte[] xTemp = new byte[] { 0x00, 0x00, 0x00, 0x00 };

            if (dirs.Length - 1 != 1)
            {
                for (int i = 1; i < dirs.Length - 1; i++)
                {
                    xTemp = GetCDC(dirs[i], xTemp);
                }
            }
            xData[0] = Attrib;
            xData[1] = xTemp[0];
            xData[2] = xTemp[1];
            xData[3] = xTemp[2];
            xData[4] = xTemp[3];

            xData[5] = xData[10] = rtc.Hour;
            xData[6] = xData[11] = rtc.Minute;
            xData[7] = xData[12] = rtc.DayOfTheMonth;
            xData[8] = xData[13] = rtc.Month;
            xData[9] = xData[14] = rtc.Year;

            for (int i = 15; i < 15 + yData.Length; i++)
            {
                xData[i] = yData[i - 15];
            }
            CreatEntry(FileName, xData, EntryBlock.Entry.File);
            
        }
        public override void CreateDirectory(string Filepath, byte Attrib)
        {
            byte[] yData = new byte[15];
            string FileName;
            string[] dirs = Filepath.Split('/');

            if (dirs[0] != ":")
            {
                throw new Exception("Wrong Parameter");
            }

            FileName = dirs[dirs.Length - 1];
            byte[] xTemp = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            uint a = GetNewDirectoryCode();

            byte[] NDC = BitConverter.GetBytes(a);
            if (dirs.Length - 1 != 1)
            {
                for (int i = 1; i < dirs.Length - 1; i++)
                {
                    xTemp = GetCDC(dirs[i], xTemp);
                }
            }
            yData[0] = Attrib;
            yData[1] = xTemp[0];
            yData[2] = xTemp[1];
            yData[3] = xTemp[2];
            yData[4] = xTemp[3];

            yData[5] = NDC[0];
            yData[6] = NDC[1];
            yData[7] = NDC[2];
            yData[8] = NDC[3];

            yData[9] = yData[14] = rtc.Hour;
            yData[10] = yData[15] = rtc.Minute;
            yData[11] = yData[16] = rtc.DayOfTheMonth;
            yData[12] = yData[17] = rtc.Month;
            yData[13] = yData[18] = rtc.Year;

            CreatEntry(FileName, yData, EntryBlock.Entry.Directory);
            
        }
        private byte[] ReadDisk(UInt32 Blocks, UInt32 StartBlock)
        {
            
            int p = 0;
            byte[] aData = this.CurrentPartition.NewBlockArray(Blocks);
            byte[] xData = new byte[512];
            for (uint i = StartBlock; i < StartBlock + Blocks; i++)
            {
                this.CurrentPartition.ReadBlock(i, 1U, xData);
                for (int j = 0; j < 512; j++)
                {
                    aData[p] = xData[j];
                    p++;
                }
            }
            return aData;
        }
        public override Entries[] GetEntries(string Dir)
        {
            string[] dirs = Dir.Split('/');
            byte[] xTemp = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            for (int i = 1; i < dirs.Length; i++)
            {
                xTemp = GetCDC(dirs[i], xTemp);
            }

            Entries[] DiskEntries = new Entries[100000];

            int Address = Block.GetAddressInBlock(1024);
            uint SizeX = Block.GetBlockSize(WriteAddress - 1024);

            byte[] EntryBlock = this.CurrentPartition.NewBlockArray(SizeX);
            this.CurrentPartition.ReadBlock(2UL, SizeX, EntryBlock);
            //EntryBlock = ReadDisk(SizeX, 2);

            bool stop = false;
            int pointer = 0;
            int x = 0;

            while (stop != true)
            {
                //Console.Write("r");
                if (pointer == WriteAddress - 1024)
                {
                    //Console.Write("Stopped");
                    stop = true;
                }
                if (EntryBlock[pointer] == 0x50)
                {
                    //This is for Folder
                    //Console.Write("-E");
                    pointer++;
                    if (EntryBlock[pointer] == 0x0D)
                    {
                        pointer++;
                        //Console.Write("-d");
                        Int32 DataSize = BitConverter.ToInt32(EntryBlock, pointer + 48);
                        byte[] xData = new byte[DataSize];
                        for (int i = pointer + 52; i < pointer + 52 + DataSize; i++)
                        {
                            xData[i - pointer - 52] = EntryBlock[i];
                        }

                        if (xData[1] == xTemp[0] && xData[2] == xTemp[1] && xData[3] == xTemp[2] && xData[4] == xTemp[3])
                        {
                            Byte[] Label = new Byte[48];
                            for (int i = pointer; i < pointer + 48; i++)
                            {
                                Label[i - pointer] = EntryBlock[i];
                            }
                            string labelX = Encoding.ASCII.GetString(Label);
                            labelX = labelX.TrimEnd('\0');
                            DiskEntries[x] = new Entries(Entries.Entry.Directory, (uint)pointer, xData, labelX.Trim());
                            x++;
                        }
                        pointer = pointer + 52 + DataSize;

                    }
                    else if (EntryBlock[pointer] == 0x0F)
                    {
                        //Then ATAPIO Crashes and stucks
                        //This is for File
                        pointer++;
                      //  Console.Write("-f");
                        Int32 DataSize = BitConverter.ToInt32(EntryBlock, pointer + 48);
                        byte[] xData = new byte[DataSize];
                        for (int i = pointer + 52; i < pointer + 52 + DataSize; i++)
                        {
                            xData[i - pointer - 52] = EntryBlock[i];
                        }

                        if (xData[1] == xTemp[0] && xData[2] == xTemp[1] && xData[3] == xTemp[2] && xData[4] == xTemp[3])
                        {
                            Byte[] Label = new Byte[48];
                            for (int i = pointer; i < pointer + 48; i++)
                            {
                                Label[i - pointer] = EntryBlock[i];
                            }
                            string labelX = Encoding.ASCII.GetString(Label);
                            labelX = labelX.TrimEnd('\0');
                            DiskEntries[x] = new Entries(Entries.Entry.File, (uint)pointer, xData, labelX.Trim());
                            x++;
                        }
                        pointer = pointer + 52 + DataSize;
                    }
                    else
                    {
                        //No signature :(
                        //pointer++;
                    }
                }
                else if (EntryBlock[pointer] == 0x80)
                {
                    //File is deleted
                    //Console.Write("-A");
                }
                else
                {
                    //Nothing found
                    //Console.Write("-N");
                    //pointer++;
                }
            }

            Entries[] DiskEntriesX = new Entries[x];
            for (int i = 0; i < x; i++)
            {
                DiskEntriesX[i] = DiskEntries[i];
            }
            return DiskEntriesX;
        }
        public override Entries[] GetEntries()
        {
            Entries[] DiskEntries = new Entries[100000];

            int Address = Block.GetAddressInBlock(1024);
            uint SizeX = Block.GetBlockSize(WriteAddress - 1024);

            byte[] EntryBlock = this.CurrentPartition.NewBlockArray(SizeX);
            this.CurrentPartition.ReadBlock(2UL, SizeX, EntryBlock);

            bool stop = false;
            int pointer = 0;
            int x = 0;

            while (stop != true)
            {
                if (pointer == WriteAddress - 1024)
                {
                    stop = true;
                }
                if (EntryBlock[pointer] == 0x50)
                {
                    pointer++;
                    if (EntryBlock[pointer] == 0x0D)
                    {
                        pointer++;
                        Byte[] Label = new Byte[48];
                        for (int i = pointer; i < pointer + 48; i++)
                        {
                            Label[i - pointer] = EntryBlock[i];
                        }
                        Int32 DataSize = BitConverter.ToInt32(EntryBlock, pointer + 48);
                        string labelX = Encoding.ASCII.GetString(Label);
                        labelX = labelX.TrimEnd('\0');
                        byte[] xData = new byte[DataSize];
                        for (int i = pointer + 52; i < pointer + 52 + DataSize; i++)
                        {
                            xData[i - pointer - 52] = EntryBlock[i];
                        }
                        DiskEntries[x] = new Entries(Entries.Entry.Directory, (uint)pointer, xData, labelX.Trim());
                        pointer = pointer + 52 + DataSize;
                        x++;
                    }
                    else if (EntryBlock[pointer] == 0x0F)
                    {
                        pointer++;
                        Byte[] Label = new Byte[48];
                        for (int i = pointer; i < pointer + 48; i++)
                        {
                            Label[i - pointer] = EntryBlock[i];
                        }
                        Int32 DataSize = BitConverter.ToInt32(EntryBlock, pointer + 48);
                        string labelX = Encoding.ASCII.GetString(Label);
                        labelX = labelX.TrimEnd('\0');
                        byte[] xData = new byte[DataSize];
                        for (int i = pointer + 52; i < pointer + 52 + DataSize; i++)
                        {
                            xData[i - pointer - 52] = EntryBlock[i];
                        }
                        DiskEntries[x] = new Entries(Entries.Entry.File, (uint)pointer, xData, labelX.Trim());
                        pointer = pointer + 52 + DataSize;
                        x++;
                    }
                    else
                    {
                        //pointer++;
                        //No signature :(
                    }
                }
                else if (EntryBlock[pointer] == 0x80)
                {
                    //File is deleted
                }
                else
                {
                    pointer++;
                    //Nothing found
                }
            }

            Entries[] DiskEntriesX = new Entries[x];
            for (int i = 0; i < x; i++)
            {
                DiskEntriesX[i] = DiskEntries[i];
            }
            return DiskEntriesX;
        }

        public void CleanPartition(ulong TotalSectorCount)
        {            
            Byte[] aData = this.CurrentPartition.NewBlockArray(1);

            for (int i = 0; i < 512; i++)
            {
                aData[i] = 0x00;
            }
            Console.WriteLine();
            int m = 0;
            for (ulong i = 0; i < TotalSectorCount; i++)
            {
                m++;
                if (m == 20)
                {
                    Cosmos.System.Global.Console.Y -= 1;
                    Console.WriteLine("Formating Partition: " + i + " / " + TotalSectorCount);
                    m = 0;
                }
                this.CurrentPartition.WriteBlock(i, 1U, aData);
            } 
        }
    }
}

