/* Kernel.cs - the main kernel file of NoobOS
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
using System.Text;
using Sys = Cosmos.System;
using NoobOS.Environment;
using NoobOS.FileSystem.Physical.Drivers;
using NoobOS.Commands;
using NoobOS.FileSystem.NoobFileSystem;
namespace NoobOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            int initres = 0;
            try
            {
                initres = Init.RunInit();
            }
            catch (Exception ex)
            {
                Helper.Error("Error! " + ex.Message);
                this.Stop();
                return;
            }
            if (Init.Error)
            {
                this.Stop();
                return;
            }
            if (initres == 1)
            {
                //Power Off
                this.Stop();
            }
            if (initres == 2)
            {
                //Reboot
                this.Stop();
            }
        }

        protected override void Run()
        {
            while (true)
            {
                Login();
            }
        }
        private void Login()
        {
            Account acc;
            do
            {
                acc = Account.DoLogin();
            } while (!acc.OK);
            GlobalEnvironment.Init(acc);
            CommandInit.Init();
            Shell.StartShell();
        }
        protected override void AfterRun()
        {
            
        }
    }
}
