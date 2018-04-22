using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class FS
    {
        /* removed, throws an CPU 0x06
        public static bool DirExists(string Dir, string Name)
        {
            string[] DirList;
            DirList = Kernel.fd.ListDirectories(Dir);
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
            FileList = Kernel.fd.ListJustFiles(Dir);
            for (int i = 0; i < FileList.Length; i++)
            {
                if (FileList[i] == Name)
                {
                    return true;
                }
            }
            return false;
        }*/

        public static void AddFile(string Content, string Name, string Dir)
        {
            if (Dir[Dir.Length] == '/')
            {
                Kernel.fd.saveFile(StringToByte(Content), Dir + Name, GruntyOS.CurrentUser.Username);
            }
            else
            {
Kernel.fd.saveFile(StringToByte(Content), Dir + "/" + Name, "");
            }
            //EnvyOS.Storage.Extras.SaveTextFile(Dir, Name, Content);
        }

        public static void AddFile(byte[] Content, string Name, string Dir)
        {
            if (Dir[Dir.Length] == '/')
            {
                Kernel.fd.saveFile(Content, Dir + Name, "");
            }
            else
            {
                Kernel.fd.saveFile(Content, Dir + "/" + Name, GruntyOS.CurrentUser.Username);
            }
            //EnvyOS.Storage.Extras.SaveTextFile(Dir, Name, ByteToString(Content));
        }

        public static string ReadFile(string Path)
        {
            return ByteToString(Kernel.fd.readFile(Path));
            //return EnvyOS.Storage.Extras.OpenTextFile(Path);
        }
        #region Convert functions
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
        #endregion
        
    }
}
