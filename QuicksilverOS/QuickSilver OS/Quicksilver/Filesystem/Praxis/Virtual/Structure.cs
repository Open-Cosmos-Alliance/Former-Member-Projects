/*using System;
using System.Runtime.InteropServices;
namespace Praxis {
	[StructLayout(LayoutKind.Explicit)]
	unsafe struct Header {
        [FieldOffset(0)]
        public fixed byte label[32];
		[FieldOffset(32)] 
		public int next_sector;
		[FieldOffset(36)] 
		public Int16 formatted;
		[FieldOffset(38)] 
		public int usedsectors; //total
		[FieldOffset(42)]
		public int usedentries;
        [FieldOffset(46)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 222)]
        public Entry[] entries;
	}
    [StructLayout(LayoutKind.Explicit)]
    unsafe struct ListingSecondary
    {
        [FieldOffset(0)]
        public int next_sector;
        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 227)]
        public Entry[] entries;
    }
    [StructLayout(LayoutKind.Explicit)]
    unsafe struct File
    {
        [FieldOffset(0)]
        public fixed byte name[64];
        [FieldOffset(64)]
        public int size;
        [FieldOffset(68)]
        public int next_sector;
        [FieldOffset(72)]
        public fixed byte content[1976];
    }
    [StructLayout(LayoutKind.Explicit)]
    unsafe struct Directory
    {
        [FieldOffset(0)]
        public fixed byte name[64];
        [FieldOffset(64)]
        public int usedentries;
        [FieldOffset(68)]
        public int next_sector;
        [FieldOffset(72)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 219)]
        public Entry[] entries;
    }
    [StructLayout(LayoutKind.Explicit)]
    unsafe struct DirectorySecondary
    {
        [FieldOffset(0)]
        public int next_sector;
        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 227)]
        public Entry[] entries;
    }
    [StructLayout(LayoutKind.Explicit)]
    unsafe struct FileSecondary
    {
        [FieldOffset(0)]
        public int next_sector;
        [FieldOffset(4)]
        public fixed byte name[2048];
    }
    [StructLayout(LayoutKind.Explicit)]
	struct Entry {
		[FieldOffset(0)] 
		public byte type; //0xF0 for file, 0x0F for directory
		[FieldOffset(1)] 
		public int hash;
		[FieldOffset(5)]
		public int sector;
	}
}*/