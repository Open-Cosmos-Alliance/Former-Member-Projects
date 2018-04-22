using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem.GLNFS
{
    /*class GLNFS : Filesystem
    {
        GruntyOS.HAL.GLNFS fs = null;

        public GLNFS(Cosmos.Hardware.BlockDevice.Partition p) : base(p)
        {
            fs = new GruntyOS.HAL.GLNFS(p);
        }

        public static bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p)
        {
            return GruntyOS.HAL.GLNFS.isGFS(p);
        }

        public override void WriteFile(string path, byte[] content)
        {
            fs.Delete(path);
            fs.saveFile(content, path, "");
        }

        public void WriteFile(string path, byte[] content, string owner)
        {
            fs.Delete(path);
            fs.saveFile(content, path, owner);
        }

        public override byte[] ReadFile(string path)
        {
            return fs.readFile(path);
        }

        public override FileStream GetFile(string path)
        {
            return new FileStream(path, this);
        }

        public override void Format(string label)
        {
            fs.Format(label);
        }
    }*/
}
