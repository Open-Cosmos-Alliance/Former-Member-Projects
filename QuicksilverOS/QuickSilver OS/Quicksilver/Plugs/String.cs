using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.IL2CPU.Plugs;

namespace Quicksilver.Impl
{
    class String
    {
        public static uint GetHashCode(string par0)
        {
            if (par0.Length == 0) return 0;
            uint h = par0[0];
            for (int i = 1; par0[i] != 0; i++)
                h = (h << 4) + par0[i];
            return (uint)(h % par0.Length);
        }
    }
}
