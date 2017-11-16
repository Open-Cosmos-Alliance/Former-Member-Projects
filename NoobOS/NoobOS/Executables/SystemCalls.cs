using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.IL2CPU;
using Cosmos.IL2CPU.Plugs;
using Cosmos.Core;
using System.Runtime.InteropServices;
using CPUx86 = Cosmos.Assembler.x86;
namespace NoobOS.Executables
{
    [Plug(Target = typeof(global::Cosmos.Core.INTs))]
    class Interrupts
    {
        public static void HandleInterrupt_Default(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            if (aContext.Interrupt >= 0x20 && aContext.Interrupt <= 0x2F)
            {
                if (aContext.Interrupt >= 0x28)
                {
                    Cosmos.Core.Global.PIC.EoiSlave();
                }
                else
                {
                    Cosmos.Core.Global.PIC.EoiMaster();
                }
            }
            if (aContext.Interrupt == 0x80)
            {
                RegMan.RegSplit splitty = new RegMan.RegSplit { EXX = aContext.EAX };
                byte al = splitty.XL;
                //SystemCalls.Call(aContext, al);
                if (al == 0x01)
                {
                    Console.Clear();
                }
                else if (al == 0x02)
                {
                    //Move a pointer to an address for storing the characters into EBX!!
                    uint ramaddress = aContext.EBX + BinaryLoader.Address;
                    unsafe
                    {
                        RegMan.Sti();
                        char input = Console.ReadKey().KeyChar;
                        byte* data = (byte*)(ramaddress);
                        data[0] = (byte)input;
                    }
                }
                else if (al == 0x03)
                {
                    uint address = aContext.EBX + BinaryLoader.Address;
                    uint length = aContext.ECX;
                    unsafe
                    {
                        byte* characters = (byte*)address;
                        for (int i = 0; i < length; i++)
                        {
                            if (characters[i] != 0x0A)
                            {
                                Console.Write((char)characters[i]);
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                }
                else if (al == 0x04)
                {
                    uint x = aContext.EBX;
                    uint y = aContext.ECX;
                    Cosmos.System.Global.Console.X = (int)x;
                    Cosmos.System.Global.Console.Y = (int)y;
                }
            }
        }
    }
    #region ASM Plug
    public static class RegMan
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct RegSplit
        {
            [FieldOffset(0)]
            public uint EXX; //Like EAX
            [FieldOffset(0)]
            public byte XL; //Like AL
            [FieldOffset(1)]
            public byte XH; //Like AH
            [FieldOffset(0)]
            public ushort XX; //Like AX 
        }
        [PlugMethod(Assembler = typeof(StiImpl))]
        public static void Sti() { } //Plug
    }
    public class StiImpl : AssemblerMethod
    {
        public override void AssembleNew(object aAssembler, object aMethodInfo)
        {
            new CPUx86.Sti();
        }
    }
    #endregion
}
