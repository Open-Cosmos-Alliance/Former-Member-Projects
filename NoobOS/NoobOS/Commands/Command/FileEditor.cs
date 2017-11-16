/* FileEditor.cs - a basic file editor for NoobOS
 * Copyright (C) 2012 NoobOS
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
using System.Linq;
using System.Text;
using NoobOS.FileSystem.NoobFileSystem;
namespace NoobOS.Commands
{
    class FileEditor : CommandBase
    {
        public FileEditor() :
            base("editor", 1)
        {
            Help = "Allows you to edit any file";
            AccessRights = AccessRights.Everyone;
        }

        public override string Help
        {
            get;
            set;
        }

        public override AccessRights AccessRights
        {
            get;
            set;
        }

        public override bool CanExecute(String args)
        {
            return true;
        }

        public override void Execute(String args)
        {

        }
    }
} 
