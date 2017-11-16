/* Init.cs - the initialization system for NoobOS
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
using Cosmos.Core;
using Cosmos.Hardware;
using Cosmos.Hardware.BlockDevice;
using Cosmos.System.Filesystem.Listing;
using NoobOS.FileSystem.Physical;
using NoobOS.FileSystem.NoobFileSystem;
using NoobOS.FileSystem.Physical.Drivers;
namespace NoobOS
{
    class Init
    {
        public static bool Error = false;

        private static PrimaryPartition workPartition = null;

        /// <summary>
        /// The Init of NoobOS System.
        /// </summary>
        public static int RunInit()
        {
            #region Version
            Helper.WriteLine("NoobOS 0.5.1 Alpha Booting...", ConsoleColor.DarkMagenta);
            #endregion
            #region Memory
            Helper.WriteLine("Checking Memory...");
            uint mem = CPU.GetAmountOfRAM();
            Helper.Write("Memory: "+(mem+2)+" MB ");
            Helper.WriteLine("OK", ConsoleColor.Green);
            #endregion
            #region Disks and Partitions
            Helper.WriteLine("Getting Disks...");
            IDE[] IDEs = IDE.Devices.ToArray();
            Helper.WriteLine("Number of IDE disks: " + IDEs.Length);
            Helper.WriteLine("Looking for valid partitions...");
            for (int i = 0; i < IDEs.Length; i++)
            {
                PrimaryPartition[] parts = IDEs[i].PrimaryPartitions;
                for (int j = 0; j < parts.Length; j++)
                {
                    if (parts[j].Infos.SystemID == 0xFA)
                    {
                        workPartition = parts[j];
                    }
                }
            }
//#warning Revert to == null!!!
            if (workPartition == null)
            {
                DiskHandler.CreatePartitions(IDEs);
                Helper.WriteLine("The machine needs to be restarted.");
                return 2;
            }
            Helper.Done();
            #endregion
            #region FileSystem
            Helper.Write("Checking FileSystem... ");
            NoobFileSystem fs;
            try
            {
                fs = new NoobFileSystem(workPartition);
                NoobFileSystem.AddMapping("/", fs);
                Helper.Done();
            }
            catch (Exception ex)
            {
                Helper.Error("Error!" + ex.Message);
                Error = true;
            }
            #endregion
            #region Installation
            if (NoobFileSystem.mFS.Root.GetDirectoryByName("etc") == null)
            {
                Helper.WriteLine("Welcome to NoobOS!");
                Helper.WriteLine("The basic directories needed for running are not present.");
                Helper.WriteLine("If you're newly installing NoobOS, this is normal, otherwise, you've probably deleted something bad");
                Helper.WriteLine("This will delete all file system contents.");
                if (!Helper.Continue())
                {
                    return 1;
                }
                Helper.Write("Cleaning partition...");
                NoobEntry[] entries = NoobFileSystem.mFS.Root.GetEntries();
                for (int i = 0; i < entries.Length; i++)
                {
                    if (entries[i] is NoobDirectory)
                    {
                        NoobFileSystem.mFS.Root.RemoveDirectory(entries[i].Name);
                    }
                    if (entries[i] is NoobFile)
                    {
                        NoobFileSystem.mFS.Root.RemoveFile(entries[i].Name);
                    }
                }
                Helper.Done();
                Helper.Write("Creating required directories and files...");
                //Create Directories and check them
                try
                {
                    CreateDirectoryAndVerify("etc");
                    CreateDirectoryAndVerify("bin");
                    CreateDirectoryAndVerify("sbin");
                    CreateDirectoryAndVerify("proc");
                    CreateDirectoryAndVerify("usr");
                    CreateDirectoryAndVerify("home");
                    CreateDirectoryAndVerify("root");
                    CreateDirectoryAndVerify("tmp");
                    CreateDirectoryAndVerify("var");
                    CreateDirectoryAndVerify("srv");
                    CreateDirectoryAndVerify("lib");
                    CreateDirectoryAndVerify("opt");
                    CreateDirectoryAndVerify("dev");
                    CreateFileAndVerify("passwd", "etc");
                    CreateFileWithContentsAndVerify("motd", "etc", "Welcome to NoobOS 0.5.1!\nLeave a message to anyone who logs in by editing /etc/motd.\nThanks for using NoobOS!");
                    CreateFileWithContentsAndVerify("witcher", "bin", new byte[] { 0xB0, 0x03, 0x66, 0xBB, 0x11, 0x00, 0x00, 0x00, 0x66, 0xB9, 0x07, 0x00, 0x00, 0x00, 0xCD, 0x80, 0xC3, 0x57, 0x69, 0x74, 0x63, 0x68, 0x21, 0x0A });
                }
                catch (Exception e)
                {
                    Helper.Error("Error! " + e.Message);
                    return 1;
                }
                /*NoobFileSystem.mFS.Root.AddDirectory("inf");
                NoobFileSystem.mFS.Root.AddDirectory("bin");
                NoobDirectory infdir = NoobFileSystem.mFS.Root.GetDirectoryByName("inf");
                if (infdir == null)
                {
                    Helper.Error("Cannot create required directories...");
                    Helper.WriteLine("Aborting...");
                    return 1;
                }
                NoobDirectory bindir = NoobFileSystem.mFS.Root.GetDirectoryByName("bin");
                if (bindir == null)
                {
                    Helper.Error("Cannot create required directories...");
                    Helper.WriteLine("Aborting...");
                    return 1;
                }
                infdir.AddFile("Accounts");
                //Create Files and check them
                NoobFile Accfile = infdir.GetFileByName("Accounts");
                if (Accfile == null)
                {
                    Helper.Error("Cannot create required files...");
                    Helper.WriteLine("Aborting...");
                    return 1;
                }
                bindir.AddFile("witcher");
                NoobFile Witchfile = bindir.GetFileByName("witcher");
                if (Witchfile == null)
                {
                    Helper.Error("Cannot create required files...");
                    Helper.WriteLine("Aborting...");
                    return 1;
                }
                else
                {
                    Witchfile.WriteAllBytes();
                }*/
                Helper.Done();
                String newus;
                String newpass;
                do
                {
                    Helper.WriteLine("New user required");
                    newus = Helper.ReadLine("Username: ");
                    Helper.Write("Password: ");
                    newpass = Helper.ReadLine(true);
                } while (!Account.Add(newus,newpass));
                Helper.NoError("User Added Succesfully");
            }
            #endregion
            return 0;
        }
        private static NoobDirectory sf;
        private static void CreateDirectoryAndVerify(string directory)
        {
            NoobFileSystem.mFS.Root.AddDirectory(directory);
            sf = NoobFileSystem.mFS.Root.GetDirectoryByName(directory);
            if (sf == null)
            {
                Helper.Error("Cannot create required files, aborting creation of " + directory);
                throw new Exception("Failed creation of directories.");
            }
        }
        private static void CreateFileAndVerify(string file, string directory)
        {
            NoobDirectory dir = NoobFileSystem.mFS.Root.GetDirectoryByName(directory);
            if (dir != null)
            {
                dir.AddFile(file);
                NoobFile nf = dir.GetFileByName(file);
                if (nf == null)
                {
                    throw new Exception("Could not create!");
                }

            }
            else
            {
                throw new ArgumentException("Bad directory");
            }
        }
        private static void CreateFileWithContentsAndVerify(string file, string directory, byte[] data)
        {
            NoobDirectory dir = NoobFileSystem.mFS.Root.GetDirectoryByName(directory);
            if (dir != null)
            {
                dir.AddFile(file);
                NoobFile nf = dir.GetFileByName(file);
                if (nf == null)
                {
                    throw new Exception("Could not create!");
                }
                else
                {
                    nf.WriteAllBytes(data);
                }

            }
            else
            {
                throw new ArgumentException("Bad directory");
            }
        }
        private static void CreateFileWithContentsAndVerify(string file, string directory, string data)
        {
            NoobDirectory dir = NoobFileSystem.mFS.Root.GetDirectoryByName(directory);
            if (dir != null)
            {
                dir.AddFile(file);
                NoobFile nf = dir.GetFileByName(file);
                if (nf == null)
                {
                    throw new Exception("Could not create!");
                }
                else
                {
                    nf.WriteAllText(data);
                }

            }
            else
            {
                throw new ArgumentException("Bad directory");
            }
        }
    }
 
}
