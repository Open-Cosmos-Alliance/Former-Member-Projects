using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom_File_System;

namespace FlowDOS.IO.Filesystem.AtomFS
{
    class AtomFS : Filesystem
    {
        static Atom_File_System.Core.AFS fs;

        public AtomFS(Cosmos.Hardware.BlockDevice.Partition p) : base(p)
        {
            fs = new Atom_File_System.Core.AFS(p);
        }

        public static bool CheckIfFS(Cosmos.Hardware.BlockDevice.Partition p)
        {
            return fs.IsAFS();
        }

        public override void WriteFile(string path, byte[] content)
        {
            fs.CreateFile(path, content, 0);
        }

        public override byte[] ReadFile(string path)
        {
            int a = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/')
                {
                    a = i;
                    continue;
                }
            }
            string dir = "";
            string file = "";
            for (int j = 0; j < a; j++)
            {
                dir += path[j];
            }
            for (int k = (a + 1); k < path.Length; k++)
            {
                file += path[k];
            }
            return fs.GetFileData(dir, file);
        }

        public override FileStream GetFile(string path)
        {
            return new FileStream(path, this);
        }

        public override void Format(string label)
        {
            fs.Format(label);
        }
    }
}
