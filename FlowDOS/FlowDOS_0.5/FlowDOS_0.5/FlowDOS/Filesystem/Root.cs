using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    class Root : Filesystem
    {
        public List<MountPoint> GetMountPoints()
        {
            return this.mountedFileSystems;
        }

        public static int IndexOf(string str, char c)
        {
            int i = 0;
            foreach (char ch in str)
            {
                if (ch == c)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        public static bool Contains(string Str, char c)
        {
            foreach (char ch in Str)
            {
                if (ch == c)
                    return true;
            }
            return false;
        }

        public static string cleanName(string name)
        {
            if (!Contains(name, '/'))
                return name;
            if (name.Substring(0, 1) == "/")
            {
                name = name.Substring(1, name.Length - 1);
            }
            if (!Contains(name, '/'))
                return name;
            if (name.Substring(name.Length - 1, 1) == "/")
            {
                name = name.Substring(0, name.Length - 1);
            }
            return name;
        }

        private List<MountPoint> mountedFileSystems = new List<MountPoint>();
        public Filesystem getFS(string point)
        {
            for (int i = 0; i < mountedFileSystems.Count; i++)
            {

                if (mountedFileSystems[i].Path.Length <= point.Length)
                {
                    if ("/" + cleanName(point.Substring(0, mountedFileSystems[i].Path.Length)) + "/" == mountedFileSystems[i].Path)
                    {
                        return mountedFileSystems[i].Filesytem;
                    }
                }
            }
            throw new Exception("Mount point " + point + " does not exist.");
        }
        public string getFSName(string point)
        {
            for (int i = 0; i < mountedFileSystems.Count; i++)
            {
                if (mountedFileSystems[i].Path.Length <= point.Length)
                {
                    if ("/" + cleanName(point.Substring(0, mountedFileSystems[i].Path.Length)) + "/" == mountedFileSystems[i].Path)
                    {
                        return cleanName(point.Substring(mountedFileSystems[i].Path.Length));
                    }
                }
            }
            throw new Exception("Mount point " + point + " does not exist");
        }
        public void Umount(string point, bool force = false)
        {
            List<MountPoint> newMountPoints = new List<MountPoint>();
            for (int i = 0; i < mountedFileSystems.Count; i++)
            {
                if (mountedFileSystems[i].Path != point)
                    newMountPoints.Add(mountedFileSystems[i]);
            }
            this.mountedFileSystems = newMountPoints;
        }
        public void Mount(Filesystem fs, string point)
        {
            MountPoint mp = new MountPoint();
            mp.Path = "/" + cleanName(point) + "/";
            mp.Filesytem = fs;
            mountedFileSystems.Add(mp);
        }
        public override void Chmod(string file, string perms)
        {
            throw new NotImplementedException();
        }
        public override void Chown(string file, string owner)
        {
            throw new NotImplementedException();
        }
        public override List<FileInfo> GetEntries(string dir)
        {
            return getFS(dir).GetEntries(getFSName(dir));
        }
        public override Stream OpenFile(string file, int modes)
        {
            return getFS(file).OpenFile(getFSName(file), modes);
        }
        
    }
}
