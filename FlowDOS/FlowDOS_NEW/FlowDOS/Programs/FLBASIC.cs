using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Programs
{
    class FLBASIC
    {
        public static int Run(string code)
        {
            string[] fb = code.Split('\n');
            for (int i = 0; i < fb.Length - 1; i++)
            {
                if (fb[i].Substring(0, 7) == "PRINT ")
                {

                }
            }
            return 0;
        }
    }
}
