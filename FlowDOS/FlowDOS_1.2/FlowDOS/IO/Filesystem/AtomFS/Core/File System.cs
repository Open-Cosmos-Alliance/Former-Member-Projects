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
    public abstract class FileSystem
    {
        public abstract void CreateFile(string Filepath, byte[] yData, byte Attrib);
        public abstract void CreateDirectory(string Filepath, byte Attrib);
        public abstract Entries[] GetEntries(string Dir);
        public abstract Entries[] GetEntries();
        public abstract void Format(string Label);
        public abstract UInt32 WriteAdd();
        public abstract void Init();
        public abstract byte[] GetFileData(string Dir, string FileName);
        //public abstract void Delete(string Dir, string FileName);
    }
}
