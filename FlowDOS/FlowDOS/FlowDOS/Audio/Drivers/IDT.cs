using System;
using System.Collections.Generic;
// IDT code by Grunt
namespace dewitcher.Core
{
    public class IDT
    {
        public delegate void ISR();
        public static ISR[] idt = new ISR[0xFF];
        public static void Remap()
        {
            IO.CDDI.outb(0x20, 0x11);
            IO.CDDI.outb(0xA0, 0x11);
            IO.CDDI.outb(0x21, 0x20);
            IO.CDDI.outb(0xA1, 0x28);
            IO.CDDI.outb(0x21, 0x04);
            IO.CDDI.outb(0xA1, 0x02);
            IO.CDDI.outb(0x21, 0x01);
            IO.CDDI.outb(0xA1, 0x01);
            IO.CDDI.outb(0x21, 0x0);
            IO.CDDI.outb(0xA1, 0x0);
        }
        private void idt_handler()
        {
            int num = 0;
            if (idt[num] != null)
            {
                idt[num]();
            }
        }

        public static void SetGate(byte int_num, ISR handler)
        {
            idt[int_num] = handler;
        }

    }
}
