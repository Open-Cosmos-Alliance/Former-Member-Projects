/* Copyright (C) 2013 GruntXProductions
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Sys = Cosmos.System;
using Cosmos.IL2CPU.Plugs;
using Cosmos.Core;
using Cosmos;


namespace GDOS
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    /* 
     * This class plugs the defualt exception handlers
     * used by COSMOS. If ANY of these get called,
     * a BSOD will occur
     */
    public class INTs
    {
        static bool already = false;
        private static string[] errs = new string[] { "DIVIDE_BY_ZERO", "SINGLE_STEP", "NON_MASKABLE_INTERRUPT", "BREAK_FLOW", "OVERFLOW", "NULL", "INVALID_OPCODE", "", "DOUBLE_FAULT_EXCEPTION", "INVALID_TSS", "SEGMENT_NOT_PRESENT", "STACK_EXCEPTION", "GENERAL_PROTECTION_FAULT" };
       
        public static void HandleInterrupt_Default(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            
            API.HandleInt0x80(ref aContext);
           // Interrupts.Handlers[aContext.Interrupt](ref aContext);

        }
        public static void HandleInterrupt_00(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[0]);
        }

     
        public static void HandleInterrupt_01(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[1]);
        }
        public static void HandleInterrupt_02(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[2]);
        }
        public static void HandleInterrupt_03(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[3]);
        }
        public static void HandleInterrupt_04(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[4]);
        }
        public static void HandleInterrupt_05(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[5]);
        }
        public static void HandleInterrupt_06(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[6]);
        }
        public static void HandleInterrupt_07(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[7]);
        }
        public static void HandleInterrupt_08(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[8]);
        }
        public static void HandleInterrupt_09(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[9]);
        }
        public static void HandleInterrupt_0A(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[10]);
        }
        public static void HandleInterrupt_0B(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[11]);
          
        }
        public static void HandleInterrupt_0C(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[12]);
        }
        public static void HandleInterrupt_0D(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[13]);
        }
        public static void HandleInterrupt_0E(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[14]);
        }
        public static void HandleInterrupt_0F(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            FlowDOS.BSOD.Panic(errs[15]);
        }
    }
}
