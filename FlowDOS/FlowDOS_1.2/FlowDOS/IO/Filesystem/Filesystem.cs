using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem
{
    public abstract class Filesystem
    {
        protected Cosmos.Hardware.BlockDevice.Partition currentPart = null;

        public Filesystem(Cosmos.Hardware.BlockDevice.Partition p)
        {
            this.currentPart = p;
        }

        //public static bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p);

        public abstract void WriteFile(string path, byte[] content);

        public abstract byte[] ReadFile(string path);

        public abstract FileStream GetFile(string path);

        public abstract void Format(string label);
    }
}
