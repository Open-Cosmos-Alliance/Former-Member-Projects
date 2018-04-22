using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iPear.Core.Screen
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    class Screen
    {
        //Wrote by: Matt, for the PearOs team.
        private static Driver CurrentDriver;
        public static Resolutions CurrentRes;
        public static int Width = 0;
        public static int Height = 0;

        /// <summary>
        /// Lists the resolutions available
        /// </summary>
        public enum Resolutions
        {
            Resolution1_320 = 1,
            Resolution2_640 = 2,
            Resolution3_800 = 3,
            Resolution4_1024 = 4,
            Resolution5_1280 = 5,
            Resolution6_1600 = 6
         };
        
        public enum Driver
        {
            CosmosVGA = 1,
            VMWareSVGA = 2
        };
        /// <summary>
        /// Selects the driver for use
        /// </summary>
        /// <param name="driver">The driver to use of type Screen.Driver</param>
        public static void SetDriver(Driver driver)
        {
            CurrentDriver = driver;
        }
        /// <summary>
        /// Set the resolution
        /// </summary>
        /// <param name="res">The resolution to use of type Screen.Resolutions</param>
        public static void SetMode(Resolutions res)
        {
            CurrentRes = res;
            if (CurrentRes == Screen.Resolutions.Resolution1_320)
            {
                Width = 320;
                Height = 200;
            }
            else if (CurrentRes == Screen.Resolutions.Resolution2_640)
            {
                Width = 640;
                Height = 400;
            }
            else if (CurrentRes == Screen.Resolutions.Resolution3_800)
            {
                Width = 800;
                Height = 600;
            }
            else if (CurrentRes == Screen.Resolutions.Resolution4_1024)
            {
                Width = 1024;
                Height = 768;
            }
            else if (CurrentRes == Screen.Resolutions.Resolution5_1280)
            {
                Width = 1280;
                Height = 1024;
            }
            else if (CurrentRes == Screen.Resolutions.Resolution6_1600)
            {
                Width = 1600;
                Height = 1200;
            }
            if (CurrentDriver == Driver.VMWareSVGA)
            {
                Handlers.VMWareSVGA.SetMode(res);
            }
            else if (CurrentDriver == Driver.CosmosVGA)
            {
                Handlers.CosmosVGA.SetMode(res);
            }
        }

        public static void SetPixel(int x, int y, int Color)
        {
            if (CurrentDriver == Driver.VMWareSVGA)
            {
                Handlers.VMWareSVGA.SetPixel(x, y, Color);
            }
            else if (CurrentDriver == Driver.CosmosVGA)
            {
                Handlers.CosmosVGA.SetPixel(x, y, Color);
            }
        }

        public static void Clear(int Color)
        {
            if (CurrentDriver == Driver.VMWareSVGA)
            {
                Handlers.VMWareSVGA.Clear(Color);
            }
            else if (CurrentDriver == Driver.CosmosVGA)
            {
                Handlers.CosmosVGA.Clear(Color);
            }
        }

        public static void Update()
        {
            if (CurrentDriver == Driver.VMWareSVGA)
            {
                Handlers.VMWareSVGA.Update();
            }
            else if (CurrentDriver == Driver.CosmosVGA)
            {
                Handlers.CosmosVGA.Update();
            }
        }

        #region " Pre Defined " 
        private static int i = 0;
        private static int t = 0;
        private static int count = 0;
        private static int bb, cc;
        #endregion
        public static void DrawFrame(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            count = 0;
            for (i = 0; i < length; i++)
            {
                for (t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 0xFF00FF)
                    {
                    }
                    else
                    {
                        SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), (int)Arr[count]);
                    }
                }
            }
        }
        public static void DrawFrameColor(uint[] Arr, int width, int length, int xpixel, int ypixel,int color)
        {
            count = 0;
            for (i = 0; i < length; i++)
            {
                for (t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 1)
                    {
                        SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), color);
                    }
                }
            }
        }
        public static void DrawFrameBlackAndWhite(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            count = 0;
            for (i = 0; i < length; i++)
            {
                for (t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 1)
                    {
                        SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0x000000);
                    }
                    if (Arr[count] == 2)
                    {
                        SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0xffffff);
                    }
                }
            }
        }
        public static void DrawRectangle(int x, int y, int width, int length, int color)
        {
            for (bb = x; bb < x + width; bb++)
            {
                for (cc = y; cc < y + length; cc++)
                {
                    SetPixel(bb, cc, color);
                }
            }
        }

     }
}
