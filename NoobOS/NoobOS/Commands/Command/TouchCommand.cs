/* TouchCommand.cs - defines touch
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
	public class TouchCommand : CommandBase
	{
		public TouchCommand()
			: base("TOUCH", 1)
		{
			Help = String.Empty;
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
			if (String.IsNullOrEmpty(args))
			{
				Helper.WriteLine("Either no or invalid arguments specified!");
				return;
			}

			var dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);
			String[] str = args.Split('/');
			if (str.Length > 1)
			{
				String fulldir = dir.FullName;
				for (int i = 0; i < str.Length - 1; i++)
				{
					fulldir += str[i] + "/";
				}
				NoobDirectory app = NoobDirectory.GetDirectoryByFullName(fulldir);
				if (app != null && str[str.Length - 1] != null && str[str.Length - 1] != "")
				{
					app.AddFile(str[str.Length - 1]);
				}
			}
			else
			{
				dir.AddFile(args);
			}
		}
	}
}
