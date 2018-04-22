using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dewitcher.Crypto
{
    class FlowHash
    {
        public static byte[] Hash(string s)
        {
            uint seed = 0;
            UInt64 hash = 0;
            byte[] hash2 = null;
            byte[] bytes = new byte[s.Length + 1];
            for (int i = 0; i < s.Length; i++)
            {
                bytes[i] = (byte)(s[i]);
            }
            for (int i = 0; i < s.Length; i++)
            {
                seed += (uint)(~bytes[i] * i >> (i + 10));
            }
            Core.Memory.MemAlloc(sizeof(UInt64));
            for (int i = 0; i < s.Length; i++)
            {
                hash += seed * (~(uint)bytes[i] >> 6);
            }
            int attempts = 0;
            /*do
            {
                hash *= 2;
                hash <<= ++attempts + ++attempts;
            } while (hash.ToString().Length < 32);*/

            for (int i = 0; i < hash.ToString().Length; i++)
            {
                hash2[i] = (byte)hash.ToString()[i];
            }
            return hash2;
        }

        public static byte[] Hash(byte[] s)
        {
            uint seed = 0;
            UInt64 hash = 0;
            byte[] hash2 = null;
            byte[] bytes = s;
            for (int i = 0; i < s.Length; i++)
            {
                seed += (uint)(~bytes[i] * i >> (i + 10));
            }
            Core.Memory.MemAlloc(sizeof(UInt64));
            for (int i = 0; i < s.Length; i++)
            {
                hash += seed * (~(uint)bytes[i] >> 6);
            }
            int attempts = 0;
            /*do
            {
                hash *= 2;
                hash <<= ++attempts + ++attempts;
            } while (hash.ToString().Length < 32);*/

            for (int i = 0; i < hash.ToString().Length; i++)
            {
                hash2[i] = (byte)hash.ToString()[i];
            }
            return hash2;
        }
    }
}
