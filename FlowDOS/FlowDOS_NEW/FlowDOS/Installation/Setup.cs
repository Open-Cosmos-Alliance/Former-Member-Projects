using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Installation
{

    /*
Plasmid OS, Copyright (C) 2012-2013 PlasmidOS
See the Plasmid Binary Blobs license for more information.
*/

    class Setup
    {
        public static void Do()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Write("Initializing boot loader... ");
            Time.Wait(1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Initializing contents... ");
            Time.Wait(1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please wait... ");
            Time.Wait(1);
            Console.Clear();

            Console.WriteLine("Welcome to FlowDOS setup.");
            Console.WriteLine("Please select a partition to install onto.");
            for (int i = 0; i < Cosmos.Hardware.BlockDevice.Partition.Devices.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + " - Size: " + (Cosmos.Hardware.BlockDevice.Partition.Devices[i].BlockCount).ToString());
            }

            String a = Console.ReadLine();
            Cosmos.Hardware.BlockDevice.Partition part = (Cosmos.Hardware.BlockDevice.Partition)Cosmos.Hardware.BlockDevice.Partition.Devices[Int32.Parse(a) - 1];
            Console.WriteLine("FlowDOS will now format this partition. Press any key to continue.");
            //GruntyOS.HAL.GLNFS fs = new GruntyOS.HAL.GLNFS(part);
            Kernel.fd = new GruntyOS.HAL.GLNFS(part);

            Kernel.fd.Format("FlowDOS");
            Console.WriteLine("Format complete!");
            Console.Clear();
            Console.WriteLine("Setup part 2 - setup");
            Console.WriteLine("Enter a password for the administrator account.");

            string passwdf = "root:" + Console.ReadLine() +":0";
            Console.WriteLine("Your computer needs a name! Please enter it now");
            string name = Console.ReadLine();
            Console.WriteLine("Assembling system files, please wait");
            GruntyOS.CurrentUser.Username = "SYSTEM";
            GruntyOS.CurrentUser.Privilages = 0;
            //Kernel.fd.makeDir("/sys/bin", "SYSTEM");
            Kernel.CreateFolders();
            Kernel.fd.saveFile(FS.StringToByte(passwdf), "/usr/passwd", "SYSTEM");
            Kernel.fd.saveFile(FS.StringToByte(name), "/sys/conf/compname", "SYSTEM");
            //Kernel.fd.makeDir("/home/root", "root");

            Console.WriteLine("Install complete. Press any key to reboot...");
            Console.ReadKey();

            Kernel.RebootACPI();
                 


        }
    }
}
