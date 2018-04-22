using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO.Memory
{
    class MemoryStream : Stream
    {
        unsafe byte* ptr = null;

        public unsafe MemoryStream(int pos)
        {
            ptr = (byte*)pos;
        }

        public unsafe MemoryStream(byte pos)
        {
            ptr = (byte*)pos;
        }

        public unsafe override int Read()
        {
            return (int)*ptr;
        }

        public unsafe override void Write(byte content)
        {
            *ptr = content;
        }

        public unsafe override void Write(int content)
        {
            *ptr = (byte)content;
        }
    }
}
