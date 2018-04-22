using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware;

namespace FlowDOS.Graphics.Windows
{
    class Window
    {
        public List<Control> Controls = new List<Control>();
        public Point Position = new Point();
        public Size Size = new Size();

        public void Draw(ref VGAScreen scr)
        {
        }


        private void DrawRectWithoutFill(ref VGAScreen scr,int x, int y, int width, int height, uint color)
        {
            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
                {
                    if (j == y)
                    {
                    }
                    else
                    {
                    }
                    scr.SetPixel((uint)i, (uint)j, color);
                }
            }
        }
    }
}
