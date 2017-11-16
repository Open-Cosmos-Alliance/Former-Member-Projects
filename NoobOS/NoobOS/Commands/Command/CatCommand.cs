/* CatCommand.cs - defines cat (concatenate)
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
	public class CatCommand : CommandBase
	{
		public CatCommand()
			: base("CAT", 1)
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
				Helper.WriteLine("target File non specified");
				return;
			}

			var dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);
			
			NoobFile file = dir.GetFileByName(args);

			if(file == null)
				file = NoobDirectory.GetFileByFullName(args);

			if (file == null)
			{
				Helper.WriteLine("Can't find file");
			}
			else
			{
				String content = file.ReadAllText();
				Helper.WriteLine(content);
			}
		}
	}
}
