using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Filesystem.Praxis.IO
{
    class File
    {
        public static void Create(string path, byte[] content)
        {
            string[] paths = path.Split('/');
            Praxis p = MountService.Get(paths[1]);
            if (p != null) {
                ulong sectors = p.nextAvailableSector;
                for (int i = 2; i < paths.Length; i++) {
                    if (paths[i] == "" && i != paths.Length - 1) {

                    }
                    if (i == paths.Length - 1) {
                        p.writeEntry(0xF0, Quicksilver.Impl.String.GetHashCode(paths[2]), sectors);
                        WriteFile(paths[2], sectors, content, p);
                    }
                }
            }
        }
        private static void WriteFile(string name, ulong sector, byte[] content, Praxis part) {
            

        }
    }
}
