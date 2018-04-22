using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fs = Cosmos.System.Filesystem;
using Cosmos.Hardware.BlockDevice;
using Cosmos.Hardware;
using GruntyOS.IO;

namespace FlowDOS.FlowFS
{
    class FlowFS
    {
        Partition part;
        #region Properties
        /// <summary>
        /// The number of blocks of the current partition
        /// </summary>
        public ulong BlockCount
        {
            get
            {
                return part.BlockCount;
            }
        }
        #endregion

        public bool isFlowFS(Partition p)
        {
            return false;
        }

        public FlowFS(Partition p)
        {
            part = p;
            if (!isFlowFS(part))
            {
                /*if (!Format(part))
                {
                }*/
            }
        }

        public FlowFS()
        {
            if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    BlockDevice Device = BlockDevice.Devices[i];
                    if (Device is Partition)
                    {
                        new FlowFS((Partition)Device);
                    }
                }
            }
        }



        public void Format(string VolumeLABEL)
        {
            byte[] aData = new byte[0x200];
            MemoryStream file = new MemoryStream(0x200);
            for (int i = 0; i < 0x200; i++)
            {
                aData[i] = 0;
            }
            for (int j = 0; j < 100; j++)
            {
                this.part.WriteBlock((ulong)j, 1, aData);
            }
            file.Data = aData;
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write("GFS SC");
            writer.Write(VolumeLABEL);
            writer.Write(4);
            writer.BaseStream.Close();
            this.part.WriteBlock(1L, 1, writer.BaseStream.Data);
        }
    }
}
