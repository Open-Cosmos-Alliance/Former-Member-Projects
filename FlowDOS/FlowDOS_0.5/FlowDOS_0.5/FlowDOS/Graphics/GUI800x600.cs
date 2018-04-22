using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Graphics
{
    class GUI800x600
    {
        private static int xold = 0;
        private static int yold = 0;

        public static void Initialize()
        {
            SVGA.setres(800, 600);
            SVGA.clear(Colors.DesktopBlueBackground);
            SVGA.set(10, 10, 0xFF0000);
            SVGA.svga.SetPixel((ushort)15, (ushort)15, 0xFF0000);
            Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII svga1 = new Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII();
            svga1.SetMode((ushort)640, (ushort)480);
            svga1.Clear(Colors.DesktopBlueBackground);
            svga1.SetPixel((ushort)15, (ushort)15, 0xFF0000);
            FlowDOS.Hardware.Mouse.Initialize();
            xold = FlowDOS.Hardware.Mouse.X;
            yold = FlowDOS.Hardware.Mouse.Y;
            mainwh:
            xold = FlowDOS.Hardware.Mouse.X;
            yold = FlowDOS.Hardware.Mouse.Y;
            svga1.SetCursor(true, (uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y);
            while ((FlowDOS.Hardware.Mouse.X == xold) && (FlowDOS.Hardware.Mouse.Y == yold))
            {

            }
            goto mainwh;
        }




        public static void Initialize2()
        {



            FlowDOS.Hardware.Mouse.Initialize();

            /*while (true)
            {
                Console.WriteLine("X: " + FlowDOS.Hardware.Mouse.X + "; Y: " + FlowDOS.Hardware.Mouse.Y);
   
            }*/
            

            xold = FlowDOS.Hardware.Mouse.X;
            yold = FlowDOS.Hardware.Mouse.Y;

        mainloopone:
            while ((FlowDOS.Hardware.Mouse.X == xold) && (FlowDOS.Hardware.Mouse.Y == yold))
            {
                // code heure
                /* for (int i = 1; i < 10; i++)
                 {
                     scr.SetPixel(271, (uint)i, (uint)3);

                 }*/
                /*for (int i = 272; i < 306; i++)
                {
                    //scr.PixelHeight = 3;
                    scr.SetPixel((uint)i, (uint)1, (uint)6);
                    scr.SetPixel((uint)i, (uint)2, (uint)6);
                    scr.SetPixel((uint)i, (uint)3, (uint)6);
                    scr.SetPixel((uint)i, (uint)4, (uint)6);
                    scr.SetPixel((uint)i, (uint)5, (uint)6);
                    scr.SetPixel((uint)i, (uint)6, (uint)6);
                    scr.SetPixel((uint)i, (uint)7, (uint)6);
                    scr.SetPixel((uint)i, (uint)8, (uint)6);
                    scr.SetPixel((uint)i, (uint)9, (uint)6);
                }*/
                /*
                drawRect(271, 0, 36, 9, 6);


                //DrawFrameBlackAndWhite(FlowDOS.Font.Chars.A, 4, 7, 273, 2);

                switch(hourFormat)
                {
                    case "12":
                        if (Time.TwelveHourToString().Split(':')[0].Length == 1)
                        {
                            Font.Font.DrawText(273, 2, " " + Time.TwelveHourToString(), 0);
                        }
                        else
                        {
 Font.Font.DrawText(273, 2, Time.TwelveHourToString(), 0);
                        }
               
                if (Cosmos.Hardware.RTC.Hour > 12)
                {
                    Font.Font.DrawText(296, 2, "PM", 0);
                }
                else
                {
                    Font.Font.DrawText(296, 2, "AM", 0);
                }
                break;
                        case "24":
                Font.Font.DrawText(273, 2, " " + Time.TwentyFourHourToString(), 0);
                        break;
                }*/
            }

            //do
        //{
        returndoloop:


            do
            {
                switch (FlowDOS.Hardware.Mouse.Buttons)
                {
                    case Hardware.Mouse.MouseState.None:
                        //scr.Clear(1);
                       
                        /*if (SVGA.get((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 1) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 4) == (uint)2 &&
                                                      SVGA.get((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 4) == (uint)2)
                        {
                            goto returndoloop;
                        }*/
                        /*for (int a = 0; a < 320; a++)
                        {
                            for (int b = 0; b < 200; b++)
                            {
                                if ((SVGA.get((uint)a, (uint)b) == (uint)2) || (SVGA.get((uint)a, (uint)b) == (uint)4))
                                {
                                    SVGA.set((int)a, (int)b, (uint)1);
                                }
                            }
                        }*/
                        //setDesk();

                        Icons.Mouse.Draw(FlowDOS.Hardware.Mouse.X, FlowDOS.Hardware.Mouse.Y);
                        //setDesk();
                        break;
                    case Hardware.Mouse.MouseState.Left:
                        goto click;
                        break;
                    default:
                        break;
                }
                goto mainloopone;
            } while (true);

            //} while (FlowDOS.Hardware.Mouse.Buttons != FlowDOS.Hardware.Mouse.MouseState.None);



        click:
            

            
            
            
            

            
            
            
            
            
            
            
            if ((FlowDOS.Hardware.Mouse.X > 0 && FlowDOS.Hardware.Mouse.X <= 21 && FlowDOS.Hardware.Mouse.Y > 1 && FlowDOS.Hardware.Mouse.Y < 9))
            {
                //scr.SetTextMode(VGAScreen.TextSize.Size80x25);
                //fd.saveFile(bytenonchoix, "nonchoix", "");
                //nonchoix = true;
                //BeforeRun();
                //Run();
                Kernel.RebootACPI();
            }
            else
            {
                goto mainloopone;
            }

      
        }
    }
}
