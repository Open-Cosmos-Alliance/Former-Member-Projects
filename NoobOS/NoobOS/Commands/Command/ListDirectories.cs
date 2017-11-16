/* ListDirectories.cs - defines ls (equivalent to dir in Windows).
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
	public class ListDirectories : CommandBase
	{
		public ListDirectories()
			: base("LS", 0)
		{
			Help = "Retrives the list of directories";
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
			var dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);
			var dirs = dir.GetDirs();
			var files = dir.GetFiles();

			dirs.ForEeach(d => Helper.Write(d.ToString() + " ", ConsoleColor.Blue));

			files.ForEeach(f => Helper.Write(f.ToString() + " ", ConsoleColor.Green));
		}
	}
}
