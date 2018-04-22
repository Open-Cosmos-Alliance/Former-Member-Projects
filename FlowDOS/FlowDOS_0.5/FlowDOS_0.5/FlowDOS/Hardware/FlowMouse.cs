﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Core;
using Cosmos;
using Cosmos.Hardware;
//using Cosmos.Kernel;

namespace FlowDOS.Hardware
{
    /// <summary>
    /// This class describes the mouse.
    /// </summary>
    class Mouse
    {
        private static Cosmos.Core.IOGroup.Mouse g = new Cosmos.Core.IOGroup.Mouse();

        /// <summary>
        /// The X location of the mouse.
        /// </summary>
        public static int X;
        /// <summary>
        /// The Y location of the mouse.
        /// </summary>
        public static int Y;
        /// <summary>
        /// The state the mouse is currently in.
        /// </summary>
        public static MouseState Buttons;

        /// <summary>
        /// This is the required call to start
        /// the mouse receiving interrupts.
        /// </summary>
        public static void Initialize()
        {
            ////enable mouse
            WaitSignal();
            g.p64.Byte = (byte)0xA8;

            //// enable interrupt
            WaitSignal();
            g.p64.Byte = (byte)0x20;
            WaitData();
            //byte status1 = (byte)(g.p60.Byte);
            byte status = (byte)(g.p60.Byte | 2);
            WaitSignal();
            g.p64.Byte = (byte)0x60;
            WaitSignal();
            g.p60.Byte = (byte)status;

            ////default
            Write(0xF6);
            Read();  //Acknowledge

            ////Enable the mouse
            Write(0xF4);
            Read();  //Acknowledge

            INTs.SetIrqHandler(12, HandleMouse);
            //Console.WriteLine("INSTALLED");
        }

        private static byte Read()
        {
            WaitData();
            return g.p60.Byte;
        }

        private static void Write(byte b)
        {
            WaitSignal();
            g.p64.Byte = 0xD4;
            WaitSignal();
            g.p60.Byte = b;
        }

        private static void WaitData()
        {
            for (int i = 0; i < 100 & ((g.p64.Byte & 1) == 1); i++)
                ;
        }

        private static void WaitSignal()
        {
            for (int i = 0; i < 100 & ((g.p64.Byte & 2) != 0); i++)
                ;
        }

        /// <summary>
        /// The possible states of a mouse.
        /// </summary>
        public enum MouseState
        {
            /// <summary>
            /// No button is pressed.
            /// </summary>
            None = 0,
            /// <summary>
            /// The left mouse button is pressed.
            /// </summary>
            Left = 1,
            /// <summary>
            /// The right mouse button is pressed.
            /// </summary>
            Right = 2,
            /// <summary>
            /// The middle mouse button is pressed.
            /// </summary>
            Middle = 4
        }


        private static byte[] mouse_byte = new byte[4];
        private static byte mouse_cycle = 0;

        public static void HandleMouse(ref INTs.IRQContext context)
        {
            switch (mouse_cycle)
            {
                case 0:
                    mouse_byte[0] = Read();

                    //Bit 3 of byte 0 is 1, then we have a good package
                    if ((mouse_byte[0] & 0x8) == 0x8)
                        mouse_cycle++;

                    break;
                case 1:
                    mouse_byte[1] = Read();
                    mouse_cycle++;
                    break;
                case 2:
                    mouse_byte[2] = Read();
                    mouse_cycle = 0;

                    if ((mouse_byte[0] & 0x10) == 0x10)
                        X -= (mouse_byte[1] ^ 0xff);
                    else
                        X += mouse_byte[1];

                    if ((mouse_byte[0] & 0x20) == 0x20)
                        Y += (mouse_byte[2] ^ 0xff);
                    else
                        Y -= mouse_byte[2];

                    if (X < 0)
                        X = 0;
                    else if (X > 319)
                        X = 319;

                    if (Y < 0)
                        Y = 0;
                    else if (Y > 199)
                        Y = 199;

                    Buttons = (MouseState)(mouse_byte[0] & 0x7);

                    break;
            }

        }
    }
}