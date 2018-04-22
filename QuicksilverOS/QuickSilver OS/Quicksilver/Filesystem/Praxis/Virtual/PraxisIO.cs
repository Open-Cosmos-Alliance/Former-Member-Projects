using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Praxis;
using Praxis.Emulator;

namespace Praxis.IO
{
    public class File
    {
        /// <summary>
        /// Creates a new file at the specified path
        /// </summary>
        /// <param name="path">Format is %partname/folder/path/file ex. system/Users/Create.txt</param>
        /// <param name="content"></param>
        public static void Create(string path, byte[] content)
        {
            string[] paths = path.Split('/');
            PraxisPartition p = PraxisPartitionTable.Get(paths[1]);
            if (p != null) {
                int sectors = p.nextblock();
                for (int i = 2; i < paths.Length; i++) {
                    if (paths[i] == "" && i != paths.Length - 1) {

                    }
                    if(i == paths.Length - 1) {
                        p.AddEntry((int)sectors, (int)Quicksilver.Impl.String.GetHashCode(paths[2]), 0xF0);
                        WriteFile(paths[2], (int)sectors, content, p);
                    }
                }
            }
        }
        private static void WriteFile(string name, Int32 block, byte[] content, PraxisPartition prp)
        {
            int num_of_sectors = 0, tmp_len = content.Length - 1976;
            while (tmp_len > 0) {
                tmp_len -= 2044;
                num_of_sectors++;
            }
            byte[][] blocks = new byte[num_of_sectors + 1][];
            int[] sectors = new int[num_of_sectors + 1];
            for(int i = 0; i < blocks.Length; i++) blocks[i] = new byte[2048];
            int x = 0;
            //first block
            sectors[0] = prp.nextblock();
            var mb = new MemBlocks(blocks[0]);
            mb.Write(Encoding.UTF8.GetBytes(name), 0, 64);
            mb.Write(BitConverter.GetBytes(content.Length), 64, 4);
            mb.Write(BitConverter.GetBytes(sectors[0]), 68, 4);
            prp.inextblock();
            for (int i = 72; i < 2048; i++) { blocks[0][i] = content[x]; if (x >= content.Length - 1) break; x++; }
            if (num_of_sectors > 0) for (int i = 1; i < blocks.Length; i = Math.Min(blocks.Length - 1, i + 1)) { sectors[i] = prp.nextblock(); mb = new MemBlocks(blocks[i]); mb.Write(BitConverter.GetBytes(sectors[i]), 0, 4); for (int j = 4; j < blocks[i].Length; j++) { blocks[i][j] = content[x]; x++; if (x >= content.Length - 1) break; } if (x >= content.Length - 1) break; prp.inextblock(); }
            for (int i = 0; i < blocks.Length; i++ ) if(sectors[i] != 0) prp.part.Write(sectors[i], blocks[i]);
            #region garbagecode
            /*byte[][] blocks = new byte[num_of_sectors + 2][];
            for (int i = 0; i < blocks.Length; i++)
                blocks[i] = new byte[2048]
            ;
            var ms = new MemBlocks(blocks[0]);
            ms.Write(Encoding.UTF8.GetBytes(name), 0, 64);
            ms.Write(BitConverter.GetBytes(content.Length), 64, 4);
            if (num_of_sectors == 0) ms.Write(BitConverter.GetBytes(0), 68, 4);
            else { prp.inextblock(); ms.Write(BitConverter.GetBytes(prp.nextblock()), 68, 4); }
            byte[] temp = new byte[1976];
            for (int i = 0; i < Math.Min(1976, content.Length); i++) temp[i] = content[i];
            ms.Write(temp, 72, 1976);
            prp.part.Write(block, blocks[0]);
            prp.inextblock();
            for (int i = 0; i <= num_of_sectors; i++)
            {
                ms = new MemBlocks(blocks[i + 1]);
                int old_next = (int)prp.nextblock();
                byte[] tmp = new byte[2044];
                for (int j = 0; j < Math.Min(1976, content.Length); j++) tmp[j] = content[j + (i * 2044)];
                ms.Write(tmp, 4, 2044);
                prp.inextblock();
                if (i == num_of_sectors - 1) ms.Write(BitConverter.GetBytes(0), 0, 4);
                else ms.Write(BitConverter.GetBytes(prp.nextblock()), 0, 4);
                prp.part.Write(old_next, tmp);
            }*/
            #endregion
            //1976 is the bytes in the first sector. 2044 in the later ones.
        }
        private static byte[] ReadFile(string name, Int32 block, PraxisPartition prp)
        {
            byte[] sec0 = prp.part.Read(block);
            int length = BitConverter.ToInt32(sec0, 64);
            int num_of_sectors = 0, tmp_len = length - 1976;
            while (tmp_len > 0)
            {
                tmp_len -= 2044;
                num_of_sectors++;
            }
            byte[][] blocks = new byte[num_of_sectors + 1][];
            byte[] lasttime = new byte[2048];
            new MemBlocks(lasttime).Write(BitConverter.GetBytes(block), 68, 4);
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = prp.part.Read(BitConverter.ToInt32(lasttime, 68));
                lasttime = blocks[i];
            }
            byte[] returns = new byte[length];
            int x = 0;
            for (int i = 72; i < blocks[0].Length; i++) { returns[x] = blocks[0][i]; x++; if (x >= length) break; }
            if (num_of_sectors > 0) for (int i = 1; i < blocks.Length; i++) { for (int j = 4; j < blocks[i].Length; j++) { returns[x] = blocks[i][j]; x++; if (x >= length) break; } if (x >= length) break; }
            return returns;
        }
        /*public static void EditFile(string path, byte[] content)
        {
            string[] paths = path.Split('/');
            PraxisPartition p = PraxisPartitionTable.Get(paths[0]);
            if (p != null) {
                uint sectors = p.nextblock();
                for (int i = 1; i < paths.Length; i++) {
                    if (paths[i] == "" && i != paths.Length - 1) {

                    }
                    if (i == paths.Length - 1) {
                        p.AddEntry((int)sectors, paths[i].GetHashCode(), 0xF0);
                        WriteFile(paths[1], (int)sectors, content, p);
                    }
                }
            }
        }*/
        public static byte[] Read(string path) {
            string[] paths = path.Split('/');
            PraxisPartition p = PraxisPartitionTable.Get(paths[1]);
            if (p != null) {
                int sectors = 0;
                for (int i = 1; i < paths.Length; i++) {
                    if (paths[i] == "" && i != paths.Length - 1) {

                    }
                    if (i == paths.Length - 1) {// && p.doesHaveFile(paths[2].GetHashCode())) {
                        sectors = p.sectorOfFile((int)Quicksilver.Impl.String.GetHashCode((paths[2])));
                        return ReadFile(paths[2], sectors, p);
                    }
                }
            }
            return new byte[1];
        }
        public static byte[] get(string partition, int sector)
        {
            var x = PraxisPartitionTable.Get(partition).part.Read(sector);
            return x;
        }
    }
}
