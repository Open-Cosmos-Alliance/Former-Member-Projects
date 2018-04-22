using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Filesystem
{
    class DevMgr
    {
        public bool Init()
        {
            //Kernel.devFS = new IO.Filesystem.DevFS();

            Environment.RootFS.Mount(Environment.xDevFS, "/dev");

            Devices.ballmerDev Ballmer = new Devices.ballmerDev();
       

            Environment.xDevFS.Devices.Add(Ballmer);       //                      /dev/ballmer
            
           
           
            return true;
        }
    }
}
