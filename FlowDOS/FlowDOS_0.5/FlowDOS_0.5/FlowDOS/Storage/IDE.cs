using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware.BlockDevice;

namespace EnvyOS.Storage
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    class IDE
    {
        private BlockDevice blockDevice;

        static public List<IDE> Devices
        {
            get
            {
                List<IDE> devs = new List<IDE>();
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    if (BlockDevice.Devices[i] is AtaPio)
                    {
                        devs.Add(new IDE(BlockDevice.Devices[i]));
                    }
                }
                return devs;
            }
        }

        /// <summary>
        /// Gets the MBR of the current IDE Disk
        /// </summary>
        public MBR MBR
        {
            get
            {
                Byte[] data = blockDevice.NewBlockArray(1);
                this.ReadBlock(0, 1, data);
                MBR m = new MBR(data);
                return m;
            }
        }

        /// <summary>
        /// Gets The Primary Partitions of the current IDE Disk
        /// </summary>
        public PrimaryPartition[] PrimaryPartitions
        {
            get
            {
                List<PrimaryPartition> l = new List<PrimaryPartition>();
                for (int i = 0; i < MBR.Partitions.Length; i++)
                {
                    if (MBR.Partitions[i].SystemID != 0)
                    {
                        l.Add(new PrimaryPartition(blockDevice, MBR.Partitions[i].StartSector, MBR.Partitions[i].SectorCount, MBR.Partitions[i]));
                    }
                }
                return l.ToArray();
            }
        }

        /// <summary>
        /// The size of each block of the current Device
        /// </summary>
        public UInt64 BlockSize
        {
            get { return blockDevice.BlockSize; }
        }

        /// <summary>
        /// The number of blocks of the current Device
        /// </summary>
        public UInt64 BlockCount
        {
            get { return blockDevice.BlockCount; }
        }

        /// <summary>
        /// Creates a new IDE object based on the blockdevice
        /// </summary>
        /// <param name="blockDevice">The blockdevice to use</param>
        public IDE(BlockDevice blockDevice)
        {
            this.blockDevice = blockDevice;
        }

        /// <summary>
        /// Reads a number of blocks from the device
        /// </summary>
        /// <param name="aBlockNo">Start block number</param>
        /// <param name="aBlockCount">Number of blocks</param>
        /// <param name="aData">Buffer to write to</param>
        public void ReadBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
        {
            blockDevice.ReadBlock(aBlockNo, aBlockCount, aData);
        }

        /// <summary>
        /// Writes a number a buffer down to the current device
        /// </summary>
        /// <param name="aBlockNo">Start block number</param>
        /// <param name="aBlockCount">Number of blocks</param>
        /// <param name="aData">Buffer to read from</param>
        public void WriteBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
        {
            blockDevice.WriteBlock(aBlockNo, aBlockCount, aData);
        }

        /// <summary>
        /// Gets a new Byte array formatted with number of elements equal to [num * blockSize]
        /// </summary>
        /// <param name="num">Number of blocks</param>
        public Byte[] NewBlockArray(uint num)
        {
            return blockDevice.NewBlockArray(num);
        }
    }
}
