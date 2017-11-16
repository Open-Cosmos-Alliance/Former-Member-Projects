/* HelpCommand.cs - defines help
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

namespace NoobOS.Commands
{
    class HelpCommand: CommandBase
	{
        public HelpCommand()
			: base("HELP", 0)
		{
			Help = "Displays available list of commands - type 'Help command' for specific help";
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
                var InternalCommands = CommandManager.GetCommands();
                for (int i = 0; i < InternalCommands.Length; i++)
                {
                    Helper.WriteLine(InternalCommands[i].Name.ToLower() + " - " + InternalCommands[i].Help, ConsoleColor.Blue);
                }
            }
            else
            {
                CommandBase c = CommandManager.GetCommand(args);

                if (c == null)
                    Helper.WriteLine("No such command found!");
                else
                    Helper.WriteLine(c.Name + " - " + c.Help, ConsoleColor.Blue);
            }
        }
	}
}
