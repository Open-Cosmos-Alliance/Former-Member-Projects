using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cosmos.Hardware.BlockDevice;
namespace FlowDOS.IO.Filesystem
{
    class FilesystemManager
    {
        //public List<Filesystem> FSs = new List<Filesystem>();
        public Filesystem FS;

        public void Init(Partition p)
        {
            this.FS = new AtomFS.AtomFS(p);
            FS.Format("FlowDOS");
        }

        public static Filesystem CheckFS(Partition p)
        {
            if (AtomFS.AtomFS.CheckIfFS(p))
            {
                return new AtomFS.AtomFS(p);
            }
            else if (FAT32.FAT32.CheckIfFS(p))
            {
                return new FAT32.FAT32(p);
            }
            else if (FlowFS.FlowFS.CheckIfFS(p))
            {
                return new FlowFS.FlowFS(p);
            }
            /*else if (GLNFS.GLNFS.CheckIfFS(p))
            {
                return new GLNFS.GLNFS(p);
            }*/
            else if (WitchFS.WitchFS.CheckIfFS(p))
            {
                return new WitchFS.WitchFS(p);
            }
            else
            {
                return null;
            }
        }
    }
}
