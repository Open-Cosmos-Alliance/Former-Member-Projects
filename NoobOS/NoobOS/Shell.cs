/* Shell.cs - a simple Bash-like shell for NoobOS
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
using NoobOS.FileSystem;
using Cosmos.Core;
using NoobOS.FileSystem.NoobFileSystem;
using NoobOS.Environment;
using NoobOS.Commands;

namespace NoobOS
{
    class Shell
    {
        /// <summary>
        /// Starts a new infinite loop that represents a simple shell
        /// </summary>
        public static void StartShell()
        {
            Helper.WriteLine("Starting Shell...", ConsoleColor.Green);
            NoobFile motd = NoobDirectory.GetFileByFullName("/etc/motd");
            if (motd == null)
            {
                Helper.Error("MOTD not available. Please type touch /etc/motd in the command line!");
            }
            else
            {

                Helper.WriteLine(motd.ReadAllText());
            }
            String cmd = "";
            do
            {
                Account u = new Account(GlobalEnvironment.Current["USER"]);
                String h = GlobalEnvironment.Current["HOST"];
                NoobDirectory dir = NoobDirectory.GetDirectoryByFullName(GlobalEnvironment.Current["CURRENTDIR"]);
                String consoleline = u.Username + "@" + h + ":" + dir.FullName + "# ";
                Helper.Write(consoleline);

                cmd = Helper.ReadLine();
                String[] t = cmd.SplitAtFirstSpace();
                String c = t[0]; // Command
                String a = t.Length > 1 ? t[1] : String.Empty; // Argument(s)

                Boolean p = CommandManager.ProcessCommand(c, a);

                if (!p) Helper.WriteLine("No such command found!");

                if (!Helper.LastLineIsEmpty())
                {
                    Helper.WriteLine("");
                }
            }
            while (true);
        }
    }
}