using System;
using System.Text;
using System.Linq;
using System.Xml;
namespace Praxis.Emulator
{
    public struct VDisk
    {
        byte[] v_cache;
        MemBlocks ms;
        const int block_size = 2048;
        public static VDisk Create(int blocks)
        {
            var vd = new VDisk();
            vd.start(blocks);
            return vd;
        }
        public void start(int blocks)
        {
            v_cache = new byte[blocks * block_size];
            ms = new MemBlocks(v_cache);
        }
        public void write(int block, byte[] content)
        {
            ms.Write(content, block * 2048, Math.Min(block_size, content.Length));
        }
        public byte[] read(int block)
        {
            byte[] buffer = new byte[block_size];
            ms.Read(ref buffer, block * 2048, block_size);
            return buffer;
        }
        public void read(int block, ref byte[] buffer) {
            ms.Read(ref buffer, block * 2048, block_size);
        }
    }
    public class Partitioner
    {
        static int offset = 0;
        public static Partition Create(PartitionTable pt, int size)
        {
            offset += size;
            return new Partition(pt.vd, offset);
        }
    }
    public class PartitionTable
    {
        public VDisk vd;
        public int part_num = 0;
        public PartitionTable(VDisk par0)
        {
            vd = par0;
        }
    }
    public class Partition
    {
        VDisk Parent; int Offset;
        public Partition(VDisk vd, int offset)
        {
            Parent = vd; Offset = offset;
        }
        public void Write(int block, byte[] content) {
            if (block < 256 && block >= 0)
            {
                Parent.write(Offset + block, content);
            }
        }
        public void Read(int block, byte[] content)
        {
            if (block < 256 && block >= 0)
            {
                Parent.read(Offset + block);
            }
        }
        public byte[] Read(int block)
        {
            if (block < 256 && block >= 0)
            {
                return Parent.read(Offset + block);
            }
            return new byte[2048];
        }
    }
}