using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntyOS.HAL;

namespace FlowDOS.Filesystem
{
    class GLNxDev : GLNFS
    {
        public GLNxDev() : base(null)
        {
        }

        public GLNxDev(Cosmos.Hardware.BlockDevice.Partition p) : base(p)
        {
        }

        /*public override bool CanExecute(string file)
        {
            return true;
        }

        public override bool CanRead(string file)
        {
            return (Environment.xDevFS.OpenFile(file, FileMode.ReadAndWrite).ModeRead);
        }

        public override bool CanWrite(string file)
        {
            return (Environment.xDevFS.OpenFile(file, FileMode.ReadAndWrite).ModeWrite);
        }*/

        public override void Chmod(string item, string mod)
        {
            Environment.xDevFS.Chmod(item, mod);
        }

        public override void Chown(string item, string mod)
        {
            Environment.xDevFS.Chown(item, mod);
        }

        public override void Delete(string Path)
        {
            throw new NotImplementedException();
        }

        public override fsEntry[] getLongList(string dir)
        {
            List<fsEntry> lst = new List<fsEntry>();
            for (int i = 0; i < Environment.xDevFS.GetEntries(dir).Count; i++)
            {
                fsEntry f = new fsEntry();
                f.Name = Environment.xDevFS.GetEntries(dir)[i].Name;
                f.Owner = Environment.xDevFS.GetEntries(dir)[i].Owner;
                f.Attributes = Environment.xDevFS.GetEntries(dir)[i].Permissions;
                lst.Add(f);
            }
            return lst.ToArray();
        }

        public override string[] ListDirectories(string dir)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < Environment.xDevFS.GetEntries(dir).Count; i++)
            {
                if (Environment.xDevFS.GetEntries(dir)[i].Type == FileType.Directory)
                {
                    lst.Add(Environment.xDevFS.GetEntries(dir)[i].Name);
                }
            }
            return lst.ToArray();
        }

        public override string[] ListFiles(string dir)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < Environment.xDevFS.GetEntries(dir).Count; i++)
            {
                lst.Add(Environment.xDevFS.GetEntries(dir)[i].Name);
            }
            return lst.ToArray();
        }

        bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }

        public override string[] ListJustFiles(string dir)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < Environment.xDevFS.GetEntries(dir).Count; i++)
            {

                if ((int)Environment.xDevFS.GetEntries(dir)[i].Type == 2 || (int)Environment.xDevFS.GetEntries(dir)[i].Type == 6)
                {
                    lst.Add(Environment.xDevFS.GetEntries(dir)[i].Name);
                }
            }
            return lst.ToArray();
        }

        public override void makeDir(string name, string owner)
        {
            throw new NotImplementedException();
        }

        public override void Move(string f, string dest)
        {
            throw new NotImplementedException();
        }

        public override byte[] readFile(string name)
        {
            Stream s = Environment.xDevFS.OpenFile(name, FileMode.ReadOnly);            
            List<byte> bt = new List<byte>();
            int dat = 0;
            while (dat != -1)
            {
                dat = s.Read();
                bt.Add((byte)dat);
            }
            return bt.ToArray();
        }

        public override void saveFile(byte[] data, string name, string owner)
        {
            
        }
    }
}
