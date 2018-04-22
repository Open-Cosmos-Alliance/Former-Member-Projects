using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS
{
    class Graphs
    {
        public static void DrawHorizontalLine(ref VGAScreen VGA, uint x, uint y, uint Length, uint Thickness, byte Color)
        {
            for (uint i = x; i < x + Length; i++)
            {
                for (uint p = y; p < y + Thickness; p++)
                {
                    VGA.SetPixel(i, p, (uint)(Color));
                }
            }
        }

        public static void DrawVerticalLine(ref VGAScreen VGA, uint x, uint y, uint Length, uint Thickness, byte Color)
        {
            for (uint i = x; i < x + Thickness; i++)
            {
                for (uint p = y; p < y + Length; p++)
                {
                    VGA.SetPixel(i, p, (uint)(Color));
                }
            }
        }

        public static void DrawSquare(ref VGAScreen VGA, uint x, uint y, uint Size, byte Color)
        {
            //Use One Loop to improve speed
            uint p = 0;
            p = y;
            for (uint i = x; i <= x + Size; i++)
            {
                VGA.SetPixel(i, y, (uint)(Color));
                VGA.SetPixel(i, (y + Size), (uint)(Color));
                VGA.SetPixel(x, p, (uint)(Color));
                VGA.SetPixel((x + Size), p, (uint)(Color));
                p = p + 1;
            }

        }

        public static void DrawFillSquare(ref VGAScreen VGA, uint x, uint y, uint Size, byte Color)
        {
            //TODO: Use One Loop to improve speed

            for (uint j = y; j <= y + Size; j++)
            {
                for (uint i = x; i < x + Size; i++)
                {
                    VGA.SetPixel(i, j, (uint)(Color));
                }

            }
        }

        public static void DrawFillRectangle(ref VGAScreen VGA, uint x, uint y, uint xSize, uint ySize, uint Color)
        {

            //TODO: Use One Loop to improve speed
            for (uint y1 = y; y1 < y + ySize; y1++)
            {
                for (uint x1 = x; x1 < x + xSize; x1++)
                {
                    VGA.SetPixel(x1, y1, (uint)(Color));
                }

            }
        }

        public static void DrawRectangle(ref VGAScreen VGA, uint x, uint y, uint xSize, uint ySize, byte Color)
        {
            //Use One Loop to improve speed
            for (uint i = x; i < x + xSize; i++)
            {
                VGA.SetPixel(i, y, (uint)(Color));
                VGA.SetPixel(i, (y + ySize), (uint)(Color));
            }
            for (uint i = y; i <= y + ySize; i++)
            {
                VGA.SetPixel(x, i, (uint)(Color));
                VGA.SetPixel((x + xSize), i, (uint)(Color));
            }
        }


        //Window Drawing Functions

        public static void DrawNovaWnd(ref VGAScreen VGA, uint x, uint y, uint length, uint width, byte BorderColor)
        {
            //Function Buttons
            DrawRectangle(ref VGA, (x + length) - 30, y - 10, 30, 10, 8);
            DrawFillRectangle(ref VGA, (x + length) - 29, y - 9, 29, 8, BorderColor);

            //Seperators
            DrawVerticalLine(ref VGA, (x + length) - 10, y - 9, 10, 1, 8);
            DrawVerticalLine(ref VGA, (x + length) - 20, y - 9, 10, 1, 8);

            DrawRectangle(ref VGA, x, y, length, width, 8);
            DrawFillRectangle(ref VGA, x + 1, y + 1, length - 1, width - 2, BorderColor);
        }
     
    }
}
