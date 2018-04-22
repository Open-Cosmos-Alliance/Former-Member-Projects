using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public static class FileMode
    {
        public const byte ReadOnly = 1;
        public const byte WriteOnly = 2;
        public const byte ReadAndWrite = 4;
        public const byte Append = 8;
        public const byte Create = 16;
        public const byte NoSeek = 32;
    }
}
