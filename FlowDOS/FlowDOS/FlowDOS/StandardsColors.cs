using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Colors
    {
        public static uint DesktopBlueBackground = 0x3D9FED;
    }
    class Pictures
    {
        public static void Draw(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 0xFF00FF)
                    {
                    }
                    else
                    {
                        
                        SVGA.set((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), (uint)Arr[count]);
                    }
                }
            }
        }
        public static void DrawBlackAndWhite(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == (uint)0)
                    {
                    }
                    else if (Arr[count] == (uint)1)
                    {
                        SVGA.set((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0xFFFFFF);
                    }
                    else if (Arr[count] == (uint)2)
                    {
                        SVGA.set((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0x000000);
                    }
                    else
                    {
                        //SVGA.set((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), (uint)Arr[count]);
                    }
                }
            }
        }
    }
}
