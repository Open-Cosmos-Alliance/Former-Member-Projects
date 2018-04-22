/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver2013
{
    class KeyMap
    {
        int bits = 7;
        string name;
        List<char> keys = new List<char> { '\0' };
        public static byte[] getBytes(KeyMap par0, string par1) {
            return new byte[1];
        }
        public static string getString(KeyMap par0, byte[] par1) {
            string var0 = "";
            foreach(byte b in par1) {
                var0 += par0.keys[b];
            }
            return var0;
        }
    }
}
*/