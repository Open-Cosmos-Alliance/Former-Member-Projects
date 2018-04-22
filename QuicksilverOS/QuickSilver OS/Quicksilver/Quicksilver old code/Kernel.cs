using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using x86 = Cosmos.Assembler.x86;
using SMBIOS = Cosmos.Hardware.SMBIOS;

namespace Quicksilver2013
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
        public static string cd = "/system";
        public static GDOS.VirtualFileSystem vfs;
        Cosmos.Hardware.TextScreen ts = new Cosmos.Hardware.TextScreen();
        Cosmos.Hardware.Mouse mouse = new Cosmos.Hardware.Mouse();
        protected override void BeforeRun()
        {
            #region SMBIOS
            SMBIOS.SMBIOS smbios = new SMBIOS.SMBIOS();
            //gets smbios
            if (smbios.CheckSMBIOS())
            {
                smbios.GetSMBIOS_Data();
                List<SMBIOS.BaseInfo> bi = smbios.GetHardwareDescriptorByType( SMBIOS.TableTypes.ProcessorInformation );
                if(bi.Count >= 1) {
                    Console.ReadLine();
                    cpuid.pi = (SMBIOS.Table.ProcessorInformation)bi[0];
                }
            }
            #endregion
            #region GLNFS
            
            GruntyOS.HAL.ATA.Detect(); // This will detect all ATA devices and add them to the device filesystem
            Console.WriteLine("Welcome to Quicksilver OS Alpha 1.0.0.22 as of 130202-1133\r\nCopyright (c) 2013");
            Console.Write("Please pick a username: ");
            UserService.user = Console.ReadLine();
            GruntyOS.CurrentUser.Privilages = 0; // This has to be set, 1 = limited 0 = root
            GruntyOS.CurrentUser.Username = UserService.user; // When using anything in the class File this will be the default username

            GruntyOS.HAL.FileSystem.Root = new GruntyOS.HAL.RootFilesystem(); // initialize virtual filesystem
            bool ispart = false;
            for (int i = 0; i < GruntyOS.HAL.Devices.dev.Count; i++)
            {
                if (GruntyOS.HAL.Devices.dev[i].dev is Cosmos.Hardware.BlockDevice.Partition)
                {
                    GruntyOS.HAL.GLNFS FS = new GruntyOS.HAL.GLNFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev);

                    FS.Format("system");
                    if (GruntyOS.HAL.GLNFS.isGFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev))
                    {

                        Console.WriteLine("Drive detected!");
                        ispart = true;
                    }
                    else { Console.Write("Filesystem Label: "); new GruntyOS.HAL.GLNFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev).Format(Console.ReadLine()); }
                    GruntyOS.HAL.FileSystem.Root.Mount("/" + FS.DriveLabel, FS); // mount it as root (you can only have on partition mounted as root!!!!
                    if (!ispart) new fdisk().Execute(new string[1]);
                    cd = "/" + FS.DriveLabel;
                    var d = new GDOS.Drive();
                    d.Filesystem = FS;
                    d.DeviceFile = GruntyOS.HAL.Devices.dev[i].name;
                    vfs.AddDrive(d);
                }
            }
            
            #endregion
            //quicksilver praxis
            //pt = new Praxis.Emulator.PartitionTable(vd);
            //part = Praxis.Emulator.Partitioner.Create(pt, 4096);
            //Praxis.PraxisFormatter.format(part, "system");
            //prax = new Praxis.PraxisPartition(part);
            //Praxis.PraxisPartitionTable.Add(prax);
            Parser.Init();
            mouse.Initialize();
            Console.Clear();
            //Praxis.IO.File.Create("/system/hello.com", Quicksilver2013.Files.Exes.hello);
            vfs.saveFile(Quicksilver2013.Files.Exes.hello, cd + "/system/hello.exe", UserService.user);


        }
        protected override void Run()
        {
            QuicksilverNEXT.Console.Write(UserService.user + "@" + cd  + "# ");
            QuicksilverNEXT.Console.Flush();
            commandwpar = Console.ReadLine();
            Parser.Parse(commandwpar);
        }
    }
}
