using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimus
{
    public class PCI : BusIO
    {
        public static int CurrentNum = 0;
        static uint addressx = 0;
        static uint regx = 0;
        static uint slotx = 0;
        static uint funcx = 0;
        static uint busx = 0;
        static uint tmp = 0;

        public static uint[] DeviceData = new uint[16];

        public static PCIDevice[] PCIDeviceList = new PCIDevice[65536];


        public enum DeviceType
        {
            Standard = 1,
            Bridge = 2,
            CardBus = 3,
            Unknown = 4,
        }

        public struct PCIDevice
        {
            public DeviceType aType;
            public string ClassName;
            public string SubClassName;
            public byte slot;
            public byte bus;
            public byte func;
            public ushort VendorID;
            public ushort DeviceID;
            public ushort Status;
            public byte ClassCode;
            public byte SubClass;
            public byte ProgIF;
            public byte RevisionID;
            public byte Header;
            public uint IOBase0;
            public uint IOBase1;
            public uint MemoryBase;
            public uint MemoryBaseLimit;
            public ushort IOLimitUpper;
            public ushort IOBaseUpper;
            public ushort SubSystemID;
            public ushort SubSystemVendorID;
            public ushort BridgeControl;
            public byte InterruptPin;
            public byte InterruptLine;
        }
        //Supportes up to 64K Devices
        //Max# PCI Buses = 256
        //Max# PCI Slot = 32
        //Max# PCI Devices(func) = 8

        public static UInt32 PciConfigReadWord(byte bus, byte slot, byte func, byte RegNum)
        {
            //Reg Num    6 bits
            //Func Num   3 bits
            //Device Num 5 bits
            //Bus Nume   8 bits
            addressx = 0x80000000;

            regx = RegNum;
            regx = regx << 2;//Clear all other bits - OK

            funcx = (uint)(func & 0x07);//Clear all other bits     
            funcx = funcx << 8;

            slotx = (byte)(slot & 0x1F);//Clear all other bits
            slotx = slotx << 11;

            busx = bus;//Its 8 bit so leave it as is.
            busx = busx << 16;

            addressx = (addressx | busx | slotx | funcx | regx);
            tmp = 0;
            Write32(0xCF8, addressx);
            tmp = Read32(0xCFC);
            return tmp;
        }

        public static void Read256Bytes(byte bus, byte slot, byte func)
        {
            DeviceData[0] = PciConfigReadWord(bus, slot, func, 0x00);
            DeviceData[1] = PciConfigReadWord(bus, slot, func, 0x04);
            DeviceData[2] = PciConfigReadWord(bus, slot, func, 0x08);
            DeviceData[3] = PciConfigReadWord(bus, slot, func, 0x0C);
            DeviceData[4] = PciConfigReadWord(bus, slot, func, 0x10);
            DeviceData[5] = PciConfigReadWord(bus, slot, func, 0x14);
            DeviceData[6] = PciConfigReadWord(bus, slot, func, 0x18);
            DeviceData[7] = PciConfigReadWord(bus, slot, func, 0x1C);
            DeviceData[8] = PciConfigReadWord(bus, slot, func, 0x20);
            DeviceData[9] = PciConfigReadWord(bus, slot, func, 0x24);
            DeviceData[10] = PciConfigReadWord(bus, slot, func, 0x28);
            DeviceData[11] = PciConfigReadWord(bus, slot, func, 0x2C);
            DeviceData[12] = PciConfigReadWord(bus, slot, func, 0x30);
            DeviceData[13] = PciConfigReadWord(bus, slot, func, 0x34);
            DeviceData[14] = PciConfigReadWord(bus, slot, func, 0x38);
            DeviceData[15] = PciConfigReadWord(bus, slot, func, 0x3C);
        }

        static byte f = 0;
        public static void FindPCIDevices(byte bus, byte slot)
        {
            for (int b = 0; b < 8; b++)
            {
                Read256Bytes(bus, slot, (byte)b);
                PCIDeviceList[CurrentNum].VendorID = (ushort)(DeviceData[0]);
                PCIDeviceList[CurrentNum].DeviceID = (ushort)(DeviceData[0] >> 16);
                uint tmp1 = 0;
                tmp1 = DeviceData[2];
                PCIDeviceList[CurrentNum].RevisionID = (byte)DeviceData[2];
                PCIDeviceList[CurrentNum].ProgIF = (byte)(DeviceData[2] >> 8);
                PCIDeviceList[CurrentNum].SubClass = (byte)(DeviceData[2] >> 16);
                PCIDeviceList[CurrentNum].ClassCode = (byte)(tmp1 >> 24);
                CurrentNum++;
            }
        }



        public static string[] VendorName = new string[] { };
        public static int[] VendorIDs = new int[] { };
        public static int LSI = 0;

        public static Dictionary aDicl;

        public struct Dictionary
        {
            private static String Temp0;
            private static int Temp1;
            private static string[] EntryName = new string[] { };
            private static int[] EntryIDs = new int[] { };
            public void AddEntry(String Name, int EntryID)
            {
                EntryName[LSI] = Name;
                EntryIDs[LSI] = EntryID;
            }
            public int GetEntryID(String Name)
            {
                for (int i = 0; i < EntryName.Length; i++)
                {
                    if (EntryName[i] == Name)
                    {
                        Temp1 = EntryIDs[i];
                        break;
                    }
                }
                return Temp1;
            }
            public String GetEntryName(int EntryID)
            {
                for (int i = 0; i < EntryName.Length; i++)
                {
                    if (EntryIDs[i] == EntryID)
                    {
                        Temp0 = EntryName[i];
                        break;
                    }
                }
                return Temp0;
            }
        }


        public static void GetNames()
        {
            for (int i = 0; i < CurrentNum; i++)
            {
                if (PCIDeviceList[i].VendorID != 0xFFFF)
                {
                    if (PCIDeviceList[i].ClassCode == 0)
                    {
                        PCIDeviceList[i].ClassName = "Old Device";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Non VGA-Compatible Device";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "VGA-Compatible Device";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x1)
                    {
                        PCIDeviceList[i].ClassName = "Mass Storage Controller";
                        if (PCIDeviceList[i].SubClass == 0)
                        {
                            PCIDeviceList[i].SubClassName = "SCSI Bus Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "IDE Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 2)
                        {
                            PCIDeviceList[i].SubClassName = "Floppy Disk Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 3)
                        {
                            PCIDeviceList[i].SubClassName = "IPI Bus Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 4)
                        {
                            PCIDeviceList[i].SubClassName = "RAID Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 5)
                        {
                            //Prog IF 0x20 or 0x30
                            PCIDeviceList[i].SubClassName = "ATA Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 6)
                        {
                            PCIDeviceList[i].SubClassName = "Serial ATA (Direct Port Access)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Mass Storage Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x2)
                    {
                        PCIDeviceList[i].ClassName = "Network Controller";
                        if (PCIDeviceList[i].SubClass == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Ethernet Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "Token Ring Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 2)
                        {
                            PCIDeviceList[i].SubClassName = "FDDI Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 3)
                        {
                            PCIDeviceList[i].SubClassName = "ATM Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 4)
                        {
                            PCIDeviceList[i].SubClassName = "ISDN Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 5)
                        {
                            PCIDeviceList[i].SubClassName = "WorldFip Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 6)
                        {
                            PCIDeviceList[i].SubClassName = "PICMG 2.14 Multi Computing";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Network Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x3)
                    {
                        PCIDeviceList[i].ClassName = "Display Controller";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "VGA-Compatible Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "8512-Compatible Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "XGA Compatible Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 2)
                        {
                            PCIDeviceList[i].SubClassName = "3D Controller (Not VGA-Compatible)";
                        }

                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Display Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x4)
                    {
                        PCIDeviceList[i].ClassName = "Multimedia Controller";

                        if (PCIDeviceList[i].SubClass == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Video Device";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "Audio Device";
                        }
                        else if (PCIDeviceList[i].SubClass == 2)
                        {
                            PCIDeviceList[i].SubClassName = "Computer Telephony Device";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Multimedia Device";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x5)
                    {
                        PCIDeviceList[i].ClassName = "Memory Controller";

                        if (PCIDeviceList[i].SubClass == 0)
                        {
                            PCIDeviceList[i].SubClassName = "RAM Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "Flash Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Memory Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x6)
                    {
                        PCIDeviceList[i].ClassName = "Bridge Device";

                        if (PCIDeviceList[i].SubClass == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Host Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 1)
                        {
                            PCIDeviceList[i].SubClassName = "ISA Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 2)
                        {
                            PCIDeviceList[i].SubClassName = "EISA Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 3)
                        {
                            PCIDeviceList[i].SubClassName = "MCA Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 4)
                        {
                            //0x00 and 0x01 Normal ,Subtractive decode
                            PCIDeviceList[i].SubClassName = "PCI-To-PCI Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 5)
                        {
                            PCIDeviceList[i].SubClassName = "PCMCIA Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 6)
                        {
                            PCIDeviceList[i].SubClassName = "NuBus Brigde";
                        }
                        else if (PCIDeviceList[i].SubClass == 7)
                        {
                            PCIDeviceList[i].SubClassName = "CardBus Brigde";
                        }
                        else if (PCIDeviceList[i].SubClass == 8)
                        {
                            PCIDeviceList[i].SubClassName = "RACEway Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 9)
                        {
                            //0x40,0x80
                            PCIDeviceList[i].SubClassName = "PCI-To-PCI Bridge (Semi Transparent)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0xA)
                        {
                            PCIDeviceList[i].SubClassName = "InfiniBrand-to-PCI Host Bridge";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Network Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x7)
                    {
                        PCIDeviceList[i].ClassName = "Simple Communication Controller";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic XT-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "16450-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "16550-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 3)
                        {
                            PCIDeviceList[i].SubClassName = "16650-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 4)
                        {
                            PCIDeviceList[i].SubClassName = "16750-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 5)
                        {
                            PCIDeviceList[i].SubClassName = "16850-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 6)
                        {
                            PCIDeviceList[i].SubClassName = "16950-Compatible Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Parallerl Port";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "BiDirectional Parallel Port";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "ECP 1.X Compliant Parallel Port";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 3)
                        {
                            PCIDeviceList[i].SubClassName = "IEEE 1284 Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0xFE)
                        {
                            PCIDeviceList[i].SubClassName = "IEEE 1284 Target Device";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Multiport Serial Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic Modem";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "Hayes Compatible Modem (16450-Compatible Interface)";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "Hayes Compatible Modem (16550-Compatible Interface)";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 3)
                        {
                            PCIDeviceList[i].SubClassName = "Hayes Compatible Modem (16650-Compatible Interface)";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 0xFE)
                        {
                            PCIDeviceList[i].SubClassName = "Hayes Compatible Modem (16750-Compatible Interface)";
                        }
                        else if (PCIDeviceList[i].SubClass == 4 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "IEEE 488.1/2 (GPIB) Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 5 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Smart Card";
                        }

                        else if (PCIDeviceList[i].SubClass == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Communications Device";
                        }


                    }
                    else if (PCIDeviceList[i].ClassCode == 0x8)
                    {
                        PCIDeviceList[i].ClassName = "Base System Peripheral";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic 8259 PIC";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "ISA PIC";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "EISA PCI";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0x10)
                        {
                            PCIDeviceList[i].SubClassName = "I/O APIC Interrupt Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0x20)
                        {
                            PCIDeviceList[i].SubClassName = "I/O(x) APIC Interrupt Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic 8237 DMA Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "ISA DMA Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "EISA DMA Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic 8254 System Timer";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "ISA System Timer";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "EISA  System Timer";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic RTC Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 1)
                        {
                            PCIDeviceList[i].SubClassName = "ISA RTC Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 4 & PCIDeviceList[i].ProgIF == 2)
                        {
                            PCIDeviceList[i].SubClassName = "Generic PCI Hot-Plug Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown System Peripheal";
                        }

                    }
                    else if (PCIDeviceList[i].ClassCode == 0x9)
                    {
                        PCIDeviceList[i].ClassName = "Input Device";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Keyboard Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Digitizer";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Mouse Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 3 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Scanner Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 4 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Gameport Controller (Generic)";
                        }
                        else if (PCIDeviceList[i].SubClass == 4 & PCIDeviceList[i].ProgIF == 0x10)
                        {
                            PCIDeviceList[i].SubClassName = "Gameport Controller (Legacy)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Input Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0xA)
                    {
                        PCIDeviceList[i].ClassName = "Docking Station";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Generic Docking Station";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unkown Docking Station";
                        }

                    }
                    else if (PCIDeviceList[i].ClassCode == 0xB)
                    {
                        PCIDeviceList[i].ClassName = "Processor";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "386 Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "486 Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Pentium Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x10 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Alpha Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x20 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "PowerPC Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x30 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "MIPS Processor";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x40 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Co-Processor";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0xC)
                    {
                        PCIDeviceList[i].ClassName = "Serial Bus Controller";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "IEEE 1394 Controller (FireWire)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0x10)
                        {
                            PCIDeviceList[i].SubClassName = "IEEE 1394 Controller (1394 OpenHCI Spec)";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "ACCESS.bus";
                        }
                        else if (PCIDeviceList[i].SubClass == 2 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "SSA";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "USB (UHCI Spec)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3 & PCIDeviceList[i].ProgIF == 0x10)
                        {
                            PCIDeviceList[i].SubClassName = "USB (OHCI Spec)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3 & PCIDeviceList[i].ProgIF == 0x20)
                        {
                            PCIDeviceList[i].SubClassName = "USB2 Host Controller (Intel EHCI)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3 & PCIDeviceList[i].ProgIF == 0x80)
                        {
                            PCIDeviceList[i].SubClassName = "USB";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3 & PCIDeviceList[i].ProgIF == 0xFE)
                        {
                            PCIDeviceList[i].SubClassName = "USB Device";
                        }

                        else if (PCIDeviceList[i].SubClass == 0x4 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "Fibre Channel";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x5 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "SMBus";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x6 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "InfiniBand";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x7 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "IPMI SMIC Interface";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x7 & PCIDeviceList[i].ProgIF == 0x1)
                        {
                            PCIDeviceList[i].SubClassName = "IPMI Kydb Controller Style Interface";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x7 & PCIDeviceList[i].ProgIF == 0x2)
                        {
                            PCIDeviceList[i].SubClassName = "IPI Block Transfer Interface";
                        }

                        else if (PCIDeviceList[i].SubClass == 0x8 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "SERCOS Interface Standard (IEC 61491)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x9 & PCIDeviceList[i].ProgIF == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "CANbus";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0xD)
                    {
                        PCIDeviceList[i].ClassName = "Wireless Controller";

                        if (PCIDeviceList[i].SubClass == 0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "iRDA Compatible Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Consumer IR Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x10 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "RF Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x11 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Bluetooth Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x12 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Broadband Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x20 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Ethernet Controller (802.11a)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x21 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Ethernet Controller (802.11b)";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Wirless Controller";
                        }

                    }
                    else if (PCIDeviceList[i].ClassCode == 0xE)
                    {
                        PCIDeviceList[i].ClassName = "Intelligent I/O Controllers";
                        if (PCIDeviceList[i].SubClass == 0x0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Message FIFO";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x0)
                        {
                            PCIDeviceList[i].SubClassName = "I20 Architecture";
                        }
                    }

                    else if (PCIDeviceList[i].ClassCode == 0xF)
                    {
                        PCIDeviceList[i].ClassName = "Satellite Communication Controller";
                        if (PCIDeviceList[i].SubClass == 0x1)
                        {
                            PCIDeviceList[i].SubClassName = "TV Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x2)
                        {
                            PCIDeviceList[i].SubClassName = "Audio Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x3)
                        {
                            PCIDeviceList[i].SubClassName = "Voice Controller";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x4)
                        {
                            PCIDeviceList[i].SubClassName = "Data Controller";
                        }
                    }
                    else if (PCIDeviceList[i].ClassCode == 0x10)
                    {
                        PCIDeviceList[i].ClassName = "Encryption/Decryption Controller";

                        if (PCIDeviceList[i].SubClass == 0x0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Network and Computing Encrpytion/Decryption";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x10 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Entertainment Encryption/Decryption";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Encryption/Decryption";
                        }

                    }
                    else if (PCIDeviceList[i].ClassCode == 0x11)
                    {
                        PCIDeviceList[i].ClassName = "Data Acquisition and Signal Processing Controller";

                        if (PCIDeviceList[i].SubClass == 0x0 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "DPIO";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x1 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Performance Counters";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x10 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Communications Syncrhonization Plus Time and Frequency Test/Measurment";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x20 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Management Card";
                        }
                        else if (PCIDeviceList[i].SubClass == 0x80 & PCIDeviceList[i].ProgIF == 0)
                        {
                            PCIDeviceList[i].SubClassName = "Unknown Data Acquisition/Signal Processing Controller";
                        }
                    }
                }

            }
        }

        Cosmos.Core.MemoryAddressSpace EBDA = new Cosmos.Core.MemoryAddressSpace(0x00E0000, 0x1FFFF);//128KB
        public unsafe static void ProbeMemory()
        {
            uint Data = 0, Data1 = 0;
            uint x = 0x20545352;
            uint y = 0x20525450;
            ushort Data0 = 0;
            Data0 = (ushort)*(ushort*)(0x040E);

            Console.WriteLine("EBDA  Base Address: " + Data0.ToString());
            for (int i = 0; i < 0x1FFFF; i++)
            {
                Data = (uint)*(uint*)(Data0 + i);
                Data1 = (uint)*(uint*)(Data0 + (i + 1));
                if (Data == x & Data1 == y)
                {
                    Console.WriteLine("Got it!");
                    break;
                }
            }
        }


        public static void SearchPCIDevices(byte bus, byte slot)
        {
            tmp = 0;
            for (int i = 0; i < 8; i++)
            {
                tmp = PciConfigReadWord(bus, slot, (byte)i, 0x00);
                PCIDeviceList[i].VendorID = (ushort)(tmp);

                tmp = PciConfigReadWord(bus, slot, (byte)i, 0x08);
                PCIDeviceList[i].ClassCode = (byte)(tmp >> 24);
                PCIDeviceList[i].SubClass = (byte)((tmp << 8) >> 24);
                CurrentNum++;
            }
        }


    }

}