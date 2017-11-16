/* CommandManager.cs - manages commands
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NoobOS.FileSystem.NoobFileSystem;
#endregion

namespace NoobOS.Commands
{
	/// <summary>
	/// Defines static methods to work with commands.
	/// </summary>
	public static class CommandManager
	{
		#region Ctor

		static CommandManager()
		{
			cmds = new List<CommandBase>();
		}

		#endregion

		#region Implementations

		/// <summary>
		/// Registers a command with the <see cref="CommandManager"/>.
		/// </summary>
		/// <param name="cmd">the command instance to register.</param>
		/// <returns>true if registration was successful, false otherwise.</returns>
		public static bool Register(CommandBase cmd)
		{
			if (cmd == null) return false;

			Boolean reg = Find(cmd.Name) != null;

			if (reg) return false; // A command of same name exists.

			cmds.Add(cmd);

			return true;
		}

		/// <summary>
		/// Un-Registers a command from the <see cref="CommandManager"/>.
		/// </summary>
		/// <param name="cmd">the command instance to unregister</param>
		public static void UnRegister(CommandBase cmd)
		{
			cmds.Remove(cmd);
		}

		/// <summary>
		/// Determinse wether a command instance is registered with <see cref="CommandManager"/>.
		/// </summary>
		/// <param name="cmd">the command instance.</param>
		/// <returns>true if command manager contains the command, false otherwise.</returns>
		public static bool Contains(CommandBase cmd)
		{
			return cmds.Contains(cmd);
		}

		/// <summary>
		/// Gets the command of the specified name.
		/// </summary>
		/// <param name="name">the name of the command</param>
		/// <returns>the command with the specified name.</returns>
		public static CommandBase GetCommand(String name)
		{
			return Find(name);
		}

		/// <summary>
		/// Finds command with the specified name.
		/// </summary>
		/// <param name="cmd">the same to look for.</param>
		/// <returns>the command with the name if exists, null otherwise.</returns>
		private static CommandBase Find(String cmd)
		{
			for (int i = 0; i < cmds.Count; i++)
			{
				if (cmds[i].Name.ToLower().IsEqual(cmd.ToLower()))
					return cmds[i];
			}

			return null;
		}

		/// <summary>
		/// Executes the command specified.
		/// </summary>
		/// <param name="cmd">the command name</param>
		/// <param name="args">arguments associated with the command</param>
		/// <returns>true if there is a command of the name and executed, false other wise</returns>
		public static bool ProcessCommand(String cmd, String args)
		{
			CommandBase c = Find(cmd);

            if (c == null)
            {
                NoobDirectory dir = NoobDirectory.GetDirectoryByFullName(NoobOS.Environment.GlobalEnvironment.Current["CURRENTDIR"]);
                if (dir != null)
                {
                    NoobFile fl = dir.GetFileByName(cmd);
                    if (fl != null)
                    {
                        Helper.WriteLine("Please make sure the file you have selected IS A BINARY or expect lots of crashes!");
                        if (Helper.Continue())
                        {
                            NoobOS.Executables.BinaryLoader.CallRaw(fl.ReadAllBytes());
                            return true;
                        }
                    }
                }
                return false;
            }

			if(c.CanExecute(args))
				c.Execute(args);

			return true;
		}

		public static CommandBase[] GetCommands()
		{
		    return cmds.ToArray();
		}

		/// <summary>
		/// Gets all the commands registered with the <see cref="CommandManager"/>.
		/// </summary>
		/// <returns>commands list.</returns>
		public static ReadOnlyCollection<CommandBase> Commands
		{
			get
			{
				return new ReadOnlyCollection<CommandBase>(GetCommands());
			}
		}

		#endregion

		#region Fields

		public static List<CommandBase> cmds= null;

		#endregion
	}
}
