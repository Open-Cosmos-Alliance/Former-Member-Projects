using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public abstract class Device
    {
        public string Name;
        public DeviceType Type;
        public abstract Stream Open(int modes);
    }
}
