using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iPear.Core.Screen.Handlers
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    class CosmosVGA
    {
        //Wrote by: Matt, for the PearOs team.
        private static Screen.Resolutions CurrentRes;
        private static Drivers.CosmosVGA VGA = new Drivers.CosmosVGA();

        public static void SetMode(Screen.Resolutions Res)
        {
            CurrentRes = Res;
            if (CurrentRes == Screen.Resolutions.Resolution1_320)
            {
                VGA.SetMode(320, 200);
            }
            else if (CurrentRes == Screen.Resolutions.Resolution2_640)
            {
                VGA.SetMode(640, 400);
            }
        }

        public static void Clear(int Color)
        {
            VGA.Clear(Color);
        }

        public static void Update()
        {
            VGA.Update();
        }

        public static void SetPixel(int x, int y, int Color)
        {
            VGA.SetPixel(x, y, Color);
        }

    }
}
