using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS
{
    class GraphFuncs
    {
        #region 2D_drawer

        public static void DrawEQTriangleDown(ref VGAScreen scr, uint x, uint y, uint Base, byte Color)
        {
            //Use One Loop to improve speed
            uint p = 0;
            uint cst = Base / 2;
            uint rsy = y;
            uint rsx = x + Base;
            uint lsy = y;
            uint lsx = x;

            for (uint i = x; i <= x + Base; i++)
            {
                lsy = lsy - 1;
                lsx = lsx + 1;
                rsy = rsy - 1;
                rsx = rsx - 1;
                scr.SetPixel(i, y, (uint)(Color));
                if (p != cst)
                {
                    scr.SetPixel(lsx, lsy, (uint)(Color));
                    scr.SetPixel(rsx, rsy, (uint)(Color));
                    p = p + 1;
                }
            }
        }


        public static void DrawFillSquare(ref VGAScreen scr, uint x, uint y, uint Size, byte Color)
        {
            //TODO: Use One Loop to improve speed

            for (uint j = y; j <= y + Size; j++)
            {
                for (uint i = x; i < x + Size; i++)
                {
                    scr.SetPixel(i, j, (uint)(Color));
                }

            }
        }


        public static void DrawHorizontalLine(ref VGAScreen scr, uint x, uint y, uint Length, uint Thickness, byte Color)
        {
            for (uint i = x; i < x + Length; i++)
            {
                for (uint p = y; p < y + Thickness; p++)
                {
                    scr.SetPixel(i, p, (uint)(Color));
                }
            }
        }


   

        public static void DrawSquare(ref VGAScreen scr, uint x, uint y, uint Size, byte Color)
        {
            //Use One Loop to improve speed
            uint p = 0;
            p = y;
            for (uint i = x; i <= x + Size; i++)
            {
                scr.SetPixel(i, y, (uint)(Color));
                scr.SetPixel(i, (y + Size), (uint)(Color));
                scr.SetPixel(x, p, (uint)(Color));
                scr.SetPixel((x + Size), p, (uint)(Color));
                p = p + 1;
            }
        }

        public static void DrawFillRectangle(ref VGAScreen scr, uint x, uint y, uint xSize, uint ySize, uint Color)
        {

            //TODO: Use One Loop to improve speed
            for (uint y1 = y; y1 < y + ySize; y1++)
            {
                for (uint x1 = x; x1 < x + xSize; x1++)
                {
                    scr.SetPixel(x1, y1, (uint)(Color));
                }

            }
        }



        public static void drawRect(ref VGAScreen screen ,uint x, uint y, uint length, uint height, uint c)
        {
            for (uint l = 1; l <= length; l++)
            {
                for (uint h = 1; h <= height; h++)
                {
                    screen.SetPixel320x200x8(x + l, y + h, c);
                }
          
                
            }
        } 



        public static void DrawVerticalLine(ref VGAScreen scr, uint x, uint y, uint Length, uint Thickness, byte Color)
        {
            for (uint i = x; i < x + Thickness; i++)
            {
                for (uint p = y; p < y + Length; p++)
                {
                    scr.SetPixel(i, p, (uint)(Color));
                }
            }
        }


      







        #endregion 
    }
}
