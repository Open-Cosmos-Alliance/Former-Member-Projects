using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem.Devices
{
    public class ballmerDev : Device
    {
        public ballmerDev()
        {
            this.Name = "ballmer";
            this.Type = DeviceType.CharDevice;
        }
        public override Stream Open(int modes = 4)
        {
            return new ballmerStream();
        }
    }
    public class ballmerStream : Stream
    {
        private string DEVELOPERS = @"DEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nDEVELOPERS!\nWHO SAID SIT DOWN?";
        public ballmerStream()
        {
            base.Register();
        }
        public override void writeByte(uint ptr, byte data)
        {

        }
        public override int readByte(uint ptr)
        {
            if (ptr >= DEVELOPERS.Length)
                return -1;
            else
                return (byte)DEVELOPERS[(int)ptr];
        }
    }
}
