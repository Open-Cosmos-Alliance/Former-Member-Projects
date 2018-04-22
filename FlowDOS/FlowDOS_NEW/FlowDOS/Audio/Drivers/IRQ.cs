using System;
using System.Collections.Generic;
// IRQ code by Grunt
namespace dewitcher.Core
{
    public class IRQ
    {
        public static void SetMask(byte IRQline)
        {
            ushort port;
            byte value;

            if (IRQline < 8)
            {
                port = 0x20 + 1;
            }
            else
            {
                port = 0xA0 + 1;
                IRQline -= 8;
            }
            value = (byte)(IO.CDDI.inb(port) | (1 << IRQline));
            IO.CDDI.outb(port, value);
        }
        public static void ClearMask(byte IRQline)
        {
            ushort port;
            byte value;

            if (IRQline < 8)
            {
                port = 0x20 + 1;
            }
            else
            {
                port = 0xA0 + 1;
                IRQline -= 8;
            }
            value = (byte)(IO.CDDI.inb(port) & ~(1 << IRQline));
            IO.CDDI.outb(port, value);
        }
    }
}
