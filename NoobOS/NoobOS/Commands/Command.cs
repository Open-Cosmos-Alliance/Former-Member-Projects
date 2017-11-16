/* Command.cs - defines the base for commands in NoobOS
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

#endregion

namespace NoobOS.Commands
{
	/// <summary>
	/// Defines <see cref="CommandBase"/>
	/// </summary>
	public abstract class CommandBase
	{
		#region Ctor

		/// <summary>
		/// Creates an instance of <see cref="CommandBase"/>
		/// </summary>
		/// <param name="nArgs">Number of arguments required by this command</param>
		public CommandBase(string name, int nArgs)
		{
			if (String.IsNullOrEmpty(name)) throw new InvalidOperationException("Can not create a command with the specified name.");

			NumArgs = nArgs;
			Name = name;
		}

		#endregion

		#region Memebers

		/// <summary>
		/// Gets the name of this command.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the help text for this coommand.
		/// </summary>
		public abstract string Help { get; set; }

		/// <summary>
		/// Gets or set who have access to this command.
		/// </summary>
		public abstract AccessRights AccessRights { get; set; }

		/// <summary>
		/// Gets the number of arguments required by this command.
		/// </summary>
		public int NumArgs { get; private set; }

		#endregion

		#region Implementations

		/// <summary>
		/// Determines whether the command and arguments are valid and can be excecuted.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public abstract bool CanExecute(String args);

		/// <summary>
		/// Defines the action of this command.
		/// </summary>
		/// <param name="args"></param>
		public abstract void Execute(String args);

		#endregion
	}

	/// <summary>
	/// Defines a <see cref="Command"/>
	/// </summary>
	public class Command : CommandBase
	{
		#region Ctor

		public Command(string name, int nArgs, Action<IList<Object>> exec, Predicate<IList<Object>> canExec)
			: base(name,nArgs)
		{

		}

		#endregion

		#region ** CommandBase

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
			if (_canExec == null) return true;

			return _canExec(args);
		}

		public override void Execute(String args)
		{
			if (_exec == null) return;

			_exec(args);
		}

		#endregion

		#region Fields

		private Predicate<String> _canExec = null;
		private Action<String> _exec = null;

		#endregion
	}
}