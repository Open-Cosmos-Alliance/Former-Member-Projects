using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public class FileInfo
    {
        public string Name;
        public string Owner;
        public byte Permissions;
        public FileType Type;
    }
}
