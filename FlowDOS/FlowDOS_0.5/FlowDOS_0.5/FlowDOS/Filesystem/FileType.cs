using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public enum FileType
    {
        Directory = 1,
        File = 2,
        Executable = 4,
        BlockDevice = 8
    }
}
