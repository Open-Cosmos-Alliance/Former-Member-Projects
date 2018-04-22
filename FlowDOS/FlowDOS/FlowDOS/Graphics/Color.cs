using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware.Drivers.PCI.Video;

namespace FlowDOS
{
    // COLOR class
    // Original source : Quicksilver OS
    // All rights reserved
    public class color
    {
        public byte r, g, b, a;
        #region colors
        public static color white = new color(255, 255, 255);
        public static color black = new color(0, 0, 0);
        public static color blue = new color(0, 0, 255);
        public static color red = new color(255, 0, 0);
        public static color green = new color(0, 255, 0);
        #endregion
        public color(byte rd, byte gr, byte bl)
        {
            r = rd;
            g = gr;
            b = bl;
            a = 255;
        }
        public color(byte rd, byte gr, byte bl, byte alpha)
        {
            r = rd;
            g = gr;
            b = bl;
            a = alpha;
        }
        public uint touint()
        {
            return (uint)((this.r << 16) | (this.g << 8) | (this.b));
        }
        public static color fromuint(uint src)
        {
            double d1 = src % 65025;
            return new color((byte)(src / 65025), (byte)(d1 / 255), (byte)(d1 % 255));
        }
        public static color getalpha(byte solid, color par1, color par2)
        {
            double backr = (double)solid / (double)255;
            return new color((byte)((par1.r * backr) + (par2.r * (1d - backr))), (byte)((par1.g * backr) + (par2.g * (1d - backr))), (byte)((par1.b * backr) + (par2.b * (1d - backr))));
        }
    }
    // VEC2 class
    // Original source : Quicksilver OS
    // All rights reserved
    public class Vec2
    {
        public static Vec2 sixtyfour = new Vec2(64, 0);
        public static Vec2 zero = new Vec2(0, 0);
        public int x;
        public int y;
        public Vec2(int xpos, int ypos)
        {
            x = xpos;
            y = ypos;
        }
        public Vec2(Vec2 sz)
        {
            x = sz.x;
            y = sz.y;
        }
        public Vec2(rect rct)
        {
            x = rct.x;
            y = rct.y;
        }
        public Vec2()
        {
            x = 0;
            y = 0;
        }

    }
    public class Point
    {
       
        public static Point zero = new Point(0, 0);
        public int x;
        public int y;
        public Point(int xpos, int ypos)
        {
            x = xpos;
            y = ypos;
        }
        public Point(Vec2 sz)
        {
            x = sz.x;
            y = sz.y;
        }
        public Point(rect rct)
        {
            x = rct.x;
            y = rct.y;
        }
        public Point()
        {
            x = 0;
            y = 0;
        }

    }
    public class Size
    {

        public static Size Zero = new Size(0, 0);
        public int Width;
        public int Height;
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        
        }
        public Size(Vec2 sz)
        {
            Width = sz.x;
            Height = sz.y;
        }
        public Size(Point sz)
        {
            Width = sz.x;
            Height = sz.y;
        }
        public Size(rect rct)
        {
            Width = rct.width;
            Height = rct.height;
        }
        public Size()
        {
            this.Width = 0;
            this.Height = 0;
        }

    }
    // RECT class
    // Original source : Quicksilver OS
    // All rights reserved
    public class rect
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public rect(int xpos, int ypos, int width , int height)
        {
            x = xpos;
            y = ypos;
            this.width = width;
            this.height = height;
        }
        public rect(Vec2 sz, Vec2 pt)
        {
            x = pt.x;
            y = pt.y;
            //x = sz.x;
            //y = sz.y;
            width = sz.x;
            height = sz.y;
        }
        public rect(int xpos, int ypos, Vec2 sz)
        {
            x = xpos;
            y = ypos;
            x = sz.x;
            y = sz.y;
        }
        public rect(Vec2 pt, int mx, int my)
        {
            x = pt.x;
            y = pt.x;
            x = mx;
            y = my;
        }
        public rect()
        {
            x = 0;
            y = 0;
            x = 0;
            y = 0;
        }
    }
    class SVGA
    {
        public static Vec2 res = new Vec2(1300, 780);
        public static VMWareSVGAII svga = new VMWareSVGAII();
        public static void setres(int par1, int par2)
        {
            res = new Vec2(par1, par2);
            svga.SetMode((ushort)par1, (ushort)par2, 32);
        }
        public static void initmouse()
        {
            svga.DefineCursor();
        }
        public static void movemouse(Vec2 pos)
        {
            svga.SetCursor(true, (uint)pos.x, (uint)pos.y);
        }
        public static Vec2 getres()
        {
            return res;
        }
        public static void set(int x, int y, color col)
        {
            svga.SetPixel((ushort)x, (ushort)y, col.touint());
        }
        public static void set(int x, int y, uint col)
        {
            svga.SetPixel((ushort)x, (ushort)y, col);
        }
        public static color get(int x, int y)
        {
            return color.fromuint(svga.GetPixel((ushort)x, (ushort)y));
        }
        public static uint get(uint x, uint y)
        {
            return svga.GetPixel((ushort)x, (ushort)y);
        }
        public static void fill(Vec2 point, Vec2 size, color par1)
        {
            svga.Fill((ushort)point.x, (ushort)point.y, (ushort)size.x, (ushort)size.y, par1.touint());
        }
        public static void fill(Vec2 point, Vec2 size, uint par1)
        {
            svga.Fill((ushort)point.x, (ushort)point.y, (ushort)size.x, (ushort)size.y, par1);
        }
        public static void fill(rect tr, uint par1)
        {
            svga.Fill((ushort)tr.x, (ushort)tr.y, (ushort)tr.x, (ushort)tr.y, par1);
        }
        public static void clear(color par1)
        {
            svga.Clear(par1.touint());
        }
        public static void clear(uint par1)
        {
            svga.Clear(par1);
        }
        public static void update()
        {
            svga.Update((ushort)0, (ushort)0, (ushort)res.x, (ushort)res.y);
        }
    }
}

