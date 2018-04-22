using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Apps
{
    abstract public class App
    {
        public string Name { get; set; }

        abstract public void Initialize();


        abstract public void Execute();
       
    }
}
