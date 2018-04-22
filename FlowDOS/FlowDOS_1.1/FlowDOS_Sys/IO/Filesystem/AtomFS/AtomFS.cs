using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem.AtomFS
{
    class AtomFS : Filesystem
    {
        public override bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p)
        {
            throw new NotImplementedException();
        }

        public override void WriteFile(string path, byte[] content)
        {
            throw new NotImplementedException();
        }

        public override byte[] ReadFile(string path)
        {
            throw new NotImplementedException();
        }

        public override FileStream GetFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
