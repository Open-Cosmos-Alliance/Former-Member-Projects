using System;

using Cosmos.Hardware;

namespace WitchcraftOS.Witchcraftfx.GUI
{
    public static class Drawing
    {
        public static void FillRectangle(uint __x, uint __y, uint __width, uint __height, uint __cindex)
        {
            for (uint y = __y; y != __height; y++)
            {
                for (uint x = __x; x != __width; x++)
                {
                    Screen.screen.SetPixel320x200x8(x, y, __cindex);
                }
            }
        }
        public static void DrawRectangle(uint __x, uint __y, uint __width, uint __height, uint __cindex)
        {
            for (uint x = __x; x != __width; x++)
            {
                Screen.screen.SetPixel320x200x8(x, __y, __cindex);
                Screen.screen.SetPixel320x200x8(x, __y + __height, __cindex);
            }
            for (uint y = __y; y != __height; y++)
            {
                Screen.screen.SetPixel320x200x8(__x, y, __cindex);
                Screen.screen.SetPixel320x200x8(__x + __width, y, __cindex);
            }
        }
        public static void drawLine(uint x, uint y, uint length, uint color, bool vertical)
        {
            if (vertical)
            {
                //Is vertical
                for (uint l = 0; l <= length; l++)
                {
                    Screen.screen.SetPixel320x200x8(x, y + l, color);
                }
            }
            else
            {
                //Is NOT vertical
                for (uint l = 0; l <= length; l++)
                {
                    Screen.screen.SetPixel320x200x8(x + l, y, color);
                }

            }


        }
        public static void drawCircle(int x0, int y0, int radius, uint color)
        {
            int f = 1 - radius;
            int ddF_x = 1;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            Screen.screen.SetPixel320x200x8((uint)x0, (uint)(y0 + radius), color);
            Screen.screen.SetPixel320x200x8((uint)x0, (uint)(y0 - radius), color);
            Screen.screen.SetPixel320x200x8((uint)(x0 + radius), (uint)y0, color);
            Screen.screen.SetPixel320x200x8((uint)(x0 - radius), (uint)y0, color);

            while (x < y)
            {
                // ddF_x == 2 * x + 1;
                // ddF_y == -2 * y;
                // f == x*x + y*y - radius*radius + 2*x - y + 1;
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;
                Screen.screen.SetPixel320x200x8((uint)(x0 + x), (uint)(y0 + y), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 - x), (uint)(y0 + y), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 + x), (uint)(y0 - y), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 - x), (uint)(y0 - y), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 + y), (uint)(y0 + x), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 - y), (uint)(y0 + x), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 + y), (uint)(y0 - x), color);
                Screen.screen.SetPixel320x200x8((uint)(x0 - y), (uint)(y0 - x), color);
            }
        }
        public class Mouse
        {
            private uint X, Y;
            private uint oldX, oldY;
            private Cosmos.Hardware.Mouse mouse;
            public Mouse(uint X, uint Y)
            {
                this.X = X;
                this.Y = Y;
                this.mouse = new Cosmos.Hardware.Mouse();
                this.mouse.Initialize();
                this.mouse.X = (int)this.X;
                this.mouse.Y = (int)this.Y;
            }
            public void Update()
            {
                this.X = (uint)mouse.X;
                this.Y = (uint)mouse.Y;
                if (oldX != X || oldY != Y) UnDraw();
                Draw();
            }
            public void Draw()
            {
                Screen.screen.SetPixel320x200x8(X, Y, 0);

                Screen.screen.SetPixel320x200x8(X + 1, Y, 0);
                Screen.screen.SetPixel320x200x8(X + 2, Y, 0);
                Screen.screen.SetPixel320x200x8(X + 3, Y, 0);
                Screen.screen.SetPixel320x200x8(X + 4, Y, 0);

                Screen.screen.SetPixel320x200x8(X, Y + 1, 0);
                Screen.screen.SetPixel320x200x8(X, Y + 2, 0);
                Screen.screen.SetPixel320x200x8(X, Y + 3, 0);
                Screen.screen.SetPixel320x200x8(X, Y + 4, 0);

                Screen.screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                Screen.screen.SetPixel320x200x8(X + 2, Y + 1, 0);

                Screen.screen.SetPixel320x200x8(X + 1, Y + 1, 0);
                Screen.screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                Screen.screen.SetPixel320x200x8(X + 3, Y + 3, 0);
                Screen.screen.SetPixel320x200x8(X + 4, Y + 4, 0);
                Screen.screen.SetPixel320x200x8(X + 5, Y + 5, 0);
                Screen.screen.SetPixel320x200x8(X + 6, Y + 6, 0);

                this.oldX = this.X;
                this.oldY = this.Y;
            }
            public void UnDraw()
            {
                Screen.screen.SetPixel320x200x8(oldX, oldY, 1);

                Screen.screen.SetPixel320x200x8(oldX + 1, oldY, 1);
                Screen.screen.SetPixel320x200x8(oldX + 2, oldY, 1);
                Screen.screen.SetPixel320x200x8(oldX + 3, oldY, 1);
                Screen.screen.SetPixel320x200x8(oldX + 4, oldY, 1);

                Screen.screen.SetPixel320x200x8(oldX, oldY + 1, 1);
                Screen.screen.SetPixel320x200x8(oldX, oldY + 2, 1);
                Screen.screen.SetPixel320x200x8(oldX, oldY + 3, 1);
                Screen.screen.SetPixel320x200x8(oldX, oldY + 4, 1);

                Screen.screen.SetPixel320x200x8(oldX + 1, oldY + 2, 1);
                Screen.screen.SetPixel320x200x8(oldX + 2, oldY + 1, 1);

                Screen.screen.SetPixel320x200x8(oldX + 1, oldY + 1, 1);
                Screen.screen.SetPixel320x200x8(oldX + 2, oldY + 2, 1);
                Screen.screen.SetPixel320x200x8(oldX + 3, oldY + 3, 1);
                Screen.screen.SetPixel320x200x8(oldX + 4, oldY + 4, 1);
                Screen.screen.SetPixel320x200x8(oldX + 5, oldY + 5, 1);
                Screen.screen.SetPixel320x200x8(oldX + 6, oldY + 6, 1);
            }
        }
    }
    public static class useclass
    {
        public static void use<T>(this T thizz, Action<T> act)
        {
            act(thizz);
        }
    }
}