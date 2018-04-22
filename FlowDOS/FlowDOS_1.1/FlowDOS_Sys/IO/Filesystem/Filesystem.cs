using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem
{
    public abstract class Filesystem
    {
        public abstract bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p);

        public abstract void WriteFile(string path, byte[] content);

        public abstract byte[] ReadFile(string path);

        public abstract FileStream GetFile(string path);
    }
}
