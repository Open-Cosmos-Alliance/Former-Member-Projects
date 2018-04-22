using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Filesystem.FAT
{
    unsafe class Data
    {
        struct fat32_bootsector
        {
            public fixed byte bootjmp[3];
            public fixed byte oem_name[8];
            public ushort bytes_per_sector;
            public byte sectors_per_cluster;
            public ushort reserved_sector_count;
            public byte table_count;
            public ushort root_entry_count;
            public ushort total_sectors_16;
            public byte media_type;
            public ushort table_size_16;
            public ushort sectors_per_track;
            public ushort head_side_count;
            public uint hidden_sector_count;
            public uint total_sectors_32;

            //this will be cast to it's specific type once the driver actually knows what type of FAT this is.
            public fixed byte extended_section[54];
            //extended fat12 and fat16 stuff
            public byte bios_drive_num;
            public byte reserved1;
            //extended fat32 stuff
            public uint table_size_32;
            public ushort extended_flags;
            public ushort fat_version;
            public uint root_cluster;
            public ushort fat_info;
            public ushort backup_BS_sector;
            public fixed byte reserved_0[12];
            public byte drive_number;
            public byte reserved_1;
            public byte boot_signature;
            public uint volume_id;
            public fixed byte volume_label[11];
            public fixed byte fat_type_label[8];

        }
        public class FAT32
        {
            long root_dir_sectors;
            long first_data_sector;
            long first_fat_sector;
            long data_sectors;
            long total_clusters;
            public unsafe FAT32(byte[] par0)
            {
                fixed (byte* ptr = par0)
                {
                    fat32_bootsector* fat_boot = (fat32_bootsector*)ptr;
                    root_dir_sectors = ((fat_boot->root_entry_count * 32) + (fat_boot->bytes_per_sector - 1)) / fat_boot->bytes_per_sector;
                    first_data_sector = fat_boot->reserved_sector_count + (fat_boot->table_count * fat_boot->table_size_16);
                    first_fat_sector = fat_boot->reserved_sector_count;
                    data_sectors = fat_boot->total_sectors_16 - (fat_boot->reserved_sector_count + (fat_boot->table_count * fat_boot->table_size_16) + root_dir_sectors);
                    total_clusters = data_sectors / fat_boot->sectors_per_cluster;
                }
            }
        }
    }
}
