using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware.BlockDevice;

namespace FlowDOS.Core
{
    class Filesystem
    {
        public static void Init()
        {
            if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    var xDevice = BlockDevice.Devices[i];
                    if (xDevice is Partition)
                    {
                        Global.CurrentFS = IO.Filesystem.FilesystemManager.CheckFS((Partition)xDevice);
                    }
                }
            }
        }
    }
}
