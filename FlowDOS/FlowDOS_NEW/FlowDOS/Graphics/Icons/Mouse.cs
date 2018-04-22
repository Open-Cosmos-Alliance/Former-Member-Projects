using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Icons
{
    class Mouse
    {
        private static uint[] icon = new uint[] {
            1,1,1,1,1,1,1,1,0,0,0,0,0,0,
            1,0,0,0,0,0,0,0,0,0,0,0,0,0,
            1,0,0,0,0,0,0,0,0,0,0,0,0,0,
            1,0,0,0,0,0,0,0,0,0,0,0,0,0,
            1,0,0,0,0,0,0,0,0,0,0,0,0,0,
            1,0,0,0,0,1,1,1,1,1,1,0,0,0,
            1,0,0,0,0,1,1,1,1,1,1,0,0,0,
            1,0,0,0,0,1,1,1,1,1,1,0,0,0,
            0,0,0,0,0,1,1,1,1,1,1,1,1,1,
            0,0,0,0,0,1,1,1,1,1,1,1,1,1,
            0,0,0,0,0,1,1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0,1,1,1,1,1,1,
        };

        public static void Draw(int x, int y)
        {
            Pictures.DrawBlackAndWhite(icon, 14, 14, x, y);
        }
    }
}
