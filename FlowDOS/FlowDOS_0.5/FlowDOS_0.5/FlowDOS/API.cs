using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Cosmos.IL2CPU.Plugs;
using CPUx86 = Cosmos.Assembler.x86;
namespace GDOS
{
    public class API
    {
        public static void STI()
        {
        }
        // Basically the way this works is a number is stored in EAX, this is the function
        // we want to use. All of these can be accessed through software interrupt 0x80
        public unsafe static void HandleInt0x80(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            if (aContext.EAX == 1) // Print
            {
                byte* ptr = (byte*)aContext.ESI;
                for (int i = 0; ptr[i] != 0; i++)
                {
                    Console.Write((char)ptr[i]);
                }
            }
            else if (aContext.EAX == 2) // Read
            {
                STIEnabler se = new STIEnabler();
                se.Enable();
                STI(); // We need to enable interrupts so we can read, but for some reason this does not work :(

                byte* ptr = (byte*)aContext.EDI; // Input buffer
                string str = Console.ReadLine();
                for (int i = 0; i < str.Length; i++)
                    ptr[i] = (byte)str[i];
            }
        }
    }
    class STIEnabler
    {
        public void Enable()
        {
        }
    }
    [Plug(Target = typeof(STIEnabler))]
    public class Enable : AssemblerMethod
    {
        public override void AssembleNew(object aAssembler, object aMethodInfo)
        {
            new CPUx86.Sti();

        }
    }
}
