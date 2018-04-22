using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;
using Cosmos.Hardware;
using Cosmos.Hardware.BlockDevice;
//using Cosmos.System.Filesystem.Listing;
using Cosmos.System.Filesystem.FAT;
using Microsoft.VisualBasic;
using GruntyOS.HAL;
using CPUx86 = Cosmos.Assembler.x86;
//using GruntyOS.IO;

using Cosmos.Hardware2.Storage;

namespace FlowDOS
{
    public class Kernel : Sys.Kernel
    {
        public static string CurrentUser = "";
        public string currentDir = "/home/root";
        public static GLNFS fd;
        bool connected = false;
        //static ArkadiaOsKernel.Core.Filesystem.Vfs.HardDrive hdd;
      //ArkadiaOsKernel.Core.Filesystem.Vfs.Disk disk = new ArkadiaOsKernel.Core.Filesystem.Vfs.Disk("C");
        //FileSystem.HardDrive hdd = new FileSystem.HardDrive();
        Cosmos.System.Network.UdpClient client = new Cosmos.System.Network.UdpClient();
        bool nonchoix = false;

        public static byte[] Example_exe = new byte[] { 0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21, 0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20, 0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20, 0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x50, 0x45, 0x00, 0x00, 0x4C, 0x01, 0x03, 0x00, 0x66, 0x15, 0x18, 0x51, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE0, 0x00, 0x02, 0x01, 0x0B, 0x01, 0x0B, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x9E, 0x27, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x40, 0x85, 0x00, 0x00, 0x10, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x44, 0x27, 0x00, 0x00, 0x57, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x30, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0xB0, 0x26, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x20, 0x00, 0x00, 0x48, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2E, 0x74, 0x65, 0x78, 0x74, 0x00, 0x00, 0x00, 0xA4, 0x07, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x60, 0x2E, 0x72, 0x73, 0x72, 0x63, 0x00, 0x00, 0x00, 0x30, 0x05, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x40, 0x2E, 0x72, 0x65, 0x6C, 0x6F, 0x63, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x27, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x00, 0x00, 0x00, 0x02, 0x00, 0x05, 0x00, 0x64, 0x20, 0x00, 0x00, 0x4C, 0x06, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2E, 0x72, 0x01, 0x00, 0x00, 0x70, 0x28, 0x11, 0x00, 0x00, 0x0A, 0x2A, 0x1E, 0x02, 0x28, 0x12, 0x00, 0x00, 0x0A, 0x2A, 0x42, 0x53, 0x4A, 0x42, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x76, 0x34, 0x2E, 0x30, 0x2E, 0x33, 0x30, 0x33, 0x31, 0x39, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0x6C, 0x00, 0x00, 0x00, 0xE4, 0x01, 0x00, 0x00, 0x23, 0x7E, 0x00, 0x00, 0x50, 0x02, 0x00, 0x00, 0x84, 0x02, 0x00, 0x00, 0x23, 0x53, 0x74, 0x72, 0x69, 0x6E, 0x67, 0x73, 0x00, 0x00, 0x00, 0x00, 0xD4, 0x04, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x23, 0x55, 0x53, 0x00, 0x14, 0x05, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x23, 0x47, 0x55, 0x49, 0x44, 0x00, 0x00, 0x00, 0x24, 0x05, 0x00, 0x00, 0x28, 0x01, 0x00, 0x00, 0x23, 0x42, 0x6C, 0x6F, 0x62, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x01, 0x47, 0x15, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0xFA, 0x25, 0x33, 0x00, 0x16, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x12, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x36, 0x00, 0x2F, 0x00, 0x06, 0x00, 0x5F, 0x00, 0x4D, 0x00, 0x06, 0x00, 0x76, 0x00, 0x4D, 0x00, 0x06, 0x00, 0x93, 0x00, 0x4D, 0x00, 0x06, 0x00, 0xB2, 0x00, 0x4D, 0x00, 0x06, 0x00, 0xCB, 0x00, 0x4D, 0x00, 0x06, 0x00, 0xE4, 0x00, 0x4D, 0x00, 0x06, 0x00, 0xFF, 0x00, 0x4D, 0x00, 0x06, 0x00, 0x1A, 0x01, 0x4D, 0x00, 0x06, 0x00, 0x52, 0x01, 0x33, 0x01, 0x06, 0x00, 0x66, 0x01, 0x33, 0x01, 0x06, 0x00, 0x74, 0x01, 0x4D, 0x00, 0x06, 0x00, 0x8D, 0x01, 0x4D, 0x00, 0x06, 0x00, 0xC4, 0x01, 0xAA, 0x01, 0x06, 0x00, 0xF0, 0x01, 0xDD, 0x01, 0x3F, 0x00, 0x04, 0x02, 0x00, 0x00, 0x06, 0x00, 0x33, 0x02, 0x13, 0x02, 0x06, 0x00, 0x53, 0x02, 0x13, 0x02, 0x06, 0x00, 0x71, 0x02, 0x2F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x10, 0x00, 0x16, 0x00, 0x1E, 0x00, 0x05, 0x00, 0x01, 0x00, 0x01, 0x00, 0x50, 0x20, 0x00, 0x00, 0x00, 0x00, 0x91, 0x00, 0x3D, 0x00, 0x0A, 0x00, 0x01, 0x00, 0x5C, 0x20, 0x00, 0x00, 0x00, 0x00, 0x86, 0x18, 0x42, 0x00, 0x10, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x48, 0x00, 0x11, 0x00, 0x42, 0x00, 0x14, 0x00, 0x19, 0x00, 0x42, 0x00, 0x14, 0x00, 0x21, 0x00, 0x42, 0x00, 0x14, 0x00, 0x29, 0x00, 0x42, 0x00, 0x14, 0x00, 0x31, 0x00, 0x42, 0x00, 0x14, 0x00, 0x39, 0x00, 0x42, 0x00, 0x14, 0x00, 0x41, 0x00, 0x42, 0x00, 0x14, 0x00, 0x49, 0x00, 0x42, 0x00, 0x14, 0x00, 0x51, 0x00, 0x42, 0x00, 0x19, 0x00, 0x59, 0x00, 0x42, 0x00, 0x14, 0x00, 0x61, 0x00, 0x42, 0x00, 0x14, 0x00, 0x69, 0x00, 0x42, 0x00, 0x14, 0x00, 0x71, 0x00, 0x42, 0x00, 0x14, 0x00, 0x79, 0x00, 0x42, 0x00, 0x1E, 0x00, 0x89, 0x00, 0x42, 0x00, 0x24, 0x00, 0x91, 0x00, 0x42, 0x00, 0x10, 0x00, 0x99, 0x00, 0x79, 0x02, 0x29, 0x00, 0x09, 0x00, 0x42, 0x00, 0x10, 0x00, 0x2E, 0x00, 0x0B, 0x00, 0x2E, 0x00, 0x2E, 0x00, 0x13, 0x00, 0x3B, 0x00, 0x2E, 0x00, 0x1B, 0x00, 0x3B, 0x00, 0x2E, 0x00, 0x23, 0x00, 0x3B, 0x00, 0x2E, 0x00, 0x2B, 0x00, 0x2E, 0x00, 0x2E, 0x00, 0x33, 0x00, 0x41, 0x00, 0x2E, 0x00, 0x3B, 0x00, 0x3B, 0x00, 0x2E, 0x00, 0x4B, 0x00, 0x3B, 0x00, 0x2E, 0x00, 0x53, 0x00, 0x59, 0x00, 0x2E, 0x00, 0x63, 0x00, 0x83, 0x00, 0x2E, 0x00, 0x6B, 0x00, 0x90, 0x00, 0x2E, 0x00, 0x73, 0x00, 0xF6, 0x00, 0x2E, 0x00, 0x7B, 0x00, 0xFF, 0x00, 0x2E, 0x00, 0x83, 0x00, 0x08, 0x01, 0x04, 0x80, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1E, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x26, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3C, 0x4D, 0x6F, 0x64, 0x75, 0x6C, 0x65, 0x3E, 0x00, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x2E, 0x65, 0x78, 0x65, 0x00, 0x50, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D, 0x00, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x00, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x6C, 0x69, 0x62, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x00, 0x4F, 0x62, 0x6A, 0x65, 0x63, 0x74, 0x00, 0x4D, 0x61, 0x69, 0x6E, 0x00, 0x2E, 0x63, 0x74, 0x6F, 0x72, 0x00, 0x61, 0x72, 0x67, 0x73, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x52, 0x65, 0x66, 0x6C, 0x65, 0x63, 0x74, 0x69, 0x6F, 0x6E, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x54, 0x69, 0x74, 0x6C, 0x65, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x44, 0x65, 0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x69, 0x6F, 0x6E, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x43, 0x6F, 0x6E, 0x66, 0x69, 0x67, 0x75, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x43, 0x6F, 0x6D, 0x70, 0x61, 0x6E, 0x79, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x50, 0x72, 0x6F, 0x64, 0x75, 0x63, 0x74, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x43, 0x6F, 0x70, 0x79, 0x72, 0x69, 0x67, 0x68, 0x74, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x54, 0x72, 0x61, 0x64, 0x65, 0x6D, 0x61, 0x72, 0x6B, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x52, 0x75, 0x6E, 0x74, 0x69, 0x6D, 0x65, 0x2E, 0x49, 0x6E, 0x74, 0x65, 0x72, 0x6F, 0x70, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x73, 0x00, 0x43, 0x6F, 0x6D, 0x56, 0x69, 0x73, 0x69, 0x62, 0x6C, 0x65, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x47, 0x75, 0x69, 0x64, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x41, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x46, 0x69, 0x6C, 0x65, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x52, 0x75, 0x6E, 0x74, 0x69, 0x6D, 0x65, 0x2E, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x69, 0x6E, 0x67, 0x00, 0x54, 0x61, 0x72, 0x67, 0x65, 0x74, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x44, 0x69, 0x61, 0x67, 0x6E, 0x6F, 0x73, 0x74, 0x69, 0x63, 0x73, 0x00, 0x44, 0x65, 0x62, 0x75, 0x67, 0x67, 0x61, 0x62, 0x6C, 0x65, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x44, 0x65, 0x62, 0x75, 0x67, 0x67, 0x69, 0x6E, 0x67, 0x4D, 0x6F, 0x64, 0x65, 0x73, 0x00, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x2E, 0x52, 0x75, 0x6E, 0x74, 0x69, 0x6D, 0x65, 0x2E, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 0x72, 0x53, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x73, 0x00, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x52, 0x65, 0x6C, 0x61, 0x78, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x73, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x52, 0x75, 0x6E, 0x74, 0x69, 0x6D, 0x65, 0x43, 0x6F, 0x6D, 0x70, 0x61, 0x74, 0x69, 0x62, 0x69, 0x6C, 0x69, 0x74, 0x79, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65, 0x00, 0x43, 0x6F, 0x6E, 0x73, 0x6F, 0x6C, 0x65, 0x00, 0x57, 0x72, 0x69, 0x74, 0x65, 0x4C, 0x69, 0x6E, 0x65, 0x00, 0x00, 0x00, 0x3D, 0x48, 0x00, 0x65, 0x00, 0x6C, 0x00, 0x6C, 0x00, 0x6F, 0x00, 0x2C, 0x00, 0x20, 0x00, 0x77, 0x00, 0x6F, 0x00, 0x72, 0x00, 0x6C, 0x00, 0x64, 0x00, 0x2C, 0x00, 0x20, 0x00, 0x74, 0x00, 0x68, 0x00, 0x69, 0x00, 0x73, 0x00, 0x20, 0x00, 0x69, 0x00, 0x73, 0x00, 0x20, 0x00, 0x66, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x6D, 0x00, 0x20, 0x00, 0x63, 0x00, 0x23, 0x00, 0x2E, 0x00, 0x00, 0x00, 0xA8, 0xEA, 0xE0, 0x3D, 0x2B, 0x29, 0x82, 0x4D, 0xA2, 0xD4, 0xBB, 0x8E, 0xD4, 0x18, 0x1F, 0x0E, 0x00, 0x08, 0xB7, 0x7A, 0x5C, 0x56, 0x19, 0x34, 0xE0, 0x89, 0x05, 0x00, 0x01, 0x01, 0x1D, 0x0E, 0x03, 0x20, 0x00, 0x01, 0x04, 0x20, 0x01, 0x01, 0x0E, 0x04, 0x20, 0x01, 0x01, 0x02, 0x05, 0x20, 0x01, 0x01, 0x11, 0x41, 0x04, 0x20, 0x01, 0x01, 0x08, 0x04, 0x00, 0x01, 0x01, 0x0E, 0x0C, 0x01, 0x00, 0x07, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x00, 0x00, 0x05, 0x01, 0x00, 0x00, 0x00, 0x00, 0x17, 0x01, 0x00, 0x12, 0x43, 0x6F, 0x70, 0x79, 0x72, 0x69, 0x67, 0x68, 0x74, 0x20, 0xC2, 0xA9, 0x20, 0x20, 0x32, 0x30, 0x31, 0x33, 0x00, 0x00, 0x29, 0x01, 0x00, 0x24, 0x61, 0x33, 0x39, 0x63, 0x62, 0x64, 0x35, 0x39, 0x2D, 0x65, 0x62, 0x65, 0x39, 0x2D, 0x34, 0x33, 0x66, 0x31, 0x2D, 0x38, 0x66, 0x38, 0x36, 0x2D, 0x33, 0x30, 0x36, 0x62, 0x36, 0x66, 0x35, 0x34, 0x66, 0x63, 0x31, 0x36, 0x00, 0x00, 0x0C, 0x01, 0x00, 0x07, 0x31, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x00, 0x00, 0x65, 0x01, 0x00, 0x29, 0x2E, 0x4E, 0x45, 0x54, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x2C, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x76, 0x34, 0x2E, 0x30, 0x2C, 0x50, 0x72, 0x6F, 0x66, 0x69, 0x6C, 0x65, 0x3D, 0x43, 0x6C, 0x69, 0x65, 0x6E, 0x74, 0x01, 0x00, 0x54, 0x0E, 0x14, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x44, 0x69, 0x73, 0x70, 0x6C, 0x61, 0x79, 0x4E, 0x61, 0x6D, 0x65, 0x1F, 0x2E, 0x4E, 0x45, 0x54, 0x20, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x77, 0x6F, 0x72, 0x6B, 0x20, 0x34, 0x20, 0x43, 0x6C, 0x69, 0x65, 0x6E, 0x74, 0x20, 0x50, 0x72, 0x6F, 0x66, 0x69, 0x6C, 0x65, 0x08, 0x01, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x01, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1E, 0x01, 0x00, 0x01, 0x00, 0x54, 0x02, 0x16, 0x57, 0x72, 0x61, 0x70, 0x4E, 0x6F, 0x6E, 0x45, 0x78, 0x63, 0x65, 0x70, 0x74, 0x69, 0x6F, 0x6E, 0x54, 0x68, 0x72, 0x6F, 0x77, 0x73, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x66, 0x15, 0x18, 0x51, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0xCC, 0x26, 0x00, 0x00, 0xCC, 0x08, 0x00, 0x00, 0x52, 0x53, 0x44, 0x53, 0xA6, 0x09, 0x45, 0xED, 0x1E, 0x6B, 0x50, 0x43, 0xBE, 0x5D, 0xDC, 0x51, 0x36, 0xA6, 0xE1, 0x20, 0x01, 0x00, 0x00, 0x00, 0x43, 0x3A, 0x5C, 0x55, 0x73, 0x65, 0x72, 0x73, 0x5C, 0x4A, 0x6F, 0x68, 0x6E, 0x5C, 0x64, 0x6F, 0x63, 0x75, 0x6D, 0x65, 0x6E, 0x74, 0x73, 0x5C, 0x76, 0x69, 0x73, 0x75, 0x61, 0x6C, 0x20, 0x73, 0x74, 0x75, 0x64, 0x69, 0x6F, 0x20, 0x32, 0x30, 0x31, 0x30, 0x5C, 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x73, 0x5C, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x5C, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x5C, 0x6F, 0x62, 0x6A, 0x5C, 0x78, 0x38, 0x36, 0x5C, 0x52, 0x65, 0x6C, 0x65, 0x61, 0x73, 0x65, 0x5C, 0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x2E, 0x70, 0x64, 0x62, 0x00, 0x6C, 0x27, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8E, 0x27, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x27, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x5F, 0x43, 0x6F, 0x72, 0x45, 0x78, 0x65, 0x4D, 0x61, 0x69, 0x6E, 0x00, 0x6D, 0x73, 0x63, 0x6F, 0x72, 0x65, 0x65, 0x2E, 0x64, 0x6C, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x25, 0x00, 0x20, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x10, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x80, 0x18, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x50, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x00, 0x00, 0x00, 0xA0, 0x40, 0x00, 0x00, 0xA0, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x43, 0x00, 0x00, 0xEA, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA0, 0x02, 0x34, 0x00, 0x00, 0x00, 0x56, 0x00, 0x53, 0x00, 0x5F, 0x00, 0x56, 0x00, 0x45, 0x00, 0x52, 0x00, 0x53, 0x00, 0x49, 0x00, 0x4F, 0x00, 0x4E, 0x00, 0x5F, 0x00, 0x49, 0x00, 0x4E, 0x00, 0x46, 0x00, 0x4F, 0x00, 0x00, 0x00, 0x00, 0x00, 0xBD, 0x04, 0xEF, 0xFE, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00, 0x01, 0x00, 0x56, 0x00, 0x61, 0x00, 0x72, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x49, 0x00, 0x6E, 0x00, 0x66, 0x00, 0x6F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x00, 0x04, 0x00, 0x00, 0x00, 0x54, 0x00, 0x72, 0x00, 0x61, 0x00, 0x6E, 0x00, 0x73, 0x00, 0x6C, 0x00, 0x61, 0x00, 0x74, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB0, 0x04, 0x00, 0x02, 0x00, 0x00, 0x01, 0x00, 0x53, 0x00, 0x74, 0x00, 0x72, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x67, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x49, 0x00, 0x6E, 0x00, 0x66, 0x00, 0x6F, 0x00, 0x00, 0x00, 0xDC, 0x01, 0x00, 0x00, 0x01, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x30, 0x00, 0x34, 0x00, 0x62, 0x00, 0x30, 0x00, 0x00, 0x00, 0x38, 0x00, 0x08, 0x00, 0x01, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x44, 0x00, 0x65, 0x00, 0x73, 0x00, 0x63, 0x00, 0x72, 0x00, 0x69, 0x00, 0x70, 0x00, 0x74, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x45, 0x00, 0x78, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x70, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x00, 0x00, 0x30, 0x00, 0x08, 0x00, 0x01, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x56, 0x00, 0x65, 0x00, 0x72, 0x00, 0x73, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x31, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x00, 0x00, 0x38, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x49, 0x00, 0x6E, 0x00, 0x74, 0x00, 0x65, 0x00, 0x72, 0x00, 0x6E, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x4E, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x65, 0x00, 0x00, 0x00, 0x45, 0x00, 0x78, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x70, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x2E, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00, 0x00, 0x00, 0x48, 0x00, 0x12, 0x00, 0x01, 0x00, 0x4C, 0x00, 0x65, 0x00, 0x67, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x43, 0x00, 0x6F, 0x00, 0x70, 0x00, 0x79, 0x00, 0x72, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00, 0x74, 0x00, 0x00, 0x00, 0x43, 0x00, 0x6F, 0x00, 0x70, 0x00, 0x79, 0x00, 0x72, 0x00, 0x69, 0x00, 0x67, 0x00, 0x68, 0x00, 0x74, 0x00, 0x20, 0x00, 0xA9, 0x00, 0x20, 0x00, 0x20, 0x00, 0x32, 0x00, 0x30, 0x00, 0x31, 0x00, 0x33, 0x00, 0x00, 0x00, 0x40, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x4F, 0x00, 0x72, 0x00, 0x69, 0x00, 0x67, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x61, 0x00, 0x6C, 0x00, 0x46, 0x00, 0x69, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x6E, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x65, 0x00, 0x00, 0x00, 0x45, 0x00, 0x78, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x70, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x2E, 0x00, 0x65, 0x00, 0x78, 0x00, 0x65, 0x00, 0x00, 0x00, 0x30, 0x00, 0x08, 0x00, 0x01, 0x00, 0x50, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x64, 0x00, 0x75, 0x00, 0x63, 0x00, 0x74, 0x00, 0x4E, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x45, 0x00, 0x78, 0x00, 0x61, 0x00, 0x6D, 0x00, 0x70, 0x00, 0x6C, 0x00, 0x65, 0x00, 0x00, 0x00, 0x34, 0x00, 0x08, 0x00, 0x01, 0x00, 0x50, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x64, 0x00, 0x75, 0x00, 0x63, 0x00, 0x74, 0x00, 0x56, 0x00, 0x65, 0x00, 0x72, 0x00, 0x73, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00, 0x31, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x00, 0x00, 0x38, 0x00, 0x08, 0x00, 0x01, 0x00, 0x41, 0x00, 0x73, 0x00, 0x73, 0x00, 0x65, 0x00, 0x6D, 0x00, 0x62, 0x00, 0x6C, 0x00, 0x79, 0x00, 0x20, 0x00, 0x56, 0x00, 0x65, 0x00, 0x72, 0x00, 0x73, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00, 0x31, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x2E, 0x00, 0x30, 0x00, 0x00, 0x00, 0xEF, 0xBB, 0xBF, 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x69, 0x6E, 0x67, 0x3D, 0x22, 0x55, 0x54, 0x46, 0x2D, 0x38, 0x22, 0x20, 0x73, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x6C, 0x6F, 0x6E, 0x65, 0x3D, 0x22, 0x79, 0x65, 0x73, 0x22, 0x3F, 0x3E, 0x0D, 0x0A, 0x3C, 0x61, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3D, 0x22, 0x75, 0x72, 0x6E, 0x3A, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2D, 0x6D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2D, 0x63, 0x6F, 0x6D, 0x3A, 0x61, 0x73, 0x6D, 0x2E, 0x76, 0x31, 0x22, 0x20, 0x6D, 0x61, 0x6E, 0x69, 0x66, 0x65, 0x73, 0x74, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x3C, 0x61, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x49, 0x64, 0x65, 0x6E, 0x74, 0x69, 0x74, 0x79, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x22, 0x20, 0x6E, 0x61, 0x6D, 0x65, 0x3D, 0x22, 0x4D, 0x79, 0x41, 0x70, 0x70, 0x6C, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x2E, 0x61, 0x70, 0x70, 0x22, 0x2F, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x3C, 0x74, 0x72, 0x75, 0x73, 0x74, 0x49, 0x6E, 0x66, 0x6F, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3D, 0x22, 0x75, 0x72, 0x6E, 0x3A, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2D, 0x6D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2D, 0x63, 0x6F, 0x6D, 0x3A, 0x61, 0x73, 0x6D, 0x2E, 0x76, 0x32, 0x22, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x3C, 0x73, 0x65, 0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3C, 0x72, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x65, 0x64, 0x50, 0x72, 0x69, 0x76, 0x69, 0x6C, 0x65, 0x67, 0x65, 0x73, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3D, 0x22, 0x75, 0x72, 0x6E, 0x3A, 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x2D, 0x6D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x2D, 0x63, 0x6F, 0x6D, 0x3A, 0x61, 0x73, 0x6D, 0x2E, 0x76, 0x33, 0x22, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3C, 0x72, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x65, 0x64, 0x45, 0x78, 0x65, 0x63, 0x75, 0x74, 0x69, 0x6F, 0x6E, 0x4C, 0x65, 0x76, 0x65, 0x6C, 0x20, 0x6C, 0x65, 0x76, 0x65, 0x6C, 0x3D, 0x22, 0x61, 0x73, 0x49, 0x6E, 0x76, 0x6F, 0x6B, 0x65, 0x72, 0x22, 0x20, 0x75, 0x69, 0x41, 0x63, 0x63, 0x65, 0x73, 0x73, 0x3D, 0x22, 0x66, 0x61, 0x6C, 0x73, 0x65, 0x22, 0x2F, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3C, 0x2F, 0x72, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x65, 0x64, 0x50, 0x72, 0x69, 0x76, 0x69, 0x6C, 0x65, 0x67, 0x65, 0x73, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x3C, 0x2F, 0x73, 0x65, 0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x3E, 0x0D, 0x0A, 0x20, 0x20, 0x3C, 0x2F, 0x74, 0x72, 0x75, 0x73, 0x74, 0x49, 0x6E, 0x66, 0x6F, 0x3E, 0x0D, 0x0A, 0x3C, 0x2F, 0x61, 0x73, 0x73, 0x65, 0x6D, 0x62, 0x6C, 0x79, 0x3E, 0x0D, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0xA0, 0x37, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };


