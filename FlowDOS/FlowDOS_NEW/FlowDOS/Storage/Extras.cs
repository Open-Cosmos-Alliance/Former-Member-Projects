using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntyOS.HAL;

namespace EnvyOS.Storage
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    public static class Extras
    {
        public static bool DirExists(string Dir, string Name)
        {
            string[] DirList;
            DirList = FileSystem.Root.ListDirectories(Dir);
            for (int i = 0; i < DirList.Length; i++)
            {
                if (DirList[i] == Name)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool FileExists(string Dir, string Name)
        {
            string[] FileList;
            FileList = FileSystem.Root.ListJustFiles(Dir);
            for (int i = 0; i < FileList.Length; i++)
            {
                if (FileList[i] == Name)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool SaveTextFile(string Dir, string Name, string Content)
        {
            try
            {
                if (Dir == "/")
                {
                    FileSystem.Root.saveFile(StringToByte(Content), Dir +  Name, "root");
                }
                else
                {
                    FileSystem.Root.saveFile(StringToByte(Content), Dir + "/" + Name, "root");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string OpenTextFile(string Dir, string Name)
        {
            byte[] FileContent;
            string Conversion;
            try
            {
                if (Dir == "/")
                {
                    FileContent = FileSystem.Root.readFile(Dir + Name);
                }
                else
                {
                    FileContent = FileSystem.Root.readFile(Dir + "/" + Name);
                }
                Conversion = ByteToString(FileContent);
                return Conversion;
            }
            catch
            {
                return "Error!";
            }
        }
        public static string OpenTextFile(string Path)
        {
            byte[] FileContent;
            string Conversion;
            try
            {
                
                    FileContent = FileSystem.Root.readFile(Path);
                
                Conversion = ByteToString(FileContent);
                return Conversion;
            }
            catch
            {
                return "Error!";
            }
        }
        public static string ByteToString(byte[] text)
        {
            string newtext = "";
            int i = 0;
            for (i = 0; i < text.Length; i++)
            {
                newtext += (char)text[i];
            }
            return newtext;
        }
        //The following utilities are (C) NoobOS 2012.
        public static String byteArrayToByteString(Byte[] inp)
        {
            Byte ch = 0x00;
            String ret = "";

            String[] pseudo = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				"A", "B", "C", "D", "E", "F" };

            for (int i = 0; i < inp.Length; i++)
            {
                ret += "0x";
                ch = (byte)(inp[i] & 0xF0); // Strip off high nibble
                ch = (byte)(ch >> 4); // shift the bits down
                ch = (byte)(ch & 0x0F); // must do this is high order bit is
                // on!
                ret += pseudo[(int)ch].ToString(); // convert the nibble to a
                // String Character
                ch = (byte)(inp[i] & 0x0F); // Strip off low nibble
                ret += pseudo[(int)ch].ToString(); // convert the nibble to a
                // String Character
                ret += " ";
            }
            return ret;
        }
        public static int CopyCharToByte(Char[] Data, int dind, byte[] arr, int arrind, int many)
        {
            int i = 0;
            int j = arrind;
            for (i = dind; i < many && i < Data.Length; i++)
            {
                arr[j++] = (byte)Data[i];
            }
            while (j < many)
            {
                arr[j++] = 0;
            }
            return i;
        }
        public static int CopyByteToByte(byte[] Data, int dind, byte[] arr, int arrind, int many)
        {
            int i = 0;
            int j = arrind;
            for (i = dind; i < many + dind && i < Data.Length; i++)
            {
                arr[j++] = Data[i];
            }
            while (j < many + arrind)
            {
                arr[j++] = 0;
            }
            return i;
        }
        public static int CopyByteToByte(byte[] Data, int dind, byte[] arr, int arrind, int many, bool writeallzero)
        {
            int i = 0;
            int j = arrind;
            for (i = dind; i < many + dind && i < Data.Length; i++)
            {
                arr[j++] = Data[i];
            }
            while (writeallzero && j < many + arrind)
            {
                arr[j++] = 0;
            }
            return i;
        }
        public static byte[] StringToByte(String text)
        {
            Byte[] b = new Byte[text.Length];
            CopyCharToByte(text.ToCharArray(), 0, b, 0, text.Length);
            return b;
        }
    }
}
