using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

using Cosmos.Hardware.BlockDevice;
using FAT = Cosmos.System.Filesystem.FAT;

namespace FlowDOS.IO.Filesystem.FAT32
{
    class FAT32 : Filesystem
    {
        FAT.FatFileSystem xFS;

        public FAT32(Cosmos.Hardware.BlockDevice.Partition p) : base(p)
        {
            xFS = new FAT.FatFileSystem(p);
        }

        public static bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p)
        {
            throw new NotImplementedException();
        }

        public override void WriteFile(string path, byte[] content)
        {
            throw new NotImplementedException();
        }

        public override byte[] ReadFile(string path)
        {
            var xListen = xFS.GetRoot();
            FAT.Listing.FatFile xFile = null;

            for (int j = 0; j < xListen.Count; j++)
            {
                /*if (xListen[j] is Sys.Filesystem.Listing.Directory)
                {
                    //Console.WriteLine("[DIR]--" + xListen[j].Name + "(" + xListen[j].Size + ")");
                }
                else if (xListen[j] is Sys.Filesystem.Listing.File)
                {
                    //Console.WriteLine("[FILE]--" + xListen[j].Name + "(" + xListen[j].Size + ")");
                    if (xListen[j].Name == "Amanp.txt")
                    {
                        xFile = (FAT.Listing.FatFile)xListen[j];
                    }
                }*/
                if (xListen[j] is Sys.Filesystem.Listing.File)
                {
                    if (xListen[j].Name == path.Substring(path.LastIndexOf('/')))
                    {
                        xFile = (FAT.Listing.FatFile)xListen[j];
                    }
                }
            }
            var xStream = new Sys.Filesystem.FAT.FatStream(xFile);
            var xData = new byte[xFile.Size];
            xStream.Read(xData, 0, (int)xFile.Size);
            return xData;
        }

        public override FileStream GetFile(string path)
        {
            return new FileStream(path, this);
        }

        public override void Format(string label)
        {
            throw new NotImplementedException();
        }
    }
}
