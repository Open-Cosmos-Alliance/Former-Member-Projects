using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Core
{
    public unsafe class Power
    {
        public static unsafe void Hibernate()
        {
            uint end = Cosmos.Core.CPU.GetEndOfKernel();
            byte[] ram = new byte[end];
            for (int i = 0; i < end; i++)
            {
                byte* ptr = (byte*)0;
                ram[i] = ptr[i];
                Console.WriteLine("0x" + ptr[i]);
                //Global.CurrentFS.WriteFile("/sys/hibernate.conf", ram);
            }
        }

        public static unsafe void RestoreHibernate()
        {
            byte[] hf = Global.CurrentFS.ReadFile("/sys/hibernate.conf");
            uint end = Cosmos.Core.CPU.GetEndOfKernel();
            //byte[] ram = new byte[end];
            for (int i = 0; i < end; i++)
            {
                byte* ptr = (byte*)0;
                ptr[i] = hf[i];
                //Console.WriteLine("0x" + ptr[i])
            }
        }
    }
}
