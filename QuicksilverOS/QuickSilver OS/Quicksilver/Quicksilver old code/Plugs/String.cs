using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.IL2CPU.Plugs;

namespace Quicksilver2013.Impl
{
    class String
    {
        public static int GetHashCode(string par0)
        {
            // no empty strings, please
            if (par0.Length == 0) return 0;
            int h = par0[0];
            for (int i = 1; par0[i] != 0; ++i)
                h = (h << 4) + par0[i];
            return h % par0.Length; // remainder
        }
    }
}
