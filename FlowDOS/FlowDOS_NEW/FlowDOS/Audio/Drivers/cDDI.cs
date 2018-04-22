using System;
using System.Collections.Generic;
using Cosmos.Core;

namespace dewitcher.IO
{
    public static class CDDI
    {
        /// <summary>
        /// Reads a byte
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static byte inb(ushort port)
        {
            IOPort io = new IOPort(port);
            return io.Byte;
        }
        /// <summary>
        /// Reads a 16 bit word
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ushort inw(ushort port)
        {
            IOPort io = new IOPort(port);
            return io.Word;
        }
        /// <summary>
        /// Reads a 32 bit word
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static uint inl(ushort port)
        {
            IOPort io = new IOPort(port);
            return io.DWord;
        }
        /// <summary>
        /// Writes a byte
        /// </summary>
        /// <param name="port"></param>
        /// <param name="data"></param>
        public static void outb(ushort port, byte data)
        {
            IOPort io = new IOPort(port);
            io.Byte = data;
        }
        /// <summary>
        /// Writes a 16 bit word
        /// </summary>
        /// <param name="port"></param>
        /// <param name="data"></param>
        public static void outw(ushort port, ushort data)
        {
            IOPort io = new IOPort(port);
            io.Word = data;
        }
        /// <summary>
        /// Writes a 32 bit word
        /// </summary>
        /// <param name="port"></param>
        /// <param name="data"></param>
        public static void outl(ushort port, uint data)
        {
            IOPort io = new IOPort(port);
            io.DWord = data;
        }
    }
}