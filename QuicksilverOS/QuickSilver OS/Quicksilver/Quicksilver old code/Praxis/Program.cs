using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Praxis;
using Praxis.Emulator;

namespace Praxis.Examples
{
    class Program
    {
        static VDisk vd;
        static PartitionTable pt;
        static Partition part;
        static PraxisPartition praxpart;
        static void Main(string[] args)
        {
            //starts VDisk
            vd = VDisk.Create(1024);
            pt = new PartitionTable(vd);
            //initializes partition
            part = Partitioner.Create(pt, 1024);
            PraxisFormatter.format(part, "system");
            //formats
            praxpart = new PraxisPartition(part);
            PraxisPartitionTable.Add(praxpart);
            //creates file
            Praxis.IO.File.Create("/system/test1.txt", Encoding.UTF8.GetBytes("Hello, World. This is test 1.".PadLeft(1976, 'x')));
            Praxis.IO.File.Create("/system/test2.txt", Encoding.UTF8.GetBytes("Hello, World. This is test 2."));
            Praxis.IO.File.Create("/system/test3.txt", Encoding.UTF8.GetBytes("Hello, World. This is test 3."));
            //writes contents to console
            Console.Write(Encoding.UTF8.GetString(Praxis.IO.File.Read("/system/test1.txt")));//Encoding.UTF8.GetString(Praxis.IO.File.get("system", 0)).Replace(((char)0).ToString(), ""));
            Console.ReadKey();
        }
    }
}
