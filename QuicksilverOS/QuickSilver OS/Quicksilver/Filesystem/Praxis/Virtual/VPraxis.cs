using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Praxis.Emulator;

namespace Praxis
{
    public class PraxisFormatter
    {
        public static void format(Partition p, string label)
        {
            byte[] buffer = new byte[2048];
            var ms = new MemBlocks(buffer);
            ms.Write(Encoding.UTF8.GetBytes(label), 0, 32); //under 32 characters, one word. Used in &Label/
            ms.Write(BitConverter.GetBytes(0x00000000), 32, 4); //next sector
            ms.Write(new byte[] { 0xF0, 0x0F }, 36, 2); //is formatted
            ms.Write(BitConverter.GetBytes(1), 38, 4); //current used sectors
            ms.Write(BitConverter.GetBytes(0), 42, 4); //number of used entrys
            //format for files/directory is byte directory,int hash,int block
            p.Write(0, buffer);
        }
    }
    public class PraxisPartition
    {
        public Partition part;
        private int entries_in_sector = 0;
        private int next_block = 1;
        public PraxisPartition(Partition par0) {
            byte[] block0 = par0.Read(0);
            byte flags0 = block0[36];
            if ((flags0 & 0xF0) == 0xF0) {
                part = par0;
                entries_in_sector = BitConverter.ToInt32(block0, 42);
                entries_in_sector = BitConverter.ToInt32(block0, 42);
            }
            else throw new Exception("Please format your partition with Praxis");
        }
        public bool doesHaveFile(int hash) {
            byte[] block0 = part.Read(0);
            MemBlocks mb = new MemBlocks(block0);
            for (int index = 46; index < 2048; index += 9) {
                if (mb.Read(index, 1)[0] == 0xF0 && BitConverter.ToUInt32(block0, index + 5) == hash) {
                    return true;
                }
            }
            /*if(BitConverter.ToUInt32(block0, 32) != 0) return doesHaveSecondaryFile(hash, BitConverter.ToInt32(block0, 32);
            else */return false;
        }
        public int sectorOfFile(int hash)
        {
            byte[] block0 = part.Read(0);
            MemBlocks mb = new MemBlocks(block0);
            for (int index = 46; index < 2048; index += 9)
            {
                if (mb.Read(index, 1)[0] == 0xF0 && BitConverter.ToInt32(block0, index + 1) == hash)
                {
                    return BitConverter.ToInt32(block0, index + 5);
                }
            }
            /*if(BitConverter.ToUInt32(block0, 32) != 0) return doesHaveSecondaryFile(hash, BitConverter.ToInt32(block0, 32);
            else */
            return 0;
        }
        private bool doesHaveSecondaryFile(int hash, int file) {
            byte[] block0 = part.Read(0);
            MemBlocks mb = new MemBlocks(block0);
            for (int index = 46; index < 2048; index += 9) {
                if (mb.Read(index, 1)[0] == 0xF0 && BitConverter.ToUInt32(block0, index + 5) == hash) {
                    return true;
                }
            }
            /*if(BitConverter.ToUInt32(block0, 32) != 0) return doesHaveSecondaryFile(hash, BitConverter.ToInt32(block0, 32);
            else */return false;
        }
        public void AddEntry(int sector, int hash, byte dir)
        {
            byte[] s0 = part.Read(0);
            if (BitConverter.ToInt32(s0, 32) != 0)
                AddEntrySecondary(sector, hash, dir, BitConverter.ToInt32(s0, 32));
            else { if (entries_in_sector > 222) {
                int nb = nextblock();
                inextblock();
                byte[] block = part.Read((int)nb);
                MemBlocks mb = new MemBlocks(block);
                mb.Write(new byte[] { 0 }, 0, 4);
                //Here it writes to the new block
                mb.Write(new byte[] { dir }, 4 + (9 * entries_in_sector), 1);
                mb.Write(BitConverter.GetBytes(hash), 5 + (9 * entries_in_sector), 4);
                mb.Write(BitConverter.GetBytes(sector), 9 + (9 * entries_in_sector), 4);
                entries_in_sector = 0;
                part.Write((int)nb, block);
                block = part.Read(0);
                mb = new MemBlocks(block);
                mb.Write(BitConverter.GetBytes(nb), 32, 4);
                part.Write(0, block);
                entries_in_sector++;
            } else {
                byte[] block = part.Read(0);
                MemBlocks mb = new MemBlocks(block);
                //Here it writes to the new block
                mb.Write(new byte[] { dir }, 46 + (9 * entries_in_sector), 1);
                mb.Write(BitConverter.GetBytes(hash), 47 + (9 * entries_in_sector), 4);
                mb.Write(BitConverter.GetBytes(sector), 51 + (9 * entries_in_sector), 4);
                part.Write(0, block);
                entries_in_sector++;
            } }
        }
        private void AddEntrySecondary(int sector, int hash, byte dir, int readsector)
        {
            byte[] s0 = part.Read(readsector);
            if (BitConverter.ToInt32(s0, 32) != 0)
                AddEntrySecondary(sector, hash, dir, BitConverter.ToInt32(s0, 32));
            else { if (entries_in_sector >= 227) {
                    int nb = nextblock();
                    inextblock();
            } }
        }
        public void close() {
            byte[] buffer = part.Read(0);
            var ms = new MemBlocks(buffer);
            ms.Write(BitConverter.GetBytes(entries_in_sector), 42, 4); //number of used entrys
            part.Write(0, buffer);
        }
        public int nextblock()
        {
            return next_block;
        }
        public void inextblock()
        {
            byte[] block0 = part.Read(0);
            MemBlocks mb = new MemBlocks(block0);
            byte[] nb = BitConverter.GetBytes(nextblock() + 1);
            mb.Write(nb, 38, 4);
            next_block++;
        }
        public string Label()
        {
            byte[] block0 = part.Read(0);
            string ret = "";
            for (int i = 0; i < 32; i++)
            {
                ret += (char)block0[i];
            }
            return ret.Replace(((char)0).ToString(), "");
        }
    }
    public class PraxisPartitionTable
    {
        static PraxisPartition[] table = new PraxisPartition[4];
        static int[] hashes = new int[4];
        static int index = 0;
        public static void Add(PraxisPartition part)
        {
            table[index] = part;
            //hashes[index] = Quicksilver.Impl.String.GetHashCode(part.Label());
            index++;
        }
        public static PraxisPartition Get(string label)
        {
            for (int i = 0; i < 4; i++)
                if(hashes[i] == Quicksilver.Impl.String.GetHashCode(label))
                    return table[i];
            return null;
        }
    }
}