        string hourFormat = "12";
        
        uint[] frai_icon_png = new uint[] 
{
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
};

        uint[] txt = new uint[]
{
0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,0x000000,
0x000000,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0x000000,
0x000000,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0xFFFFFF,0x000000,
0x000000,0xFFFFFF,0xFFFFFF,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xC0C0C0,0xFFFFFF,0x000000};

        public static VGAScreen scr = new VGAScreen();
        public static Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII svga = new Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII();

        public void drawRect(uint x, uint y, uint length, uint height, uint c)
        {
            for (uint l = 1; l <= length; l++)
            {
                for (uint h = 1; h <= height; h++)
                {
                    scr.SetPixel320x200x8((uint)x + l, (uint)y + h, (uint)c);
                }
            }
        }

        public static void DrawFrameColor(uint[] Arr, int width, int length, int xpixel, int ypixel, int color)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 1)
                    {
                        scr.SetPixel((uint)(0 + (uint)t + 0 + (uint)xpixel), (uint)(0 + (uint)i + 0 + (uint)ypixel), (uint)color);
                    }
                }
            }
        }
       

        public string selectedMn = "Launch";

        public void Shutdown()
        {
          ACPI.Shutdown();
        }

        public void RestartKernel()
        {
            this.Restart();
        }

       

        public static void CreateFolders()
        {
            //if (!FS.DirExists("/", "sys"))
            fd.makeDir("/etc", "");
            fd.makeDir("/etc/conf", "");
            fd.makeDir("/usr", "");

            fd.makeDir("/apps", "");
            //{
                fd.makeDir("/sys", "");
            //}
            //if (!FS.DirExists("/sys", "bin"))
            //{
                fd.makeDir("/sys/bin", "");
            //}
            //if (!FS.DirExists("/", "home"))
            //{
                fd.makeDir("/home", "");
            //}
            //if (!FS.DirExists("/home", "root"))
            //{
                fd.makeDir("/home/root", "");
            //}
                //fd.saveFile(Example_exe, "/home/root/example.exe", "root");
                //fd.makeDir("/home/root/test1", "root");
        }

        Cosmos.Hardware.TextScreen txscr;
        protected override void BeforeRun()
        {
            Console.Clear();
            Bootscreen.Show(4);
            /*#region Check if already installed
            if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    BlockDevice Device = BlockDevice.Devices[i];
                    if (Device is Partition)
                    {


                        GLNFS fd = new GLNFS((Partition)Device);
                        Console.Write(Device.BlockSize);
                        Console.Write(":");
                        Console.WriteLine(Device.BlockCount);
                        if (GLNFS.isGFS((Partition)Device))
                        {

                            Console.WriteLine("GLNFS FOUND!");
                            Console.WriteLine("Checking for Plasmid files");


                            bool plas_found = false;
                            string[] dirs = fd.ListDirectories("/");
                            for (int m = 0; m < dirs.Length; m++)
                            {
                                Console.WriteLine(dirs[m]);
                                if (dirs[m] == "plasmid")
                                {
                                    GruntyOS.CurrentUser.Username = "SYSTEM";

                                    GruntyOS.CurrentUser.Privilages = 0;

                                    plas_found = true;
                                    //CosmosKernel4.Global.Config.compname = CosmosKernel4.Global.StringManip.GetString(fd.readFile("/plasmid/compname"));
                                    GruntyOS.HAL.FileSystem.Root = fd;
                                    Console.WriteLine("PLASMID FILESYSTEM FOUND; SETTING AS ROOT");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("BOOTSTRAP SEQUENCE DONE");
                                    GruntyOS.HAL.FileSystem.Root = fd;
                                    //CosmosKernel4.UI.Login.Login
                                    //lgin = new CosmosKernel4.UI.Login.Login();
                                    //lgin.do_login();
                                    //goto infiniloop;
                                }
                            }
                            if (plas_found == false)
                            {
                                Console.WriteLine("Plasmid files not found. Press any key to enter install");
                                Console.ReadKey();
                                Installation.Setup.Do();
                                
                            }

                        }
                        else
                        {
                            Console.WriteLine("Filesystem not present, starting setup...");
                            Time.Wait(1);
                            Installation.Setup.Do();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Err... I see that you don't have hard drives.");
            }
            #endregion*/
            #region Start screen
            /*Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();*/
            // Ligne 1
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@@@@@ @  @@  @       @  ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@@    @@   @@");

            // Ligne 2
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@     @ @  @ @       @  ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@ @  @  @ @  @");

            // Ligne 3
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@@@@  @ @  @ @   @   @  ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@  @ @  @ @  ");

            // Ligne 4
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@     @ @  @  @ @ @ @   ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@  @ @  @  @@");

            // Ligne 5
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@     @ @  @   @   @    ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@ @  @  @ @  @");

            // Ligne 6
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("@     @  @@    @   @    ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.WriteLine("@@    @@   @@");

            //Cosmos.Hardware.Global.TextScreen.Clear();
            
            #endregion
            /*InitFS();
            //Make a filesystem attached to the partition
            FATFS = new FatFileSystem(OSPartition);
            //Map the filesystem to the C drive
            Cosmos.System.Filesystem.FileSystem.AddMapping("C", FATFS);
            FATFileList = FATFS.GetRoot();*/
            
            #region Version
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkMagenta, ConsoleColor.Black);
            Console.WriteLine("FlowDOS 0.2 Booting...");
            Time.Wait((int)2);
            #endregion


            #region Memory
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine("Checking Memory...");
            Time.Wait((int)1);
            uint mem = CPU.GetAmountOfRAM();
        
                Console.Write("Memory: " + (mem + 2) + " MB ");
               
           
            
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("OK");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            #endregion

            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("S");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Green, ConsoleColor.Black);
            Console.Write("t");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write("a");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
            Console.Write("r");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write("t");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.Write("i");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkMagenta, ConsoleColor.Black);
            Console.Write("n");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Magenta, ConsoleColor.Black);
            Console.Write("g");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkGray, ConsoleColor.Black);
            Console.Write(" ");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
            Console.Write("k");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkRed, ConsoleColor.Black);
            Console.Write("e");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkYellow, ConsoleColor.Black);
            Console.Write("r");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Gray, ConsoleColor.Black);
            Console.Write("n");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write("e");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
            Console.Write("l");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.Write(".");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.Write(".");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine(".");
            Time.Wait(1);

           /* #region IDE Devices
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.WriteLine("Checking IDE devices...");
            Console.Write("There is " + Exterrium.FileSystem.Utilities.IDE.Devices.Count + " devices. ");
            if(Exterrium.FileSystem.Utilities.IDE.Devices.Count == 0)
            {
                Console.WriteLine("CRITICAL: NO HARD DRIVE FOUND!");
                this.Shutdown();
                Stop();
                while (true) ;
            }
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("OK");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            #endregion*/


            #region Hard drive
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            Console.Write("Creating partitions...");
            //disk.driveletter = "C";
            //ArkadiaOsKernel.Core.Filesystem.Vfs.HardDrive.Drive.AddDisk(disk);
            /*if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    BlockDevice Device = BlockDevice.Devices[i];
                    if (Device is Partition)
                    {
                        fd = new GLNFS((Partition)Device);
                        //Console.WriteLine("ATA Device Found! , /dev/sda" + i.ToString());
                        if (GLNFS.isGFS((Partition)Device))
                        {
                            GruntyOS.HAL.FileSystem.Root = fd;
                        }
                        else
                        {
                            //Console.WriteLine("Filesystem not present, formatting...");
                            fd.Format("flow");
                            /*fd.makeDir("/sys", "root");
                            fd.makeDir("/sys/bin/", "root");
                            fd.makeDir("/home", "");
                            fd.makeDir("/home/root", "root");
                            CreateFolders();
                            //Console.WriteLine("Please restart your computer!");
                        }
                    }
                }
            }
            */
            //Environment.CurrentDir = "/home/root/";
            /*
            string code123 = "## mon programme\n" +
                             "push 123\n" +
                             "push$ est mon programme\n" +
                             "push$ ceci\n" +
                             "prt" +
                             "show et ceci une boite#13#de dialogue avec#13#des caractères comme #56#";
            FS.AddFile(code123, "test.fsc", "/home/root/");*/
            //CreateFolders();
        /*bool ATAfound = false;
        for (int i = 0; i < BlockDevice.Devices.Count; i++)
        {
            BlockDevice device = BlockDevice.Devices[i];
            if (device is Partition)
            {
                fd = new GLNFS((Partition)device);
                
                if (GLNFS.isGFS((Partition)device)/* && fd.Label == "filesystem")
                {
                    
                    GruntyOS.HAL.FileSystem.Root = fd;
                }

                ATAfound = true;
            }
            else
            {
                            
            }
        }
        if(!ATAfound)
        {
             fd.Format("filesystem");
        }
            //Storage.IDE[] IDEs = Storage.IDE.Devices.ToArray();*/

            /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
            /*EnvyOS.Storage.IDE[] IDEs = EnvyOS.Storage.IDE.Devices.ToArray();
            if (BlockDevice.Devices.Count > 0)
            {
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    BlockDevice Device = BlockDevice.Devices[i];
                    if (Device is Partition)
                    {
                        /*GLNFS fd = new GLNFS((Partition)Device);
                        //Console.WriteLine("ATA Device Found! , /dev/sda" + i.ToString());
                        if (GLNFS.isGFS((Partition)Device))
                        {
                            //GruntyOS.HAL.FileSystem.Root = (GruntyOS.HAL.FileSystem)fd;
                            GruntyOS.HAL.FileSystem.Root.Mount("\\", fd);
                        }
                        else
                        {
                            //SwitchToConsole();
                            Console.WriteLine("Filesystem not present, formatting...");
                            fd.Format("FLOWDOS");
                            CreateFolders();
                            Console.WriteLine("Press any key to restart your computer...");
                            Console.ReadKey();
                            RebootACPI();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Hard drive not found! Please check the cables into your dark and deep PC.");
            }*/
            GruntyOS.HAL.ATA.Detect(); // This will detect all ATA devices and add them to the device filesystem
            GruntyOS.CurrentUser.Privilages = 0; // This has to be set, 1 = limited 0 = root
            GruntyOS.CurrentUser.Username = "root"; // When using anything in the class File this will be the default username

            GruntyOS.HAL.FileSystem.Root = new GruntyOS.HAL.RootFilesystem(); // initialize virtual filesystem
            for (int i = 0; i < GruntyOS.HAL.Devices.dev.Count; i++)
            {
                if (GruntyOS.HAL.Devices.dev[i].dev is Cosmos.Hardware.BlockDevice.Partition)
                {
                    fd = new GruntyOS.HAL.GLNFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev);


                    if (GruntyOS.HAL.GLNFS.isGFS((Cosmos.Hardware.BlockDevice.Partition)GruntyOS.HAL.Devices.dev[i].dev))
                    {
                        Console.WriteLine("Drive detected!");
                    }
                    else
                    {
                        fd.Format("FLOWDOS");
                    }
                    GruntyOS.HAL.FileSystem.Root.Mount("/", fd); // mount it as root (you can only have on partition mounted as root!!!!
                }
            }


          
        
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("OK");
            Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);


            #endregion


            #region Easter eggs
            #region Happy new year
            if ((Time.Month == 1) && (Time.Day == 1))
            {
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
                Console.Write("H");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Green, ConsoleColor.Black);
                Console.Write("a");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Blue, ConsoleColor.Black);
                Console.Write("p");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Cyan, ConsoleColor.Black);
                Console.Write("p");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Yellow, ConsoleColor.Black);
                Console.Write("y");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkCyan, ConsoleColor.Black);
                Console.Write(" ");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkMagenta, ConsoleColor.Black);
                Console.Write("n");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Magenta, ConsoleColor.Black);
                Console.Write("e");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkGray, ConsoleColor.Black);
                Console.Write("w");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
                Console.Write(" ");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkRed, ConsoleColor.Black);
                Console.Write("y");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.DarkYellow, ConsoleColor.Black);
                Console.Write("e");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Gray, ConsoleColor.Black);
                Console.Write("a");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Blue, ConsoleColor.Black);
                Console.Write("r");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.Red, ConsoleColor.Black);
                Console.Write("!");
                Cosmos.Hardware.Global.TextScreen.SetColors(ConsoleColor.White, ConsoleColor.Black);
            }
            #endregion

            #endregion
        }

        byte[] bytenonchoix = new byte[1];
        

        public Cosmos.System.Filesystem.Listing.File fl;
        int x123;
        int y123;
        protected override void Run()
        {
            //PCSpeaker p = new PCSpeaker();
            //p.beep();
            //Time.Wait(1);
            //p.nosound();
            // removed beep because it's annoying

/*
            string usr, ps;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to FlowDOS!");
            Console.WriteLine("First, create an account.");
            Console.WriteLine("Type an username and press ENTER.");
            nm:
Console.BackgroundColor = ConsoleColor.Black;
            if (Console.ReadLine() == "")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Username cannot be empty!");
                Console.WriteLine("Type an username and press ENTER.");
                goto nm;
            }
            else
            {
                usr = Console.ReadLine();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Good! Now type a password and press ENTER.");
            pass:
                Console.BackgroundColor = ConsoleColor.Black;
                if (Console.ReadLine() == "")
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("Password cannot be empty!");
                    Console.WriteLine("Type a password and press ENTER.");
                    goto pass;
                }
                else
                {
                    ps = Console.ReadLine();
                    for (int i = 0; i < Environment.Accounts.Count - 1; i++)
                    {
                        if (Environment.Accounts[i].Type == UserType.Root)
                        {
                            Environment.CreateUser(usr, ps);
                        }
                        else
                        {
                            Environment.CreateUser(usr, ps, UserType.Root);
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("Good! Now you can start using FlowDOS.");
                    Console.WriteLine("Type 'help' for a list of commands.");
                    Console.BackgroundColor = ConsoleColor.Black;
                    nonchoix = true;
                    connected = true;
                }
            }*/

            

            //FlowDOS.Hardware.Mouse.Initialize();
            //x123 = FlowDOS.Hardware.Mouse.X;
            //y123 = FlowDOS.Hardware.Mouse.Y;
            /*if (nonchoix)
            {
                nonchoix = false;
                
            }
            else
            {
                goto choix;
            }*/
            
            /*if (fd.readFile("nonchoix") == bytenonchoix)
            {
                fd.Delete("nonchoix");
            }
            else
            {
                goto choix;
            }*/
            unsafe
            {
            dewitcher.Core.Memory.MemAlloc((uint)sizeof(int));
            }
        alaffutdescommandes:
            //while (true)
            //{
            //    Console.WriteLine("X: " + x123 + "; Y: " + y123);
            //}
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("root ");
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.Write("~/home/root/ ");
            Console.Write("~");
            Console.Write(Environment.CurrentDir);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("# ");
            Console.ForegroundColor = ConsoleColor.White;
            var cinput = Console.ReadLine();
            var coutput = cinput.SplitAtFirstSpace();
            var otherargs = cinput.Split(' ');
            /*var cmd = coutput[0];
            for (int i = 0; i < cinput.; i++)
            {
                
            }*/
            runFunc(coutput[0], coutput[1], otherargs);
            goto alaffutdescommandes;



            /*choix:
            Console.WriteLine("Do you want to boot on DOS or on GUI? DOS/GUI");
            var input = Console.ReadLine();
            switch (input)
            {
                case "DOS":
                    goto alaffutdescommandes;
                    break;
                case "GUI":
                    initGui();
                    break;
                default:
                    break;
            }*/
        }
        /// <summary>
        /// <summary>
        /// </summary>
        void drawText()
        {
           
        }

        void drawTime(int x, int y, int hour, int min, int color)
        {
            string hr = Convert.ToString(hour);
            string mn = Convert.ToString(min);

            if (hr[0] == '0')
            {
                for (int i = y; i < y + 6; i++)
                {
                    
                }
            }
        }

        protected void RootRun()
        {
    
        }

      

        bool isThereAMenu = false;

        public static void DrawFrame(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 0xFF00FF)
                    {
                    }
                    else
                    {
                        scr.SetPixel((uint)(0 + (uint)t + 0 + (uint)xpixel), (uint)(0 + (uint)i + 0 + (uint)ypixel), (uint)Arr[count]);
                    }
                }
            }
        }

        public static void DrawFrame(uint[] Arr, int width, int length, uint xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 0xFF00FF)
                    {
                    }
                    else
                    {
                        svga.SetPixel((ushort)(0 + (uint)t + 0 + (uint)xpixel), (ushort)(0 + (uint)i + 0 + (uint)ypixel), (uint)Arr[count]);
                    }
                }
            }
        }

        public static void DrawFrame(byte[] Arr, int width, int length, int xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    /*if (Arr[count] == unchecked((byte)0xFF00FF))
                    {
                        
                    }
                    else
                    {*/
                        svga.SetPixel((ushort)(0 + (uint)t + 0 + (uint)xpixel), (ushort)(0 + (uint)i + 0 + (uint)ypixel), (uint)Arr[count]);
                    //}
                }
            }
        }

        public static void DrawFrameBlackAndWhite(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                for (int t = 0; t < width; t++, count++)
                {
                    if (Arr[count] == 1)
                    {
                        scr.SetPixel((uint)(0 + (uint)t + 0 + (uint)xpixel), (uint)(0 + (uint)i + 0 + (uint)ypixel), 0x000000);
                    }
                    if (Arr[count] == 2)
                    {
                        scr.SetPixel((uint)(0 + (uint)t + 0 + (uint)xpixel), (uint)(0 + (uint)i + 0 + (uint)ypixel), 0xffffff);
                    }
                }
            }
        }



        void clr1()
        {
            
            //Console.WriteLine("Press enter!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Time.Wait(400);
Console.Clear();
        }

    public static byte[] ConvertStringToBytes(string input)
  {
      System.IO.MemoryStream stream = new System.IO.MemoryStream();

      using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
    {
      writer.Write(input);
      writer.Flush();
    }
   
    return stream.ToArray();
  }

    void runFunc(string cmd, string args, string[] args1)
    {
    hlp:
        if (cmd.ToLower() == "help" || cmd.ToLower() == "help ")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                //hlp:
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine("FlowDOS Help : Page 1/1");
                Console.WriteLine("-----------------------");
               
                Console.WriteLine("help: show this help");
                Console.WriteLine("fsc <file path>: run the FlowScript interpreter");
                //Console.WriteLine("help <command>: shows help of a command");
                //Console.WriteLine("app <command>: run an app");
                Console.WriteLine("showgui: show graphical interface");
                //Console.WriteLine("beep: throw a beep");
                Console.WriteLine("reboot: restart the computer");
                Console.WriteLine("shutdown: shut down the computer");
                //Console.WriteLine("break, brk: do a CPU halt");
                Console.WriteLine("ls: list root directory");
                //Console.WriteLine("fdisk: clear the hard drive and reinstall system");
                //Console.WriteLine("textedit: shows text editor");
                Console.WriteLine("lolz: just types lolz for infinite if you get bored");
                Console.WriteLine("lolz: WARNING IT WILL PROBABLY BURN OUT YOUR CPU!");
                Console.WriteLine("debughlp: show the development commands");
                Console.WriteLine("cls: clear screen");
                Console.WriteLine("info: show system informations");
                Console.WriteLine("mkdir <folder name>: make a folder with the specified name and/or path");
                Console.WriteLine("rmdir <folder name>: remove a folder with the specified path");
                //Console.WriteLine("cbc <color name>: change background color(see 'clist')");
                //Console.WriteLine("cfc <color name>: change foreground color(see 'clist')");
                //Console.WriteLine("clist: list of colors(see 'cbc' & 'cfc')");
                Console.WriteLine("flwrite <file path(optional)>: launch the Flow text editor");
                Console.WriteLine("textedit: launch text editor [OBSELETE]");
                Console.WriteLine("textedit: type #end# to exit");
                Console.WriteLine("textedit: type #save# to save and exit");
                Console.WriteLine("stopwatch: run stopwatch. press ESCAPE to exit.");
                Console.WriteLine("CREDITS: SHOW CREDITS AND COPYRIGHTS");
                //fd.makeDir("test", "");
                Console.BackgroundColor = ConsoleColor.Black;
                //client.Send(Encoding.Unicode.GetBytes("Hello world!"));
                // client.Close();
            }

            else
            {
                switch (args)
                {
                    case "showgui":
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Help of command : showgui");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("Switches the system to graphical mode.");
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Parameters : Nothing");
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;

                    default:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("CAN'T PARSE ARGUMENT : " + args);
                        break;
                }
            }
        }
        /*else if ((cmd.ToLower().Substring(0, 5) == "help "))
        {
            switch (cmd.ToLower().Replace("help ", ""))
            {
                case "showgui":
                    Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Help of command : showgui");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Switches the system to graphical mode.");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Parameters : Nothing");
            Console.BackgroundColor = ConsoleColor.Black;            
            break;
                default:
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ARGUMENT NEEDED: COMMAND");
            Console.BackgroundColor = ConsoleColor.Black;
            break;
            }
        }*/


        /*else if ((cmd.ToLower() == "break") || (cmd.ToLower() == "brk"))
        {
            Cosmos.Kernel.CPU.Halt();
        }
        else if (cmd.ToLower() == "reboot")
        {
            Cosmos.Kernel.CPU.Reboot();
        }
        else if (cmd.ToLower() == "cbc")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ARGUMENT NEEDED: COLOR");
            Console.BackgroundColor = ConsoleColor.Black;
        }*/
        else if (cmd.ToLower() == "random")
        {
            Random r = new Random();
        rg:
            Console.WriteLine("" + r.Next(0, 100));
            Time.Wait(1);
            goto rg;
        }
        else if (cmd.ToLower() == "cmdmouse")
        {
            Console.Clear();
            /*while (true)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(' ');
                Time.WaitMS(500);
            }*/
            FlowDOS.Hardware.Mouse.Initialize();
            int oldx = FlowDOS.Hardware.Mouse.X;
            int oldy = FlowDOS.Hardware.Mouse.Y;
        ing:
            while ((FlowDOS.Hardware.Mouse.X == oldx) && (FlowDOS.Hardware.Mouse.Y == oldy))
            {
            }
            /*Console.CursorTop = oldy;
            Console.CursorLeft = oldx;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(' ');*/
            Console.Clear();
            Console.CursorTop = FlowDOS.Hardware.Mouse.Y;
            Console.CursorLeft = FlowDOS.Hardware.Mouse.X;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(' ');
            oldx = FlowDOS.Hardware.Mouse.X;
            oldy = FlowDOS.Hardware.Mouse.Y;
            Time.WaitMS(100);
            goto ing;
        }
        else if (cmd.ToLower() == "snake")
        {
            Snake.Start();
        }
        else if (cmd.ToLower() == "exe")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: exe <executable path>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                GDOS.Executable.MZ_EXE g = new GDOS.Executable.MZ_EXE(args);
            }
        }
        else if (cmd.ToLower() == "testprogbar")
        {
            Console2.ProgressBar pg = new Console2.ProgressBar(0, true);
            pg.Draw();
            int count = 0;
            while (pg.Value < 100)
            {
                if (pg.Value % 10 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(200);
                }
                else if (pg.Value % 5 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(600);
                }
                else if (pg.Value % 4 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(100);
                }
                else if (pg.Value % 6 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(800);
                }
                else if (pg.Value % 3 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(750);
                }
                else if (pg.Value % 2 == 0)
                {
                    pg.Increment();
                    Time.WaitMS(400);
                }
                else
                {
                    pg.Increment();
                    Time.WaitMS(500);
                }
            }
        }
        else if (cmd.ToLower() == "pci")
        {
            for (int i = 0; i < Optimus.PCI.PCIDeviceList.Length; i++)
            {
                Optimus.PCI.PCIDevice p = Optimus.PCI.PCIDeviceList[i];
                Console.WriteLine("Name: " + p.ClassName);
                Console.WriteLine("Sub-class name: " + p.SubClassName);
                Console.WriteLine("Device " + p.DeviceID + " is on bus " + (int)p.bus);
                //Console.WriteLine("Sub-class name: " + p.SubClassName);

            }
        }
        else if (cmd.ToLower() == "exexe")
        {
            fd.saveFile(Example_exe, "/home/root/example.exe", "root");
            fd.makeDir("/home/root/test1", "root");

            //g.Execute();
        }
        else if (cmd.ToLower() == "exexe2")
        {
            GDOS.Executable.MZ_EXE g = new GDOS.Executable.MZ_EXE("/home/root/example.exe");
            g.Execute();
        }
        else if (cmd.ToLower() == "flowfs")
        {
        

        }
        else if (cmd.ToLower() == "matrix")
        {
            int sec1 = RTC.Second;
            int sec2 = sec1;
            do { sec2 = RTC.Second; } while (sec1 == sec2);
            int sec3;
            if (sec2 <= 56) sec3 = sec2 + 3;
            else if (sec2 == 57) sec3 = 1;
            else if (sec2 == 58) sec3 = 2;
            else if (sec2 == 59) sec3 = 3;
            else sec3 = 3;
            int tmr = 0;
            int tmrx = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int ih = 0; ih < Console.WindowHeight; ih++)
                {
                    for (int iw = 0; iw < Console.WindowWidth; iw++)
                    {
                        if (tmr == 11) tmr = 0;
                        if (tmrx == 4) tmrx = 0;
                        tmr++;
                        if (tmr == 0) Extensions.Write2("#", ConsoleColor.Magenta);
                        if (tmrx == 3) Extensions.Write2("*", ConsoleColor.Green);
                        if (tmr == 2) { Extensions.Write2(";", ConsoleColor.Red); ++tmrx; }
                        if (tmrx == 1) Extensions.Write2("+", ConsoleColor.Yellow);
                        if (tmr == 4) { Extensions.Write2("~", ConsoleColor.Blue); ++tmrx; }
                        if (tmrx == 2) Extensions.Write2("&", ConsoleColor.Cyan);
                        Extensions.Write2("FlowDOS", ConsoleColor.White, true, true);
                    }
                }
            }
        }
        else if (cmd.ToLower() == "credits")
        {
            Console.Clear();
            Console.WriteLine("Press ESC to exit");
            ConsoleKeyInfo a;
        cr:
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
            }
            Console.WriteLine("");
            goto cr;
        }
        else if (cmd.ToLower() == "stopwatch")
        {
            bool g = true;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            while (g)
            {
                Keyboard kb = new Keyboard();
                ConsoleKey a = ConsoleKey.A;
                kb.GetKey(out a);
                while (a != ConsoleKey.Escape)
                {
                    Console.WriteLine(hours + ":" + minutes + ":" + seconds);
                    if (seconds == 59)
                    {
                        seconds = 0;
                        minutes++;
                    }
                    else
                    {
                        if (minutes == 59)
                        {
                            minutes = 0;
                            hours++;
                        }
                        else
                        {
                        }
                        seconds++;
                    }
                    Time.Wait(1);
                }
            }

        }
        else if (cmd.ToLower() == "snake")
        {
            Console.Clear();
            Programs.Games.Snake.Start();
        }
        else if (cmd.ToLower() == "fillcon")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 1; i < 25; i++)
            {
                for (int j = 1; j < 80; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(" ");

            }

        }
        else if (cmd.ToLower() == "peargui")
        {
            Console.WriteLine(FS.ReadFile("/home/root/test.fsc"));
        }
        else if (cmd.ToLower() == "svga")
        {
            //Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII svga = new Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII();
            //svga.setres(1300, 780);
            //SVGA.setres(800, 600);
            //SVGA.clear(Colors.DesktopBlueBackground);
            Graphics.GUI800x600.Initialize();
            //DrawFrame(frai_icon_png, 16, 16, 10, 10);
            //DrawFrame(txt, 16, 4, (uint)10, 10);
        }
        else if (cmd.ToLower() == "fsc")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: fsc <file path>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Programs.FlowScript.Run(FS.ReadFile(args));
            }
        }
        else if (cmd.ToLower() == "cd")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: cd <directory name>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {

                Environment.CurrentDir = args;
                currentDir = args;
            }
        }
        else if (cmd.ToLower() == "flwrite")
        {
            Console.Clear();

            if (string.IsNullOrWhiteSpace(args))
            {
                Cosmos.Hardware.Keyboard k = new Keyboard();
                k.Initialize(new HandleKeyboardDelegate(KeyPress));
                string typed = "";
                char cdefg;
                char c;
                Console.Clear();
                while (true)
                {
                    if (k.GetChar(out cdefg))
                    {
                        k.GetChar(out c);
                        //ProcessKeyboard(c);
                        typed = typed + c;
                    }
                    else
                    {
                        ConsoleKey ke;
                        k.GetKey(out ke);
                        switch (ke)
                        {
                            case ConsoleKey.Backspace:
                                //Console.
                                break;
                            default:
                                break;
                        }
                    }
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("-F1:OPEN--F2:SAVE-------------------------------------------------------F4:EXIT-");
                    Console.WriteLine("------------------------------------FLWRITE-------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(typed);
                }
            }
        }
        else if (cmd.ToLower() == "bas")
        {
            //FlowDOS.Programs.FlowScript.Run("VGA\nPIX 10 10 1");
        }
        else if (cmd.ToLower() == "keybrd")
        {
            Cosmos.Hardware.Keyboard k = new Cosmos.Hardware.Keyboard();
            //k.Initialize(InitKeybrd());
            ConsoleKey i;
            k.GetKey(out i);
        loop:
            while (i == null)
            {
                k.GetKey(out i);
            }
            Console.WriteLine(getKeyFromKey(i));
            goto loop;
        }
        else if (cmd.ToLower() == "textedit")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                #region normal
                string typed = "";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Press ENTER to start.");
                //Console.WriteLine("Press ESCAPE to cancel and return to console.");
                //Console.WriteLine("Type #end# and press ENTER to exit.");
                Console.ForegroundColor = ConsoleColor.White;
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                logo:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("---------------F1:OPEN--F2:SAVE----------------------------------------F4:EXIT-");
                    Console.WriteLine("-| A B C |--------------------------FLOWTEXT-----------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.BackgroundColor = ConsoleColor.Black;
                beginfreetype:
                    string txt = Console.ReadLine();
                    ConsoleKeyInfo k = Console.ReadKey();
                    switch (k.Key)
                    {
                        case ConsoleKey.F4:
                            if (string.IsNullOrWhiteSpace(typed))
                            {
                                nonchoix = true;
                                Console.Clear();
                                BeforeRun();
                                Run();
                                nonchoix = false;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Magenta;
                            question1:
                                Console.WriteLine("Do you want to save your file ? Y/N");
                                var answer = Console.ReadLine();
                                if (answer == "Y")
                                {
                                qu2:
                                    Console.WriteLine("Please type the name :");
                                    var answer2 = Console.ReadLine();
                                    if (answer2 == null)
                                        goto qu2;
                                    else
                                        FS.AddFile(typed, answer2 + ".txt", "/documents");
                                }
                                else if (answer == "N")
                                {
                                    nonchoix = true;
                                    Console.Clear();
                                    BeforeRun();
                                    Run();
                                    nonchoix = false;
                                }
                                else
                                {
                                    goto question1;
                                }
                            }
                            break;
                        case ConsoleKey.F1:
                            if (string.IsNullOrWhiteSpace(typed))
                            {
                                Console.Clear();
                                goto logo;
                                Console.BackgroundColor = ConsoleColor.Magenta;
                            question1:
                                Console.WriteLine("Path : ");

                                var answer = Console.ReadLine();
                                if (fd.readFile(answer) == null)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.WriteLine("FILE DOESN'T EXIST!!!");
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    goto question1;

                                }
                                else
                                {
                                    Console.Clear();
                                    goto logo;
                                    Console.WriteLine(FS.ReadFile(answer));
                                }
                            }
                            break;
                        case ConsoleKey.F2:
                        qu3:
                            Console.WriteLine("Please type the name :");
                            var answer3 = Console.ReadLine();
                            if (answer3 == null)
                                goto qu3;
                            else
                                FS.AddFile(typed, answer3 + ".txt", "/documents");
                            break;
                        default:
                            typed = typed + getKeyFromKey(k.Key);
                            break;
                    }
                    goto beginfreetype;
                    /*if (txt == "#end#")
                    {
                        nonchoix = true;
                        Console.Clear();
                        BeforeRun();
                        Run();
                        nonchoix = false;
                    }
                    else if (txt == "#save#")
                    {
                    n:
                        Console.WriteLine("Type name and press ENTER");
                        string nm = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nm))
                        {
                            FS.AddFile(typed, nm, "/documents/");
                            //dont work File.Save(nm, typed);
                            
                            nonchoix = true;
                            Console.Clear();
                            BeforeRun();
                            Run();
                            nonchoix = false;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid file name!");
                            goto n;
                        }
                    }
                    else if (txt == "#basic#")
                    {
                        FlowDOS.Compilers.Basic.BCompiler c = new Compilers.Basic.BCompiler();
                        Console.Clear();
                        Console.WriteLine("Your ASM code is :");
                        Console.WriteLine(c.CompileBasic(typed));
                    }
                    else
                    {
                        typed = typed + txt + System.Environment.NewLine;
                        goto beginfreetype;
                    }*/

                }
                #endregion
            }
            else
            {
                #region normal
                string typed = FS.ReadFile(args);


                Console.Clear();
            logo:
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("---------------F1:OPEN--F2:SAVE-----------------------------------------F4:EXIT-");
                Console.WriteLine("-| A B C |--------------------------FLOWTEXT------------------------------------");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(FS.ReadFile(args));
            beginfreetype:
                string txt = Console.ReadLine();
                ConsoleKeyInfo k = Console.ReadKey();
                switch (k.Key)
                {
                    case ConsoleKey.F4:
                        if (string.IsNullOrWhiteSpace(typed))
                        {
                            nonchoix = true;
                            Console.Clear();
                            BeforeRun();
                            Run();
                            nonchoix = false;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        question1:
                            Console.WriteLine("Do you want to save your file ? Y/N");
                            var answer = Console.ReadLine();
                            if (answer == "Y")
                            {
                            qu2:
                                Console.WriteLine("Please type the name :");
                                var answer2 = Console.ReadLine();
                                if (answer2 == null)
                                    goto qu2;
                                else
                                    FS.AddFile(typed, answer2 + ".txt", "/documents");
                            }
                            else if (answer == "N")
                            {
                                nonchoix = true;
                                Console.Clear();
                                BeforeRun();
                                Run();
                                nonchoix = false;
                            }
                            else
                            {
                                goto question1;
                            }
                        }
                        break;
                    case ConsoleKey.F1:
                        if (string.IsNullOrWhiteSpace(typed))
                        {
                            Console.Clear();
                            goto logo;
                            Console.BackgroundColor = ConsoleColor.Magenta;
                        question1:
                            Console.WriteLine("Path : ");

                            var answer = Console.ReadLine();
                            if (fd.readFile(answer) == null)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine("FILE DOESN'T EXIST!!!");
                                Console.BackgroundColor = ConsoleColor.Black;
                                goto question1;

                            }
                            else
                            {
                                Console.Clear();
                                goto logo;
                                Console.WriteLine(FS.ReadFile(answer));
                            }
                        }
                        break;
                    case ConsoleKey.F2:
                    qu3:
                        Console.WriteLine("Please type the name :");
                        var answer3 = Console.ReadLine();
                        if (answer3 == null)
                            goto qu3;
                        else
                            FS.AddFile(typed, answer3 + ".txt", "/documents");
                        break;
                    default:
                        typed = typed + k.KeyChar;
                        break;
                }
                goto beginfreetype;



                #endregion
            }

        }

        /*else if (cmd.ToLower() == "cfc")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("ARGUMENT NEEDED: COLOR");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        else if (cmd.ToLower() == "clist")
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("black");


            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("white");


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("blue");


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("green");


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("red");
        }*/
        else if (cmd.ToLower() == "showgui")
        {
            initGui();

        }
        else if (cmd.ToLower() == "cls")
        {
            Console.Clear();
            BeforeRun();
            Run();
        }
        else if (cmd.ToLower() == "reboot")
        {
            RebootACPI();
        }
        else if (cmd.ToLower() == "shutdown")
        {
            //Cosmos.Sys.Plugs.Assemblers.ShutDown sh = new Cosmos.Sys.Plugs.Assemblers.ShutDown();
            //sh.AssembleNew(null, null);
            Shutdown();
        }
        #region File functions
        else if (cmd.ToLower() == "mkdir")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: rmdir <directory name>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                fd.makeDir(args, "");
            }
        }
        else if (cmd.ToLower() == "rmdir")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: rmdir <directory name>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                fd.Delete(args);
            }
        }
        else if (cmd.ToLower() == "chmod")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("USAGE: chmod <file/folder path> <permissions>");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {

            }
        }
        else if (cmd.ToLower() == "ls")
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                GruntyOS.HAL.fsEntry[] files = fd.getLongList(Environment.CurrentDir);
                //Console.WriteLine("Volume in drive " + Kernel.CurrentDirectory[0].ToString() + " is " + Kernel.FileSystem.getDrive(Kernel.CurrentDirectory).DriveLabel);
                Console.WriteLine("Directory of " + Environment.CurrentDir + "\n");
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Attributes == 2)
                        Console.Write("<DIR>");
                    Console.CursorLeft = 6;
                    Console.WriteLine(files[i].Name + "   ");
                }
            }
            else if (args == "-f")
            {
                GruntyOS.HAL.fsEntry[] files = fd.getLongList(Environment.CurrentDir);
                //Console.WriteLine("Volume in drive " + Kernel.CurrentDirectory[0].ToString() + " is " + Kernel.FileSystem.getDrive(Kernel.CurrentDirectory).DriveLabel);
                Console.WriteLine("Directory of " + Environment.CurrentDir + "\n");
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Attributes == 2)
                        break;
                    Console.CursorLeft = 6;
                    Console.WriteLine(files[i].Name + "   ");
                }
            }
            else if (args == "-d")
            {
                GruntyOS.HAL.fsEntry[] files = fd.getLongList(Environment.CurrentDir);
                //Console.WriteLine("Volume in drive " + Kernel.CurrentDirectory[0].ToString() + " is " + Kernel.FileSystem.getDrive(Kernel.CurrentDirectory).DriveLabel);
                Console.WriteLine("Directory of " + Environment.CurrentDir + "\n");
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Attributes == 2)
                        Console.Write("<DIR>");
                    else
                        break;
                    Console.CursorLeft = 6;
                    Console.WriteLine(files[i].Name + "   ");
                }
            }
            else
            {
                GruntyOS.HAL.fsEntry[] files = fd.getLongList(args);
                //Console.WriteLine("Volume in drive " + Kernel.CurrentDirectory[0].ToString() + " is " + Kernel.FileSystem.getDrive(Kernel.CurrentDirectory).DriveLabel);
                Console.WriteLine("Directory of " + args + "\n");
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Attributes == 2)
                        Console.Write("<DIR>");
                    Console.CursorLeft = 6;
                    Console.WriteLine(files[i].Name + "   ");
                }
            }
        }/*
            else if (cmd.ToLower() == "newtestfile")
            {
                hdd.AddFile("testfile.txt");
             hdd.AddFolder("testfolder");
            }*/
        #endregion
        /*else if ((cmd[0] == 'h') && (cmd[1] == 'e') && (cmd[2] == 'l') && cmd[3] == 'p')
            {
                string commandtohelp = cmd.Replace("help ", "");
                switch (commandtohelp)
                {
                    case "showgui":
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Help of command : " + commandtohelp);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("Switches the system to GUI(see under) mode.");
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Parameters : Nothing");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("GUI :");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("Graphical User Interface");
                        break;
                    default:
                        cmd = "help";
                        goto hlp;
                        break;
                }
            }*/
        /*else if (cmd.ToLower() == "help showgui")
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Help of command : showgui");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Switches the system to GUI(see under) mode.");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Parameters : Nothing");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("GUI :");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Graphical User Interface");
        }*/
        /*else if (cmd.ToLower() == "reboot")
        {
            Cosmos.System.Network.UdpClient client = new Cosmos.System.Network.UdpClient();
            client.Connect(new Sys.Network.IPv4.Address((byte)82, (byte)254, (byte)65, (byte)134), 1500);
            client.Send(Encoding.Unicode.GetBytes("Hello world!"));
            client.Close();
        }*/
        else if (cmd.ToLower() == "debughlp")
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("FlowDOS Developer Help : Page 1/1");
            Console.WriteLine("---------------------------------");
            //Console.WriteLine("img: show a test picture to screen");
            //Console.WriteLine("msgbox: show a 'hello world' message box to the screen");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        /*else if (cmd.ToLower() == "msgbox")
        {
            //FlowDOS.Graphics.ConsoleMessageBox.Show("Hello World!", "Message box", new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), new System.Drawing.Size(200, 100)));
        }*/


    /*else if (cmd.ToLower() == "udp-test")
    {
        Cosmos.System.Network.UdpClient client = new Cosmos.System.Network.UdpClient();
        client.Connect(new Sys.Network.IPv4.Address((byte)82, (byte)254, (byte)65, (byte)134), 1500);
        //client.Send(Encoding.Unicode.GetBytes("Hello world!"));
        client.Send(FS.StringToByte("Hello world"));
        client.Close();
    }
        */
        else if (cmd.ToLower() == "hdvga")
        {
            HD.VGA.SetMode(HD.VGA.ScreenResolution.Res640x480x16);
            HD.VGA.Clear((int)Colors.DesktopBlueBackground);
            DrawCircleHD(10, 10, 100, 0xFF00FF);
            HD.VGA.SetPixel((uint)500, (uint)500, 0x00FF00);
        }
        else if (cmd.ToLower() == "info")
        {

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SYSTEM INFORMATION");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("FlowDOS 0.2 Dev");
            Console.WriteLine("Copyright " + (char)(byte)0169 + " 2012 zVision");
            Console.WriteLine();
            Console.WriteLine("Created with Cosmos User Kit 92560");
            Console.Write("For more informations, go to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("http://cosmos.codeplex.com/");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("COMPUTER INFORMATION");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            //Console.WriteLine("CPU vendor : " + Cosmos.Kernel.CPU.CPUVendor);
        }
        else if (cmd.ToLower() == "lolz")
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                if (i == 1)
                {
                    i = 2;
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (i == 2)
                {
                    i = 3;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (i == 3)
                {
                    i = 4;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else if (i == 4)
                {
                    i = 5;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else if (i == 5)
                {
                    i = 6;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (i == 6)
                {
                    i = 7;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (i == 7)
                {
                    i = 8;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }
                else if (i == 8)
                {
                    i = 9;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else if (i == 9)
                {
                    i = 10;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else if (i == 10)
                {
                    i = 11;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else if (i == 11)
                {
                    i = 12;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (i == 12)
                {
                    i = 13;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (i == 13)
                {
                    i = 14;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else if (i == 14)
                {
                    i = 15;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (i == 15)
                {
                    i = 16;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (i == 16)
                {
                    i = 1;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("LOLZ");
                //Time.Wait(1);
                PIT p = new PIT();
                p.Wait(100);
            }
        }
        else if (cmd.ToLower() == "time")
        {
            Console.Write("24-hours time: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Time.TwentyFourHourToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("12-hours time: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Time.TwelveHourToString());
            Console.ForegroundColor = ConsoleColor.White;
            //Cosmos.System.Network.UdpClient client = new Cosmos.System.Network.UdpClient();
            //client.Connect(new Sys.Network.IPv4.Address((byte)82, (byte)254, (byte)65, (byte)134), 1500);

        }
        else if (cmd.ToLower() == "about")
        {
            #region about
            //clr1();

            Console.ForegroundColor = ConsoleColor.Cyan;
            clr1();
            #region colonnes 1->5

            #region colonne 1
            Console.WriteLine("");
            Console.WriteLine("@");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("@");
            Console.WriteLine("");
            #endregion

            clr1();
            #region colonne 2
            Console.WriteLine("@ ");
            Console.WriteLine(" @");
            Console.WriteLine("");
            Console.WriteLine("@ ");
            Console.WriteLine(" @");
            Console.WriteLine("@ ");
            #endregion
            clr1();
            #region colonne 3
            Console.WriteLine("@@ ");
            Console.WriteLine("  @");
            Console.WriteLine("    ");
            Console.WriteLine("@@ ");
            Console.WriteLine("  @");
            Console.WriteLine("@@ ");
            #endregion
            clr1();
            #region colonne 4
            Console.WriteLine(" @@ ");
            Console.WriteLine("@  @");
            Console.WriteLine("@   ");
            Console.WriteLine(" @@ ");
            Console.WriteLine("@  @");
            Console.WriteLine(" @@ ");
            #endregion
            clr1();
            #region colonne 5
            Console.WriteLine("  @@ ");
            Console.WriteLine(" @  @");
            Console.WriteLine(" @   ");
            Console.WriteLine("  @@ ");
            Console.WriteLine(" @  @");
            Console.WriteLine("  @@ ");
            #endregion
            clr1();
            #endregion
            #region colonnes 6->10
            #region colonne 6
            Console.WriteLine("   @@ ");
            Console.WriteLine("@ @  @");
            Console.WriteLine("@ @   ");
            Console.WriteLine("@  @@ ");
            Console.WriteLine("@ @  @");
            Console.WriteLine("   @@ ");
            #endregion
            clr1();
            #region colonne 7
            Console.WriteLine("@   @@ ");
            Console.WriteLine(" @ @  @");
            Console.WriteLine(" @ @   ");
            Console.WriteLine(" @  @@ ");
            Console.WriteLine(" @ @  @");
            Console.WriteLine("@   @@ ");
            #endregion
            clr1();
            #region colonne 8
            Console.WriteLine("@@   @@ ");
            Console.WriteLine("  @ @  @");
            Console.WriteLine("  @ @   ");
            Console.WriteLine("  @  @@ ");
            Console.WriteLine("  @ @  @");
            Console.WriteLine("@@   @@ ");
            #endregion
            clr1();
            #region colonne 9
            Console.WriteLine(" @@   @@ ");
            Console.WriteLine("@  @ @  @");
            Console.WriteLine("@  @ @   ");
            Console.WriteLine("@  @  @@ ");
            Console.WriteLine("@  @ @  @");
            Console.WriteLine(" @@   @@ ");
            #endregion
            clr1();
            #region colonne 10
            Console.WriteLine("  @@   @@ ");
            Console.WriteLine(" @  @ @  @");
            Console.WriteLine(" @  @ @   ");
            Console.WriteLine(" @  @  @@ ");
            Console.WriteLine(" @  @ @  @");
            Console.WriteLine("  @@   @@ ");
            #endregion
            #endregion
            clr1();
            #region colonnes 11->15
            #region colonne 11
            Console.WriteLine("   @@   @@ ");
            Console.WriteLine("  @  @ @  @");
            Console.WriteLine("@ @  @ @   ");
            Console.WriteLine("@ @  @  @@ ");
            Console.WriteLine("  @  @ @  @");
            Console.WriteLine("   @@   @@ ");
            #endregion
            clr1();
            #region colonne 12
            Console.WriteLine("    @@   @@ ");
            Console.WriteLine("@  @  @ @  @");
            Console.WriteLine(" @ @  @ @   ");
            Console.WriteLine(" @ @  @  @@ ");
            Console.WriteLine("@  @  @ @  @");
            Console.WriteLine("    @@   @@ ");
            #endregion
            clr1();
            #region colonne 13
            Console.WriteLine("@    @@   @@ ");
            Console.WriteLine(" @  @  @ @  @");
            Console.WriteLine("  @ @  @ @   ");
            Console.WriteLine("  @ @  @  @@ ");
            Console.WriteLine(" @  @  @ @  @");
            Console.WriteLine("@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 14
            Console.WriteLine("@@    @@   @@ ");
            Console.WriteLine("@ @  @  @ @  @");
            Console.WriteLine("@  @ @  @ @   ");
            Console.WriteLine("@  @ @  @  @@ ");
            Console.WriteLine("@ @  @  @ @  @");
            Console.WriteLine("@@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 15
            Console.WriteLine(" @@    @@   @@ ");
            Console.WriteLine(" @ @  @  @ @  @");
            Console.WriteLine(" @  @ @  @ @   ");
            Console.WriteLine(" @  @ @  @  @@ ");
            Console.WriteLine(" @ @  @  @ @  @");
            Console.WriteLine(" @@    @@   @@ ");
            #endregion
            #endregion
            clr1();
            /*
        @@@@@ @  @@  @       @  @@    @@   @@
        @     @ @  @ @       @  @ @  @  @ @  @
        @@@@  @ @  @ @   @   @  @  @ @  @ @  
        @     @ @  @  @ @ @ @   @  @ @  @  @@
        @     @ @  @   @   @    @ @  @  @ @  @   
        @     @  @@    @   @    @@    @@   @@   
              */
            #region colonnes 16->20
            #region colonne 16
            Console.WriteLine("  @@    @@   @@ ");
            Console.WriteLine("  @ @  @  @ @  @");
            Console.WriteLine("  @  @ @  @ @   ");
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.WriteLine("  @ @  @  @ @  @");
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 17
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.WriteLine("   @  @ @  @  @@ ");
            Console.WriteLine("   @ @  @  @ @  @");
            Console.WriteLine("   @@    @@   @@ ");
            #endregion

            /*#region colonne 18
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" @");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @@    @@   @@ ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" @");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @ @  @  @ @  @");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" @");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @  @ @  @ @   ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @  @ @  @  @@ ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @ @  @  @ @  @");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  @@    @@   @@ ");
                #endregion*/
            clr1();
            #region colonne 19
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 20
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            #endregion
            #region colonnes 21->25
            clr1();
            #region colonne 21
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 22
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 23
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 24
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("      @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("      @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 25
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            #endregion
            #region colonnes 26->30
            clr1();
            #region colonne 26
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 27
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 28
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 29
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 30
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            #endregion
            #region colonnes 31->35
            clr1();
            #region colonne 31
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 32
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 33
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 34
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 35
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            #endregion
            #region colonnes 36->39
            clr1();
            #region colonne 36
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@ @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@  @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 37
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@@ @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@  @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 38
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@@@ @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@@  @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("     @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            clr1();
            #region colonne 39
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@@@@ @  @@  @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@     @ @  @ @       @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@@@@  @ @  @ @   @   @");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @ @   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@     @ @  @  @ @ @ @ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @  @ @  @  @@ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@     @ @  @   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @ @  @  @ @  @");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@     @  @@   @   @  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  @@    @@   @@ ");
            #endregion
            #endregion
            #endregion
        }



        else
        {

            if (cmd == "")
                return;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("UNKNOWN COMMAND: " + cmd);
            Console.BackgroundColor = ConsoleColor.Black;

        }
        /*switch (cmd.ToLower())
        {
            default:
                  
                if (cmd == "")
                    break;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("UNKNOWN COMMAND: " + cmd);
                Console.BackgroundColor = ConsoleColor.Black;
                break;
        }*/
    }

        public void DrawCircleSD(int x, int y, int radius)
        {
            int centerX = x + radius;
             
            int centerY = y + radius;
           


            for (int i = 0; i < 360; i++)
            {
                double i_rad = 2 * Math.PI * i / 360;

                //                                  \/ conversion|conversion en décimal
                //                                     en integer|de I
                //                                     du cosinus|
                int xpointabc = (int)(centerX + (radius * Math.Cos(2 * (double)i_rad)));
               

                int ypointabc = (int)(centerY + (radius * Math.Sin(2 * (double)i_rad)));


                scr.SetPixel((uint)xpointabc, (uint)ypointabc, (uint)3);
            }
        }

        public void DrawCircleHD(int x, int y, int radius, uint color)
        {
            int centerX = x + radius;

            int centerY = y + radius;



            for (int i = 0; i < 360; i++)
            {
                double i_rad = 2 * Math.PI * i / 360;

                //                                  \/ conversion|conversion en décimal
                //                                     en integer|de I
                //                                     du cosinus|
                int xpointabc = (int)(centerX + (radius * Math.Cos(2 * (double)i_rad)));


                int ypointabc = (int)(centerY + (radius * Math.Sin(2 * (double)i_rad)));

                
                HD.VGA.SetPixel((uint)xpointabc, (uint)ypointabc, color);
            }
        }


        public void KeyPress(byte a, bool b)
        {
        }


        //public abstract HandleKeyboardDelegate InitKeybrd();
   
        public string getKeyFromKey(ConsoleKey i)
        {
            switch (i)
            {
                case ConsoleKey.A:
                    return "a";
                case ConsoleKey.B:
                    return "b";
                case ConsoleKey.C:
                    return "c";
                case ConsoleKey.D:
                    return "d";
                case ConsoleKey.E:
                    return "e";
                case ConsoleKey.F:
                    return "f";
                case ConsoleKey.G:
                    return "g";
                case ConsoleKey.H:
                    return "h";
                case ConsoleKey.I:
                    return "i";
                case ConsoleKey.J:
                    return "j";
                case ConsoleKey.K:
                    return "k";
                case ConsoleKey.L:
                    return "l";
                case ConsoleKey.M:
                    return "m";
                case ConsoleKey.N:
                    return "n";
                case ConsoleKey.O:
                    return "o";
                case ConsoleKey.P:
                    return "p";
                case ConsoleKey.Q:
                    return "q";
                case ConsoleKey.R:
                    return "r";
                case ConsoleKey.S:
                    return "s";
                case ConsoleKey.T:
                    return "t";
                case ConsoleKey.U:
                    return "u";
                case ConsoleKey.V:
                    return "v";
                case ConsoleKey.W:
                    return "w";
                case ConsoleKey.X:
                    return "x";
                case ConsoleKey.Y:
                    return "y";
                case ConsoleKey.Z:
                    return "z";
                case ConsoleKey.Backspace:
                    return "DEL";
                case ConsoleKey.UpArrow:
                    return "UP";
                case ConsoleKey.DownArrow:
                    return "DOWN";
                case ConsoleKey.Spacebar:
                    return " ";
                case ConsoleKey.Enter:
                    return "\n";
                case ConsoleKey.LeftArrow:
                    return "LEFT";
                case ConsoleKey.RightArrow:
                    return "RIGHT";
                case ConsoleKey.Escape:
                    return "ESC";
            }
            return "";
        }

        

        public static void RebootACPI()
        {
            byte good = 0x02;
            while ((good & 0x02) != 0)
                good = Inb(0x64);
            Outb(0x64, 0xFE);
            Cosmos.Core.Global.CPU.Halt();
            Cosmos.Core.Global.CPU.Halt();
        }

        static Cosmos.Core.IOPort io = new Cosmos.Core.IOPort(0);
        static int PP = 0, D = 0;
        public static void Outb(ushort port, byte data)
        {
            if (io.Port != port)
                io = new Cosmos.Core.IOPort(port);
            io.Byte = data;
            PP = port;
            D = data;

        }
        public static byte Inb(ushort port)
        {
            if (io.Port != port)
                io = new Cosmos.Core.IOPort(port);
            return io.Byte;

        }

int cubeX = 10;
            int cubeY = 20;



            int xold = 0;
            int yold = 0;


            public static SystemColors GetPaletteColorFromHexa(uint clr)
            {
                switch (clr)
                {
                    case 0x000000:
                        return SystemColors.Black;
                        break;
                    default:
                        return 0;
                        break;
                }
            }




        void initGui()
        {
            
            InitColors();

            
FlowDOS.Hardware.Mouse.Initialize();

/*while (true)
{
    Console.WriteLine("X: " + FlowDOS.Hardware.Mouse.X + "; Y: " + FlowDOS.Hardware.Mouse.Y);
   
}*/
scr.SetMode320x200x8();
scr.Clear(19);
//scr.SetPaletteEntry(1, (byte)0,(byte)51,(byte)255);
//            scr.SetPaletteEntry(2, (byte)255,(byte)255,(byte)255);
//            scr.SetPaletteEntry(3, (byte)255, (byte)201, (byte)14);
//            scr.SetPaletteEntry(4, (byte)255, (byte)0, (byte)0);
//            scr.SetPaletteEntry(5, (byte)153, (byte)217, (byte)234);
//            scr.SetPaletteEntry(6, (byte)255, (byte)242, (byte)0);
//
//            scr.SetPaletteEntry(7, (byte)195, (byte)195, (byte)195);
//            scr.SetPaletteEntry(8, (byte)0, (byte)0, (byte)0);
//
//            scr.SetPaletteEntry(9, (byte)127, (byte)127, (byte)127);

            xold = FlowDOS.Hardware.Mouse.X;
            yold = FlowDOS.Hardware.Mouse.Y;

            mainloopone:
            while ((FlowDOS.Hardware.Mouse.X == xold) && (FlowDOS.Hardware.Mouse.Y == yold))
            {
                // code heure
               /* for (int i = 1; i < 10; i++)
                {
                    scr.SetPixel(271, (uint)i, (uint)3);

                }*/
                /*for (int i = 272; i < 306; i++)
                {
                    //scr.PixelHeight = 3;
                    scr.SetPixel((uint)i, (uint)1, (uint)6);
                    scr.SetPixel((uint)i, (uint)2, (uint)6);
                    scr.SetPixel((uint)i, (uint)3, (uint)6);
                    scr.SetPixel((uint)i, (uint)4, (uint)6);
                    scr.SetPixel((uint)i, (uint)5, (uint)6);
                    scr.SetPixel((uint)i, (uint)6, (uint)6);
                    scr.SetPixel((uint)i, (uint)7, (uint)6);
                    scr.SetPixel((uint)i, (uint)8, (uint)6);
                    scr.SetPixel((uint)i, (uint)9, (uint)6);
                }*/
                /*
                drawRect(271, 0, 36, 9, 6);


                //DrawFrameBlackAndWhite(FlowDOS.Font.Chars.A, 4, 7, 273, 2);

                switch(hourFormat)
                {
                    case "12":
                        if (Time.TwelveHourToString().Split(':')[0].Length == 1)
                        {
                            Font.Font.DrawText(273, 2, " " + Time.TwelveHourToString(), 0);
                        }
                        else
                        {
 Font.Font.DrawText(273, 2, Time.TwelveHourToString(), 0);
                        }
               
                if (Cosmos.Hardware.RTC.Hour > 12)
                {
                    Font.Font.DrawText(296, 2, "PM", 0);
                }
                else
                {
                    Font.Font.DrawText(296, 2, "AM", 0);
                }
                break;
                        case "24":
                Font.Font.DrawText(273, 2, " " + Time.TwentyFourHourToString(), 0);
                        break;
                }*/
            }

            //do
            //{
            returndoloop:

           
                do
                {
                    switch (FlowDOS.Hardware.Mouse.Buttons)
                    {
                        case Hardware.Mouse.MouseState.None:
                            //scr.Clear(1);
  if(scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y) == (uint)1 && 
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 1) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 2) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 3) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 4) == (uint)1 &&
                                scr.GetPixel320x200x8((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 4) == (uint)1)
                            {
                                goto returndoloop;
                            }
                            for (int a = 0; a < 320; a++)
                            {
                                for (int b = 0; b < 200; b++)
                                {
                                    if ((scr.GetPixel320x200x8((uint)a, (uint)b) == (uint)1) || (scr.GetPixel320x200x8((uint)a, (uint)b) == (uint)6))
                                    {
                                        scr.SetPixel((uint)a, (uint)b, (uint)19);
                                    }
                                }
                            }
                            setDesk();
                          
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y, (uint)1);
                            setDesk();
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 1, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 1, (uint)FlowDOS.Hardware.Mouse.Y, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y, (uint)1);
                            //setDesk();
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 4, (uint)1);
                            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 4, (uint)1);
                            //setDesk();
                            break;
                        case Hardware.Mouse.MouseState.Left:
                            goto click;
                            break;
                        default:
                            break;
                    }
                    goto mainloopone;
                } while (true);
            
            //} while (FlowDOS.Hardware.Mouse.Buttons != FlowDOS.Hardware.Mouse.MouseState.None);



        click:
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y, (uint)6);

            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 1, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 1, (uint)FlowDOS.Hardware.Mouse.Y, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y, (uint)6);
            
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 2, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 2, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 3, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 4, (uint)FlowDOS.Hardware.Mouse.Y + 4, (uint)6);
            scr.SetPixel((uint)FlowDOS.Hardware.Mouse.X + 3, (uint)FlowDOS.Hardware.Mouse.Y + 4, (uint)6);
 /*if ((FlowDOS.Hardware.Mouse.X >= 272 && FlowDOS.Hardware.Mouse.X <= 306 && FlowDOS.Hardware.Mouse.Y >= 1 && FlowDOS.Hardware.Mouse.Y <= 9))
            {
                if (hourFormat == "12")
                {
                    hourFormat = "24";
                }
                else if(hourFormat == "24")
                {
                    hourFormat = "12";
                }
                goto mainloopone;
            }
            else
            {
                goto mainloopone;
            }*/
            OnClick((uint)FlowDOS.Hardware.Mouse.X, (uint)FlowDOS.Hardware.Mouse.Y);
            goto mainloopone;
        rightclick:
            if (/*(FlowDOS.Hardware.Mouse.X > 0 && FlowDOS.Hardware.Mouse.X < 21 && FlowDOS.Hardware.Mouse.Y > 1 && FlowDOS.Hardware.Mouse.Y < 9) || (FlowDOS.Hardware.Mouse.X == 0 && FlowDOS.Hardware.Mouse.Y == 9)*/FlowDOS.Hardware.Mouse.Y >= 11)
            {
                isThereAMenu = !isThereAMenu;
                if (isThereAMenu)
                {
                }
                else
                {

                }
            }
            Console.Read(); 

        goto returndoloop;
        }
        FlowDOS.Graphics.Windows.Controls.Button btn1 = new Graphics.Windows.Controls.Button();


        void OnClick(uint x, uint y)
        {
            if ((x > 0 && x <= 21 && y > 1 && y < 9))
            {
                //scr.SetTextMode(VGAScreen.TextSize.Size80x25);
                //fd.saveFile(bytenonchoix, "nonchoix", "");
                nonchoix = true;
                //BeforeRun();
                //Run();
                RebootACPI();
            }
            else if ((x >= btn1.Location.x && x <= btn1.Location.x + btn1.Size.Width - 1) && (y >= btn1.Location.y && y <= btn1.Location.y + btn1.Size.Height - 1))
            {
                BtnClick();
            }
            else if (x >= 308 && y >= 1 && y <= 9)
            {
                //!isConfMenu;
                isConfMenu = !isConfMenu;
            }
            else
            {
                return;
            }
        }




        void BtnClick()
        {
            Font.Font.DrawText(150, 180, "Bonjour");
        }

        bool isConfMenu = false;
        
        void setDesk()
        {
        /*    btn1.Location = new Point(150,200);
            btn1.Size = new Size(20, 10);
            btn1.Draw(ref scr);*/
           
            
            // bureau
            //Orvid.Graphics.ImageFormats.PngImage img = null;
            //Orvid.Graphics.Image img1 = new Orvid.Graphics.Image(16, 16);
            //img.Load(new System.IO.Stream());
            //System.IO.MemoryStream str1;
           
            //img.Load(new System.IO.MemoryStream(frai_icon_png));
            //Cosmos.Graphics.frai.Draw(10, 20);

            
            // Barre des taches haut
            for (int i = 0; i < 320; i++)
            {
                scr.SetPixel((uint)i, 0, (uint)105);
            }
            for (int i = 0; i < 320; i++)
            {
                scr.SetPixel((uint)i, 10, (uint)105);
            }
            
          


            // cercle test
            DrawCircleSD(50, 50, 10);


            // Barre des taches bas
            for (int i = 0; i < 320; i++)
            {
                scr.SetPixel((uint)i, 188, (uint)105);
            }
            for (int i = 0; i < 320; i++)
            {
                scr.SetPixel((uint)i, 199, (uint)105);
            }
            // bouton LAUNCH
            for (int i = 0; i < 31; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)189, (uint)14);
                scr.SetPixel((uint)i, (uint)1 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)2 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)3 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)4 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)5 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)6 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)7 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)8 + 189, (uint)14);
                scr.SetPixel((uint)i, (uint)9 + 189, (uint)14);
             
            }
            for (int i = 189; i < 199; i++)
            {
                scr.SetPixel(31, (uint)i, (uint)105);

            }
            FlowDOS.Font.Font.DrawText(1, 191, "LAUNCH", 144);

            FlowDOS.Font.Font.DrawText(50, 20, "ABCDEFGHIJKLMNOPQRSTUVWXYZ:A/B\\", 144);

            // bouton DOS
            for (int i = 0; i < 22; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)1, (uint)14);
                scr.SetPixel((uint)i, (uint)2, (uint)14);
                scr.SetPixel((uint)i, (uint)3, (uint)14);
                scr.SetPixel((uint)i, (uint)4, (uint)14);
                scr.SetPixel((uint)i, (uint)5, (uint)14);
                scr.SetPixel((uint)i, (uint)6, (uint)14);
                scr.SetPixel((uint)i, (uint)7, (uint)14);
                scr.SetPixel((uint)i, (uint)8, (uint)14);
                scr.SetPixel((uint)i, (uint)9, (uint)14);
            }


            // debut code pour le 'D'
            scr.SetPixel((uint)2, (uint)2, (uint)11);
            scr.SetPixel((uint)2, (uint)3, (uint)11);
            scr.SetPixel((uint)2, (uint)4, (uint)11);
            scr.SetPixel((uint)2, (uint)5, (uint)11);
            scr.SetPixel((uint)2, (uint)6, (uint)11);
            scr.SetPixel((uint)2, (uint)7, (uint)11);
            scr.SetPixel((uint)2, (uint)8, (uint)11);
            scr.SetPixel((uint)3, (uint)2, (uint)11);
            scr.SetPixel((uint)4, (uint)3, (uint)11);
            scr.SetPixel((uint)5, (uint)4, (uint)11);
            scr.SetPixel((uint)6, (uint)5, (uint)11);
            scr.SetPixel((uint)6, (uint)6, (uint)11);
            scr.SetPixel((uint)5, (uint)7, (uint)11);
            scr.SetPixel((uint)3, (uint)8, (uint)11);
            scr.SetPixel((uint)4, (uint)8, (uint)11);
            // fin code pour le 'D'

            // debut code pour le 'O'
            scr.SetPixel((uint)9, (uint)2, (uint)11);
            scr.SetPixel((uint)10, (uint)2, (uint)11);
            scr.SetPixel((uint)11, (uint)2, (uint)11);
            scr.SetPixel((uint)9, (uint)8, (uint)11);
            scr.SetPixel((uint)10, (uint)8, (uint)11);
            scr.SetPixel((uint)11, (uint)8, (uint)11);

            scr.SetPixel((uint)8, (uint)3, (uint)11);
            scr.SetPixel((uint)8, (uint)4, (uint)11);
            scr.SetPixel((uint)8, (uint)5, (uint)11);
            scr.SetPixel((uint)8, (uint)6, (uint)11);
            scr.SetPixel((uint)8, (uint)7, (uint)11);
            scr.SetPixel((uint)12, (uint)3, (uint)11);
            scr.SetPixel((uint)12, (uint)4, (uint)11);
            scr.SetPixel((uint)12, (uint)5, (uint)11);
            scr.SetPixel((uint)12, (uint)6, (uint)11);
            scr.SetPixel((uint)12, (uint)7, (uint)11);
            // fin code pour le 'O'

            // debut code pour le 'S'
            scr.SetPixel((uint)15, (uint)2, (uint)11);
            scr.SetPixel((uint)16, (uint)2, (uint)11);
            scr.SetPixel((uint)17, (uint)2, (uint)11);
            scr.SetPixel((uint)15, (uint)8, (uint)11);
            scr.SetPixel((uint)16, (uint)8, (uint)11);
            scr.SetPixel((uint)17, (uint)8, (uint)11);
            scr.SetPixel((uint)14, (uint)3, (uint)11);
            scr.SetPixel((uint)14, (uint)4, (uint)11);
            scr.SetPixel((uint)15, (uint)5, (uint)11);
            scr.SetPixel((uint)16, (uint)5, (uint)11);
            scr.SetPixel((uint)17, (uint)6, (uint)11);
            scr.SetPixel((uint)18, (uint)7, (uint)11);
            scr.SetPixel((uint)14, (uint)7, (uint)11);
            scr.SetPixel((uint)18, (uint)3, (uint)11);
            // fin code pour le 'S'

            for (int i = 1; i < 10; i++)
            {
                scr.SetPixel(22, (uint)i, (uint)105);
                
            }


            // code heure
            
            /*for (int i = 272; i < 306; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)1, (uint)6);
                scr.SetPixel((uint)i, (uint)2, (uint)6);
                scr.SetPixel((uint)i, (uint)3, (uint)6);
                scr.SetPixel((uint)i, (uint)4, (uint)6);
                scr.SetPixel((uint)i, (uint)5, (uint)6);
                scr.SetPixel((uint)i, (uint)6, (uint)6);
                scr.SetPixel((uint)i, (uint)7, (uint)6);
                scr.SetPixel((uint)i, (uint)8, (uint)6);
                scr.SetPixel((uint)i, (uint)9, (uint)6);
            }*/

            //drawRect(271, 0, 36, 9, 6);

            /*for (int i = 276; i < 306; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)1, (uint)1);
                scr.SetPixel((uint)i, (uint)2, (uint)1);
                scr.SetPixel((uint)i, (uint)3, (uint)1);
                scr.SetPixel((uint)i, (uint)4, (uint)1);
                scr.SetPixel((uint)i, (uint)5, (uint)1);
                scr.SetPixel((uint)i, (uint)6, (uint)1);
                scr.SetPixel((uint)i, (uint)7, (uint)1);
                scr.SetPixel((uint)i, (uint)8, (uint)1);
                scr.SetPixel((uint)i, (uint)9, (uint)1);
            }
            //DrawFrameBlackAndWhite(FlowDOS.Font.Chars.A, 4, 7, 273, 2);

            switch (hourFormat)
            {
                case "12":
                    for (int i = 1; i < 10; i++)
                    {
                        scr.SetPixel(271, (uint)i, (uint)105);

                    }
                    for (int i = 272; i < 307; i++)
                    {
                        //scr.PixelHeight = 3;
                        scr.SetPixel((uint)i, (uint)1, (uint)11);
                        scr.SetPixel((uint)i, (uint)2, (uint)11);
                        scr.SetPixel((uint)i, (uint)3, (uint)11);
                        scr.SetPixel((uint)i, (uint)4, (uint)11);
                        scr.SetPixel((uint)i, (uint)5, (uint)11);
                        scr.SetPixel((uint)i, (uint)6, (uint)11);
                        scr.SetPixel((uint)i, (uint)7, (uint)11);
                        scr.SetPixel((uint)i, (uint)8, (uint)11);
                        scr.SetPixel((uint)i, (uint)9, (uint)11);
                    }
                    if ((int)Cosmos.Hardware.RTC.Hour > 9)
                    {
                        Font.Font.DrawText(273, 2, Time.TwelveHourToString(), 144);
                    }
                    else
                    {
                        Font.Font.DrawText(273, 2, "0", 144);
                        Font.Font.DrawText(278, 2, Time.TwelveHourToString(), 144);
                    }

            if (Cosmos.Hardware.RTC.Hour > 12)
            {
                Font.Font.DrawText(296, 2, "PM", 144);
            }
            else
            {
                Font.Font.DrawText(296, 2, "AM", 144);
            }
            /*break;
                *///case "24":
            for (int i = 1; i < 10; i++)
            {
                scr.SetPixel(283, (uint)i, (uint)105);

            }
            for (int i = 284; i < 307; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)1, (uint)11);
                scr.SetPixel((uint)i, (uint)2, (uint)11);
                scr.SetPixel((uint)i, (uint)3, (uint)11);
                scr.SetPixel((uint)i, (uint)4, (uint)11);
                scr.SetPixel((uint)i, (uint)5, (uint)11);
                scr.SetPixel((uint)i, (uint)6, (uint)11);
                scr.SetPixel((uint)i, (uint)7, (uint)11);
                scr.SetPixel((uint)i, (uint)8, (uint)11);
                scr.SetPixel((uint)i, (uint)9, (uint)11);
            }
            //Font.Font.DrawText(285, 2, Time.TwentyFourHourToString(), 144);
            if ((int)Cosmos.Hardware.RTC.Hour > 9)
            {
                Font.Font.DrawText(285, 2, Time.TwentyFourHourToString(), 144);
            }
            else
            {
                Font.Font.DrawText(285, 2, "0", 144);
                Font.Font.DrawText(290, 2, Time.TwentyFourHourToString(), 144);
            }
            /*break;
                default:
            break;*/
            //    }
            #region Menu config
            if (isConfMenu)
            {
                for (int i = 10; i < 20; i++)
                {
                    scr.SetPixel((uint)307, (uint)i, (uint)38);

                }
                for (int i = 307; i < 320; i++)
                {
                    scr.SetPixel((uint)i, (uint)20, (uint)38);

                }
                for (int i = 308; i < 320; i++)
                {
                    //scr.PixelHeight = 3;
                    scr.SetPixel((uint)i, (uint)11, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)12, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)13, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)14, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)15, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)16, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)17, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)18, (uint)SystemColors.Red);
                    scr.SetPixel((uint)i, (uint)19, (uint)SystemColors.Red);
                }
            }
            else
            {
                for (int i = 307; i < 320; i++)
                {
                    for (int j = 10; j < 30; j++)
                    {
                        scr.SetPixel((uint)i, (uint)j, (uint)19);
                    }
                }
            }
            #endregion




            //debut code bouton config droite
            for (int i = 1; i < 10; i++)
            {
                scr.SetPixel((uint)307, (uint)i, (uint)38);

            }

            for (int i = 308; i < 320; i++)
            {
                //scr.PixelHeight = 3;
                scr.SetPixel((uint)i, (uint)1, (uint)14);
                scr.SetPixel((uint)i, (uint)2, (uint)14);
                scr.SetPixel((uint)i, (uint)3, (uint)14);
                scr.SetPixel((uint)i, (uint)4, (uint)14);
                scr.SetPixel((uint)i, (uint)5, (uint)14);
                scr.SetPixel((uint)i, (uint)6, (uint)14);
                scr.SetPixel((uint)i, (uint)7, (uint)14);
                scr.SetPixel((uint)i, (uint)8, (uint)14);
                scr.SetPixel((uint)i, (uint)9, (uint)14);
            }

            // clé a mollette gris foncé
            // couleurs : 9 foncé
            // 7 clair

            //tige
            scr.SetPixel((uint)310, (uint)8, (uint)2);
            scr.SetPixel((uint)311, (uint)7, (uint)2);
            scr.SetPixel((uint)312, (uint)6, (uint)2);

            //cadre
            scr.SetPixel((uint)313, (uint)5, (uint)2);

            //direction: haut
            scr.SetPixel((uint)313, (uint)4, (uint)2);
            scr.SetPixel((uint)313, (uint)3, (uint)2);
            scr.SetPixel((uint)313, (uint)2, (uint)2);
            scr.SetPixel((uint)314, (uint)2, (uint)2);

            scr.SetPixel((uint)314, (uint)5, (uint)2);
            scr.SetPixel((uint)315, (uint)5, (uint)2);
            scr.SetPixel((uint)316, (uint)5, (uint)2);
            scr.SetPixel((uint)316, (uint)4, (uint)2);


            // clé a mollette gris clair

            //tige
            scr.SetPixel((uint)310, (uint)7, (uint)7);
            scr.SetPixel((uint)311, (uint)6, (uint)7);
            scr.SetPixel((uint)312, (uint)5, (uint)7);


            // cadre haut
            scr.SetPixel((uint)312, (uint)4, (uint)7);
            scr.SetPixel((uint)312, (uint)3, (uint)7);
            scr.SetPixel((uint)312, (uint)2, (uint)7);
            scr.SetPixel((uint)312, (uint)1, (uint)7);
            scr.SetPixel((uint)313, (uint)1, (uint)7);
            scr.SetPixel((uint)314, (uint)1, (uint)7);

            // milieu
            scr.SetPixel((uint)314, (uint)4, (uint)7);
            scr.SetPixel((uint)315, (uint)4, (uint)7);

            scr.SetPixel((uint)315, (uint)3, (uint)7);
            scr.SetPixel((uint)316, (uint)3, (uint)7);


            // icone fry
            
            DrawFrame(txt, 16, 16, 10, 20);
        }

        void drawContext()
        {

        }

        void basic()
        {
            //Cosmos.Compiler.Assembler.Assembler asm = null;
            //asm.Initialize();
            //txscr.Clear();
            scr.SetTextMode(VGAScreen.TextSize.Size40x25);
            scr.SetTextMode(VGAScreen.TextSize.Size40x50);
            scr.SetTextMode(VGAScreen.TextSize.Size80x25);
            BeforeRun();
            Run();
            //txscr.SetColors(ConsoleColor.White, ConsoleColor.Blue);
        }

        public void SetColorEntry(int index, byte[] Pal)
        {
            scr.SetPalette(index, Pal);
        }

        void InitColors()
        {
            
            SetColorEntry(0, SysColors.Black);
            SetColorEntry(1, SysColors.White);
            SetColorEntry(2, SysColors.Gray);
            SetColorEntry(3, SysColors.Magenta);
            SetColorEntry(4, SysColors.Purple);
            SetColorEntry(5, SysColors.DarkPurple);
            SetColorEntry(6, SysColors.Red);
            SetColorEntry(7, SysColors.LightGray);
            SetColorEntry(8, SysColors.Green);
            SetColorEntry(9, SysColors.Lime);
            SetColorEntry(10, SysColors.DarkGray);
            SetColorEntry(11, SysColors.Yellow);
            SetColorEntry(12, SysColors.AliceBlue);// 12
            SetColorEntry(13, SysColors.AntiqueWhite); // 13
            SetColorEntry(14, SysColors.Aquamarine); // 14
            SetColorEntry(15, SysColors.Azure);    // 15
            SetColorEntry(16, SysColors.Beige);      // 16
            SetColorEntry(17, SysColors.Bisque);      // 17
            SetColorEntry(18, SysColors.BlanchedAlmond);  // 18
            SetColorEntry(19, SysColors.Blue);                 // 19
            SetColorEntry(20, SysColors.BlueViolet);        // 20
            SetColorEntry(21, SysColors.Brown);             // 21
            SetColorEntry(22, SysColors.BurlyWood);      // 22
            SetColorEntry(23, SysColors.CadetBlue);         // 23     
            SetColorEntry(24, SysColors.Chartreuse);       // 24 
            SetColorEntry(25, SysColors.Chocolate);       // 25 
            SetColorEntry(26, SysColors.Coral);       // 26 
            SetColorEntry(27, SysColors.CornflowerBlue);  // 27
            SetColorEntry(28, SysColors.Cornsilk);       // 28 
            SetColorEntry(29, SysColors.Crimson);           // 29 
            SetColorEntry(30, SysColors.Cyan);             // 30 
            SetColorEntry(31, SysColors.DarkBlue);        // 31 
            SetColorEntry(32, SysColors.DarkCyan);         // 32 
            SetColorEntry(33, SysColors.DarkGrey);      // 33 
            SetColorEntry(34, SysColors.DarkGreen); // 34 
            SetColorEntry(35, SysColors.DarkKhaki);    // 35 
            SetColorEntry(36, SysColors.DarkMagneta);    // 36 
            SetColorEntry(37, SysColors.DarkOliveGreen);// 37 
            SetColorEntry(38, SysColors.DarkOrange);    // 38 
            SetColorEntry(39, SysColors.DarkOrchid);      // 39 
            SetColorEntry(40, SysColors.DarkRed);            // 40 
            SetColorEntry(41, SysColors.DarkSalmon);    // 41 
            SetColorEntry(42, SysColors.DarkSeaGreen);    // 42 
            SetColorEntry(43, SysColors.DarkSlateBlue);     // 43 
            SetColorEntry(44, SysColors.DarkSlateGrey);     // 44 
            SetColorEntry(45, SysColors.DarkTurquiose);       // 45 
            SetColorEntry(46, SysColors.DarkViolet);        // 46
            SetColorEntry(47, SysColors.DeepPink);         // 47
            SetColorEntry(48, SysColors.DeepSkyBlue);       // 48 
            SetColorEntry(49, SysColors.DimGrey);          // 49
            SetColorEntry(50, SysColors.DodgerBlue);       // 50
            SetColorEntry(51, SysColors.FireBrick);         // 51
            SetColorEntry(52, SysColors.FloralWhite);    // 52
            SetColorEntry(53, SysColors.ForestGreen);      // 53 
            SetColorEntry(54, SysColors.Fuchsia);           // 54 
            SetColorEntry(55, SysColors.Gainsboro);      // 55 
            SetColorEntry(56, SysColors.GhostWhite);      // 56
            SetColorEntry(57, SysColors.Gold);            // 57
            SetColorEntry(58, SysColors.GoldenRod);   // 58
            SetColorEntry(59, SysColors.Web_Green);          // 60
            SetColorEntry(60, SysColors.GreenYellow);     // 61
            SetColorEntry(61, SysColors.HoneyDew);       // 62
            SetColorEntry(62, SysColors.HotPink);         // 63
            SetColorEntry(63, SysColors.IndianRed);          // 64
            SetColorEntry(64, SysColors.Indigo);            // 65
            SetColorEntry(65, SysColors.Ivory);          // 66
            SetColorEntry(66, SysColors.Khaki);           // 67
            SetColorEntry(67, SysColors.Lavender);       // 68 
            SetColorEntry(68, SysColors.LavenderBlush);   // 69 
            SetColorEntry(69, SysColors.LawnGreen);        // 70 
            SetColorEntry(70, SysColors.LemonChiffon);  // 71 
            SetColorEntry(71, SysColors.LightBlue);       // 72 
            SetColorEntry(72, SysColors.LightCoral);     // 73
            SetColorEntry(73, SysColors.LightCyan);       // 74
            SetColorEntry(74, SysColors.LightGoldenRodYellow); // 93 
            SetColorEntry(75, SysColors.Web_LightGrey);    // 75 
            SetColorEntry(76, SysColors.LightGreen);     // 76 
            SetColorEntry(77, SysColors.LightPink);       // 77 
            SetColorEntry(78, SysColors.LightSalmon);     // 78 
            SetColorEntry(79, SysColors.LightSeaGreen);     // 79 
            SetColorEntry(80, SysColors.LightSkyBlue);    // 80 
            SetColorEntry(81, SysColors.LightSlateGrey);   // 81 
            SetColorEntry(82, SysColors.LightSteelBlue);   // 82 
            SetColorEntry(83, SysColors.LightYellow);    // 83 
            SetColorEntry(84, SysColors.LimeGreen);       // 84
            SetColorEntry(85, SysColors.Linen);       // 85
            SetColorEntry(86, SysColors.Maroon);           // 86
            SetColorEntry(87, SysColors.MediumAquaMarine);  // 87
            SetColorEntry(88, SysColors.MediumBlue);         // 88
            SetColorEntry(89, SysColors.MediumOrchid);    // 89
            SetColorEntry(90, SysColors.MediumPurple);    // 90
            SetColorEntry(91, SysColors.MediumSeaGreen);   // 91
            SetColorEntry(92, SysColors.MediumSlateBlue); // 92
            SetColorEntry(93, SysColors.MediumSpringGreen); // 98
            SetColorEntry(94, SysColors.MediumTurquoise);  // 99
            SetColorEntry(95, SysColors.MediumVioletRed);// 100
            SetColorEntry(96, SysColors.MidnightBlue); // 101
            SetColorEntry(97, SysColors.MintCream);    // 102
            SetColorEntry(98, SysColors.MintyRose);    // 103
            SetColorEntry(99, SysColors.Moccasin);     // 104
            SetColorEntry(100, SysColors.NavajoWhite);  // 105
            SetColorEntry(101, SysColors.Navy);         // 106
            SetColorEntry(102, SysColors.OldLace);      // 107
            SetColorEntry(103, SysColors.Olive);        // 108
            SetColorEntry(104, SysColors.OliveDrab);    // 109
            SetColorEntry(105, SysColors.Orange);       // 110
            SetColorEntry(106, SysColors.OrangeRed);      // 111
            SetColorEntry(107, SysColors.Orchid);         // 112
            SetColorEntry(108, SysColors.PaleGoldenRod);  // 113
            SetColorEntry(109, SysColors.PaleGreen);      // 114
            SetColorEntry(110, SysColors.PaleTurquoise);  // 115
            SetColorEntry(111, SysColors.PaleVioletRed);  // 116
            SetColorEntry(112, SysColors.PapayaWhip);     // 117
            SetColorEntry(113, SysColors.PeachPuff);      // 118
            SetColorEntry(114, SysColors.Peru);           // 119
            SetColorEntry(115, SysColors.Pink);           // 120
            SetColorEntry(116, SysColors.Plum);          // 121
            SetColorEntry(117, SysColors.PowderBlue);     // 122
            SetColorEntry(118, SysColors.Web_Purple);     // 123
            SetColorEntry(119, SysColors.RosyBrown); ;      // 124
            SetColorEntry(120, SysColors.RoyalBlue);        // 125
            SetColorEntry(121, SysColors.SaddleBrown);      // 126
            SetColorEntry(122, SysColors.Salmon); ;         // 127
            SetColorEntry(123, SysColors.SandyBrown);       // 128
            SetColorEntry(124, SysColors.SeaGreen);         // 129
            SetColorEntry(125, SysColors.SeaShell);        // 130
            SetColorEntry(126, SysColors.Sienna);         // 131
            SetColorEntry(127, SysColors.Silver);         // 132
            SetColorEntry(128, SysColors.SkyBlue);        // 133
            SetColorEntry(129, SysColors.SlateBlue);      // 134
            SetColorEntry(130, SysColors.SlateGrey);      // 135
            SetColorEntry(131, SysColors.Snow);            // 136
            SetColorEntry(132, SysColors.SpringGreen);      // 137
            SetColorEntry(133, SysColors.SteelBlue);        // 138
            SetColorEntry(134, SysColors.Tan);             // 139
            SetColorEntry(135, SysColors.Teal);           // 140
            SetColorEntry(136, SysColors.Thistle);         // 141
            SetColorEntry(137, SysColors.Tomato);          // 142
            SetColorEntry(138, SysColors.Turquoise);       // 143
            SetColorEntry(139, SysColors.Violet);         // 144
            SetColorEntry(140, SysColors.Wheat);           // 145       
            SetColorEntry(141, SysColors.WhiteSmoke);     // 147 
            SetColorEntry(142, SysColors.YellowGreen);      // 149
            SetColorEntry(143, SysColors.HenrysRandom);    // 150
            SetColorEntry(144, SysColors.Black);
        }
   }
}