using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO
{
    public abstract class Stream
    {
        public abstract int Read();

        public abstract void Write(byte content);

        public abstract void Write(int content);
    }
}
