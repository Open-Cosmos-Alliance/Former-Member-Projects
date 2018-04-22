using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Filesystem.Praxis
{
    class Partition {
        private Cosmos.Hardware.BlockDevice.Partition partition;
        private uint clusterScale = 1;
        public ulong clusterSize = 4096;
        public ulong clusterAmount = 0;
        public Partition(Cosmos.Hardware.BlockDevice.Partition part, clusterSize cluster_size) {
            clusterAmount = (ulong)cluster_size;
            clusterScale = (uint)cluster_size / (uint)part.BlockSize;
            clusterAmount = part.BlockCount / (ulong)cluster_size;
        }
        public void writeCluster(ulong cluster, byte[] content) {
            if ((ulong)content.Length == clusterSize) {
                partition.WriteBlock(cluster, clusterScale, content);
            }
        }
        public void readCluster(ulong cluster, ref byte[] content) {
            if ((ulong)content.Length == clusterSize) {
                partition.ReadBlock(cluster, clusterScale, content);
            }
        }
        public byte[] readCluster(ulong cluster) {
            byte[] content = new byte[clusterSize];
            partition.ReadBlock(cluster, clusterScale, content);
            return content;
        }
    }
    enum clusterSize : ulong {
        c4096 = 4096,
    }
    unsafe class Praxis
    {
        Partition partition;
        public uint totalEntries = 0;
        public ulong nextAvailableSector = 0;
        /// <summary>
        /// Formats a partition with Praxis
        /// </summary>
        /// <param name="part">Partition to format with Praxis</param>
        public Praxis(Partition part, bool format) {
            partition = part;
            byte[] c0 = part.readCluster(0);
            fixed (byte* ptr = c0) {
                if (format) {
                    UnsignedWriter writer = new UnsignedWriter(ptr);
                    writer.Advance(0x0020); //Label, ASCII not implemented, so will advance until then
                    writer.Write16(0xF00F); //if 0xF00F then drive is formatted
                    writer.Write64(0x0000); //Used clusters
                    writer.Write32(0x0000); //Used entries
                    writer.Write16(0x0001); //Major version
                    writer.Write16(0x0000); //Minor version
                    writer.Write32(0x0000); //Bug fixes
                    writer.Write64(0x646E454878617250); //UTF8 for PraxHEnd
                }
                UnsignedReader reader = new UnsignedReader(ptr);
                reader.Advance(42);
                totalEntries = reader.Read32();
            }
            partition.writeCluster(0, c0);
        }
        public void writeEntry(byte dirorfile, uint hash, ulong sector) {
            byte[] c1 = partition.readCluster(1UL);
            fixed (byte* ptr = c1) {
                UnsignedWriter c1W = new UnsignedWriter(ptr);
                c1W.Advance(1U);
                Entry.Write(c1W, dirorfile, hash, sector, totalEntries);
                incTotalEntries();
            }
            partition.writeCluster(1UL, c1);
        }
        public ulong getSectorOfFile(uint hash, ulong sector) {
            byte[] tsector = partition.readCluster(sector);
            bool found = false;
            uint times = 0;
            while (!found && times <= 314) {
                if (Listing.getHash(tsector, times) == hash && Listing.isDirectory(tsector, times) == 0xF0) return Listing.getSector(tsector, times) ;
                times++;
            }
            return 0;
        }
        public ulong getSectorOfDir(uint hash, ulong sector)
        {
            byte[] tsector = partition.readCluster(sector);
            bool found = false;
            uint times = 0;
            while (!found && times <= 314)
            {
                if (Listing.getHash(tsector, times) == hash && Listing.isDirectory(tsector, times) == 0x0F) return Listing.getSector(tsector, times);
                times++;
            }
            return 0;
        }
        public void incTotalEntries() {
            totalEntries++;
            byte[] c0 = partition.readCluster(0);
            fixed (byte* ptr = c0)
            {
                UnsignedWriter reader = new UnsignedWriter(ptr);
                reader.Write32AtOffset(totalEntries, 42);
            }
            partition.writeCluster(0UL, c0);
        }
        public void incAvailable() {
            nextAvailableSector++;
            byte[] c0 = partition.readCluster(0);
            fixed (byte* ptr = c0)
            {
                UnsignedWriter writer = new UnsignedWriter(ptr);
                writer.Write64AtOffset(nextAvailableSector, 34);
            }
            partition.writeCluster(0UL, c0);
        }
    }
    unsafe class Listing {
        //Each listing has the ulong next cluster in front, followed by the Entries(byte isdirectory(0xF0 file, 0x0F directory), uint hash, ulong sector)
        public static void writeEntry(byte[] listing, uint i, EntryStruct entry) {
            fixed (byte* ptr = listing) {
                UnsignedWriter w = new UnsignedWriter(ptr);
                w.Advance(8 + (i * 13));
                w.Write8(entry.isDirectory);
                w.Write32(entry.hash);
                w.Write64(entry.sector);
            }
        }
        public static void setNextSector(byte[] listing, ulong sector) {
            fixed (byte* ptr = listing) {
                UnsignedWriter w = new UnsignedWriter(ptr);
                w.Write64(sector);
            }
        }
        public static uint getHash(byte[] listing, uint num) {
            return (new UnsignedReader((byte*)listing[9U + (13U * num)]).Read32());
        }
        public static uint getSector(byte[] listing, uint num) {
            return (new UnsignedReader((byte*)listing[13U + (13U * num)]).Read32());
        }
        public static byte isDirectory(byte[] listing, uint num) {
            return (new UnsignedReader((byte*)listing[8U + (13U * num)]).Read8());
        }
    }
    unsafe class Directory {
        
        }
    unsafe class Entry {
        public static void Write(UnsignedWriter writer, byte dirorfile, uint hash, ulong sector, uint which) {
            writer.Advance(8U + (which * 13U));
            writer.Write8(dirorfile);
            writer.Write32(hash);
            writer.Write64(sector);
        }
        public static EntryStruct Read(UnsignedReader reader, byte dirorfile, uint hash, ulong sector, uint which) {
            reader.Advance(8U + (which + 13U));
            return new EntryStruct(reader.Read8(), reader.Read32(), reader.Read64());
        }
    }
    struct EntryStruct {
        public byte isDirectory;
        public uint hash;
        public ulong sector;
        public EntryStruct(byte par0, uint par1, ulong par2) { //isDir, hash, sector
            isDirectory = par0; hash = par1; sector = par2;
        }
    }
}
