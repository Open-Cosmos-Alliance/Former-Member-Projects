using System;
using System.Linq;
using System.Text;

namespace WitchcraftOS.Witchcraftfx.Collections.Generic
{
    // DO NOT USE IT IN YOUR PROJECT!
    // IT IS JUST EXPERIMENTAL AND WILL NOT WORK!
    public class StringDict
    {
        private string[] key;
        private string[] val;
        public StringDict() { ;}
        public int count() { return key.Length; }
        public string getValue(string _key)
        {
            int length = key.Length;
            int count = 0;
            do
            {
                if (key[count] == _key) return val[count];
            } while (count < length);
            throw new Exception("Element not found");
        }
        public string getValue(int index)
        {
            try { return val[index]; }
            catch { throw new Exception("Element not found"); }
        }
        public void Add(string _key, string _value)
        {
            key[key.Length] = _key;
            val[key.Length] = _value;
        }
    }
}
