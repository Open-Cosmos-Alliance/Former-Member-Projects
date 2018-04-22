using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Filesystem
{
    class FileStream : Stream
    {
        public Filesystem FS = null;

        int currentPos = 0;

        public string Path = "";

        public FileStream(string path, Filesystem fs)
        {
            this.Path = path;
            this.FS = fs;
        }

        public override int Read()
        {
            int a = (int)FS.ReadFile(Path)[currentPos];
            currentPos++;
            return a;
        }

        public void Write(byte[] content)
        {
            byte[] c2 = FS.ReadFile(Path);
            for (int i = 0; i < content.Length; i++)
            {
                c2[c2.Length + i] = content[i];
            }
            FS.WriteFile(Path, c2);
        }

        public override void Write(byte content)
        {
            byte[] c2 = FS.ReadFile(Path);
            c2[c2.Length] = content;
            FS.WriteFile(Path, c2);
        }

        public override void Write(int content)
        {
            byte[] c2 = FS.ReadFile(Path);
            c2[c2.Length] = (byte)content;
            FS.WriteFile(Path, c2);
        }

        public void Write(string content)
        {
        }
    }
}
