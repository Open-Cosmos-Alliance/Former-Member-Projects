using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem.FlowFS
{
    class FlowFS : Filesystem
    {
        public FlowFS(Cosmos.Hardware.BlockDevice.Partition p) : base(p)
        {
        }

        public static bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p)
        {
            throw new NotImplementedException();
        }

        public override FileStream GetFile(string path)
        {
            throw new NotImplementedException();
        }

        public override byte[] ReadFile(string path)
        {
            throw new NotImplementedException();
        }

        public override void WriteFile(string path, byte[] content)
        {
            throw new NotImplementedException();
        }

        public override void Format(string label)
        {
            throw new NotImplementedException();
        }
    }
}
