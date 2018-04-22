using System;

namespace WitchcraftOS.Witchcraftfx.GUI
{
    public static class Screen
    {
        public enum colors : uint { black, white, red, green, blue, purple }
        public static Cosmos.Hardware.VGAScreen screen;
        public static Drawing.Mouse mouse;
        public static void Initialize()
        {
            screen = new Cosmos.Hardware.VGAScreen();
            screen.SetMode320x200x8(); 
            screen.SetPaletteEntry(0, 0, 0, 0); // black
            screen.SetPaletteEntry(1, 255, 255, 255); // white
            screen.SetPaletteEntry(2, 255, 0, 0); // red
            screen.SetPaletteEntry(3, 0, 255, 0); // green
            screen.SetPaletteEntry(4, 0, 0, 255); // blue
            screen.SetPaletteEntry(5, 255, 0, 255); // purple
            mouse = new Drawing.Mouse((uint)screen.PixelWidth / (uint)2, (uint)screen.PixelHeight / (uint)2);
            Font.SetupFont();
            Update();
        }
        public static void Update()
        {
            screen.Clear((int)colors.white);
            while (true)
            {
                Font.drawText("witchcraft desktop v" + Core.MainLoop.KernelVersion.ToString(), 4, 4, 0, ref screen);
                Drawing.drawCircle(0, 0, 40, 0);
                mouse.Update();
            }
        }
    }
}
