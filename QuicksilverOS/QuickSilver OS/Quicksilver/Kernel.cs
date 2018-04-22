using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using x86 = Cosmos.Assembler.x86;
using SMBIOS = Cosmos.Hardware.SMBIOS;
using Quicksilver.Shells;

namespace Quicksilver
{
    public class Kernel : Sys.Kernel
    {
        public bool didcommand = true;
        string commandwpar = "";
        //public static string cd = "/";
        //public static GDOS.VirtualFileSystem FileSystem;
        //Praxis.Emulator.VDisk vd = Praxis.Emulator.VDisk.Create(4096);
        //Praxis.Emulator.PartitionTable pt;
        //Praxis.Emulator.Partition part;
        //Praxis.PraxisPartition prax;
        public static string cd = "/";
        public static Shell current;
        public static GruntyOS.HAL.GLNFS fs;
        Cosmos.Hardware.TextScreen ts = new Cosmos.Hardware.TextScreen();
        Cosmos.Hardware.Mouse mouse = new Cosmos.Hardware.Mouse();
        protected override void BeforeRun()
        {
            #region GLNFS
            GruntyOS.HAL.ATA.Detect(); // This will detect all ATA devices and add them to the device filesystem
            GruntyOS.CurrentUser.Privilages = 0; // This has to be set, 1 = limited 0 = root
            GruntyOS.CurrentUser.Username = "Admin"; // When using anything in the class File this will be the default username


            GruntyOS.HAL.FileSystem.Root = new GruntyOS.HAL.RootFilesystem(); // initialize virtual filesystem
            GruntyOS.HAL.FileSystem.Root.Seperator = '/';
            bool ispart = false;
            for (int i = 0; i < GruntyOS.HAL.Devices.dev.Count; i++)
            {
                if (GruntyOS.HAL.Devices.dev[i].dev is Cosmos.Hardware.BlockDevice.Partition)
                {
                    GruntyOS.HAL.GLNFS FS = new GruntyOS.HAL.GLNFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev);

                    if (GruntyOS.HAL.GLNFS.isGFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev))
                    {
                        Console.WriteLine("Drive Detected");
                        ispart = true;
                        cd = "/";
                        fs = FS;
                        GruntyOS.HAL.FileSystem.Root.Mount("/", FS);

                    }
                    else
                    {
                        Console.Write("Filesystem Label: "); new GruntyOS.HAL.GLNFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev).Format(Console.ReadLine());
                        Cosmos.Core.Global.CPU.Reboot();
                    }
                }
            }
            if (!ispart) new fdisk().Execute(new string[1]);
            #endregion
            fs.makeDir("/root", "Admin");
            Console.WriteLine("Welcome to Quicksilver OS Alpha 1.0.0.44 as of 24.02.13\r\nCopyright (c) 2013");

            if (fs.ListFiles("/root").Contains("users.sys"))
            {
                string pswd = GruntyOS.IO.File.Open("/root/password.sys");
                string s = GruntyOS.IO.File.Open("/root/users.sys");
                string[] parts = s.Split(':');
                bool password_correct = false;
                while (!password_correct) {
                    Console.Write("Please enter your password, " + parts[0] + ": ");
                    if (Console.ReadLine() == (pswd))
                    {
                        Console.Write("You have sucessfully logged in.");
                        UserService.user = parts[0];
                        cd = parts[2];
                        password_correct = true;
                    }
                    else
                    {
                        Console.Write("Incorrect password.");
                    }
                }
            }
            else
            {
                Console.Write("Please pick a username and Password\r\nUsername: ");
                UserService.user = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                GruntyOS.IO.File.Save("/root/users.sys", UserService.user + ":/:/home" + ":ROOT");
                GruntyOS.IO.File.Save("/root/password.sys", password);
                Console.WriteLine("Account " + UserService.user + " has been created. Press a key to continue.");
                cd = "/home";
                Console.Read();
            }
            Parser.Init();
            Console.Clear();
            current = new Prompt();
            
        }
        protected override void Run()
        {
            current.Run();
        }
    }
    static class QArray
    {
        public static bool Contains(this string[] x, string s)
        {
            foreach (string var0 in x) if (s == var0) return true;
            return false;
        }
    }
}
