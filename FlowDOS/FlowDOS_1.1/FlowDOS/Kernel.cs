using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace FlowDOSKernel
{
    public class Kernel : Sys.Kernel
    {
        FlowDOS.Core.Main mn = new FlowDOS.Core.Main();
        protected override void BeforeRun()
        {
            mn.Init();
        }

        protected override void Run()
        {
           
        }
    }
}
