using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Filesystem.Praxis
{
    class MountService
    {
        private static Drive[] drives = new Drive[8];
        private static byte used = 0;
        public static void Mount(Praxis partition, string point)
        {
            drives[used] = new Drive(point, partition);
            used++;
        }
        public static Praxis Get(string point)
        {
            for (byte i = 0; i < 8; i++) { if (drives[i].mountpath == point) { return drives[i].partition; } }
            return null;
        }
    }
    class Drive
    {
        public string mountpath = "/";
        public Praxis partition;
        public Drive(string mount, Praxis part)
        {
            mountpath = mount; partition = part;
        }
    }
}
