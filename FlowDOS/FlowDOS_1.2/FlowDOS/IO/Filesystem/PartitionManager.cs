using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Hardware.BlockDevice;

namespace FlowDOS.IO.Filesystem
{
    class PartitionManager
    {
        char coinhaut1 = (char)(byte)201; // ╔
        char coinbas1 = (char)(byte)200; // ╚
        char coinhaut2 = (char)(byte)187; // ╗
        char coinbas2 = (char)(byte)188; // ╝

        char droitegauche2 = (char)(byte)185; // ╣
        char droitegauche1 = (char)(byte)204; // ╠

        char hautbas = (char)(byte)186; // ║
        char droitegauche = (char)(byte)205; // ═

        public List<Partition> Parts = new List<Partition>();

        public void Init()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 80 * 25; i++)
            {
                Console.Write(' ');
            }


            Console.CursorLeft = 1;
            Console.CursorTop = 1;

            //Console.SetCursorPosition(1, 1);
            Console.Write(coinhaut1);

            for (int j = 1; j < 12; j++)
            {
                Console.Write(droitegauche);
            }

            Console.CursorLeft = 13;
            Console.CursorTop = 0;
            Console.Write(coinhaut1);
            Console.CursorLeft = 13;
            Console.CursorTop = 1;
            Console.Write(droitegauche2);

            //

            Console.CursorLeft = 13;
            Console.CursorTop = 2;
            Console.Write(coinbas1);

            Console.CursorTop = 0;

            for (int k = 1; k < 53; k++)
            {
                Console.Write(droitegauche);
            }



            Console.CursorTop = 0;

            Console.CursorLeft = 66;
            Console.Write(coinhaut2);
            Console.CursorLeft = 66;
            Console.CursorTop = 1;
            Console.Write(droitegauche1);
            Console.CursorLeft = 66;
            Console.CursorTop = 2;
            Console.Write(coinbas2);

            Console.CursorTop = 1;

            Console.CursorTop = 1;
            Console.CursorLeft = 14;
            Console.Write("   FlowDOS Partition Manager 1.2 - Partition list   ");

            Console.CursorTop = 1;
            Console.CursorLeft = 67;

            for (int j = 1; j < 12; j++)
            {
                Console.Write(droitegauche);
            }

            Console.Write(coinhaut2);

            Console.CursorLeft = 14;
            Console.CursorTop = 2;

            for (int k = 1; k < 53; k++)
            {
                Console.Write(droitegauche);
            }





            if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    var xDevice = BlockDevice.Devices[i];
                    if (xDevice is Partition)
                    {
                        Parts.Add((Partition)xDevice);
                    }
                }
            }


        }
    }
}
