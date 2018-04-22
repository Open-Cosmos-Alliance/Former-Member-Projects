using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public class Stream
    {
        public bool ModeRead = true;
        public bool ModeWrite = true;
        public bool ModeAppend = false;
        public bool SupportsSeek = true;

        public int Descriptor;
        private uint _pointer = 0;
        public uint Length;

        private static List<Stream> OpenedStreams = new List<Stream>();

        public static Stream fromDescriptor(int desc, byte mode = 0)
        {
            if (OpenedStreams.Count >= desc)
            {
                Stream stm = OpenedStreams[desc];
                if (mode != 0)
                    stm.setModes(mode);
                return stm;
            }
            else return null;
        }

        public uint Pointer
        {
            get
            {
                if (SupportsSeek)
                    return _pointer;
                else
                    return 0;
            }
            set
            {
                if (SupportsSeek)
                    _pointer = value;

            }
        }

        bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
        public static void reassign(int des, Stream newstream)
        {
            OpenedStreams[des] = newstream;
        }
        protected void setModes(byte modes)
        {
            if (GetBit(modes, 0))
            {
                ModeRead = true;
                ModeWrite = false;
            }
            if (GetBit(modes, 1))
            {
                ModeRead = false;
                ModeWrite = true;
            }
            if (GetBit(modes, 2))
            {
                ModeRead = true;
                ModeWrite = true;
            }

            if (GetBit(modes, 3))
            {
                this.Pointer = Length;
            }
            if (GetBit(modes, 5))
            {
                SupportsSeek = false;
            }
        }

        protected void Register(byte modes = 4)
        {

            this.Descriptor = OpenedStreams.Count;
            this.setModes(modes);
            OpenedStreams.Add(this);
        }
        public virtual void ReadBytes(int length, byte[] dat)
        {
            if (!ModeRead)
                return;
            for (int i = 0; i < length; i++)
            {
                dat[i] = (byte)Read();
            }
        }
        public void Write(byte data)
        {
            if (!ModeWrite)
                return;
            OpenedStreams[Descriptor].writeByte(Pointer, data);
            Pointer++;
        }
        public int Read()
        {
            if (!ModeRead)
                return -1;
            Pointer++;
            return OpenedStreams[Descriptor].readByte(Pointer - 1);
        }
        public void Close()
        {
            OpenedStreams[Descriptor].onClose();
            for (int i = 0; i < OpenedStreams.Count; i++)
            {
                if (i == Descriptor)
                    OpenedStreams[i] = null;
            }
        }
        public virtual void onClose()
        {
        }
        public virtual int readByte(uint ptr)
        {
            return -1;
        }

        public virtual void writeByte(uint ptr, byte dat)
        {
        }
    }
}
