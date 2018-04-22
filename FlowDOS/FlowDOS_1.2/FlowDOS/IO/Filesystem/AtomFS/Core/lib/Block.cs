/*
 * Written by: Aman Priyadarshi
 * Updated on: 03-04-2013
 */
/*
DO NOT REMOVE THIS WITHOUT PERMISSION

Copyright (c) 2013, Atom OS Team
All rights reserved.
Redistribution and use in source and binary forms, 
with or without modification, are permitted provided that the following conditions are met:

=> Redistributions of source code must retain the above copyright notice, 
   this list of conditions and the following disclaimer.

=> Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation and/or 
   other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom_File_System.Core.lib
{
    public static class Block
    {
        public static UInt32 GetNodeBlock(UInt32 Size)
        {
            UInt32 NodeBlock = 0;
            while (Size >= 512)
            {
                NodeBlock++;
                Size -= 512;
            }
            return NodeBlock;
        }

        public static int GetAddressInBlock(UInt32 Pos)
        {
            UInt32 NodeBlock = 0;
            while (Pos >= 512)
            {
                NodeBlock++;
                Pos -= 512;
            }
            return (int)Pos;
        }

        public static uint GetBlockSize(UInt32 SizeX)
        {
            uint Size = 0;
            while (SizeX >= 512)
            {
                Size++;
                SizeX -= 512;
            }
            if (SizeX > 0)
            {
                Size++;
            }
            return Size;
        }
    }
}

