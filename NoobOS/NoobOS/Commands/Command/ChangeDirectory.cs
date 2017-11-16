/* ChangeDirectory.cs - defines cd (change directory)
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
#region Namespace Imports

using System;
using NoobOS.Environment;
using NoobOS.FileSystem.NoobFileSystem;

#endregion

namespace NoobOS.Commands
{
	public class ChangeDirectory : CommandBase
	{
		public ChangeDirectory()
			: base("CD", 1)
		{
			Help = "Changes a directory";
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
            if (args == ".")
                return;
            else if (args == "..")
            {
                GlobalEnvironment.Current["CURRENTDIR"] = GetHomeDir(GlobalEnvironment.Current["CURRENTDIR"]);
            }
            else
            {
                var dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);
                var name = args;
                var d = dir.GetDirectoryByName(name);

                if (d == null)
                    d = NoobDirectory.GetDirectoryByFullName(name);

                if (d == null)
                    Helper.WriteLine("Can't find path");
                else
                    GlobalEnvironment.Current["CURRENTDIR"] = d.FullName;
            }
		}

        private string GetHomeDir(string dir)
        {
            if (dir == "/")
                return "/";
            else
            {
                var a = dir.Split('/');
                String homedir = "/";
                for (int i = 0; i < a.Length - 2; i++)
                {
                    homedir += a[i] + "/";
                }
                return homedir;
            }
        }
	}
}
