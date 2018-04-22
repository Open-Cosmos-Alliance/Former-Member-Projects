using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Compilers.Basic
{
    class BNext
    {
        public enum nexttype
        {
            to,
            lessthan,
            greaterthan
        };

        public nexttype type = nexttype.to;
        public int line = 0;
    }
}
