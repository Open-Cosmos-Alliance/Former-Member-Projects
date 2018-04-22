using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    public class xDev : Filesystem
    {
        public List<Device> Devices = new List<Device>();
        public override void Chmod(string file, string perms)
        {
            throw new NotImplementedException();
        }
        public override void Chown(string file, string owner)
        {
            throw new NotImplementedException();
        }
        public void Init()
        {

        }
        public override List<FileInfo> GetEntries(string dir)
        {
            List<FileInfo> ret = new List<FileInfo>();
            for (int i = 0; i < Devices.Count; i++)
            {
                FileInfo f = new FileInfo();                
                f.Type = FileType.BlockDevice;
                f.Name = Devices[i].Name;
                ret.Add(f);
            }
            return ret;
        }
        public override Stream OpenFile(string file, int modes)
        {
            for (int i = 0; i < Devices.Count; i++)
            {

                if (Devices[i].Name == file)
                    return Devices[i].Open(modes);
            }
            throw new Exception("File not found");
        }
        
    }
}
