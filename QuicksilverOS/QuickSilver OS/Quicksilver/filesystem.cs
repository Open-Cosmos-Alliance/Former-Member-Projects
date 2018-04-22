using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver
{
    abstract class filesystem
    {
        public abstract void write(string path, byte[] content);
        public abstract byte read(string path);
    }
    enum pathtypes
    {
        quicksilver, // system/users
        linux, // /usr
        windows // C:\Users
    }
    class GLNFS : filesystem
    {
        public override void write(string path, byte[] content)
        {
        }
        public override byte read(string path)
        {
            throw new NotImplementedException();
        }
    }
}
