using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver
{
    /*class KeyMap
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
    }*/
    class ASCII
    {
        static List<KeyMap> keys = new List<KeyMap> { new KeyMap(0x20, ' '), new KeyMap(0x21, '!'), new KeyMap(0x22, '"'), new KeyMap(0x23, '#'), new KeyMap(0x24, '$') };
        public static string GetString(byte[] input) {
            string rtrn = "";
            foreach(byte b in input) {
                foreach (KeyMap key in keys)
                {
                    if (key.b == b) { rtrn += key.c; break; }
                }
            }
            return rtrn;
        }
    }
    class KeyMap {
        public byte b;
        public char c;
        public KeyMap(byte par0, char par1) {
            b = par0; c = par1;
        }
    }
}
