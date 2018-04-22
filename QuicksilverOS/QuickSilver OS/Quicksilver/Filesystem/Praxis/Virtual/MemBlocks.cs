using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Praxis.Emulator
{
    class MemBlocks
    {
        byte[] src;
        public MemBlocks(byte[] source)
        {
            src = source;
        }
        public void Read(ref byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[i] = src[i + offset];
            }
        }
        public byte[] Read(int offset, int count)
        {
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
            {
                buffer[i] = src[i + offset];
            }
            return buffer;
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if(i < buffer.Length) src[i + offset] = buffer[i];
                else src[i + offset] = 0;
            }
        }
    }
}
