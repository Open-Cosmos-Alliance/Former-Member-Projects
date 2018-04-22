using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public abstract class Filesystem
    {
        public abstract Stream OpenFile(string file, int modes);
        public abstract void Chmod(string file, string perms);
        public abstract void Chown(string file, string owner);
        public abstract List<FileInfo> GetEntries(string dir);
    }
}
