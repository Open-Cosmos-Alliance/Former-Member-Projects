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

namespace Atom_File_System.Core
{
    public class Entries
    {
        public Entry EntryType;
        public UInt32 Position;
        public Byte[] EntryData;
        public string EntryName;

        public enum Entry
        {
            File,
            Directory,
            None
        };

        public Entries()
        {
            this.EntryName = "";
            this.EntryData = null;
            this.EntryType = Entry.None;
            this.Position = 0;
        }

        public Entries(Entry ET, UInt32 pos, byte[] dataX, string Name)
        {
            this.EntryName = Name;
            this.EntryData = dataX;
            this.EntryType = ET;
            this.Position = pos;
        }
    }
}

