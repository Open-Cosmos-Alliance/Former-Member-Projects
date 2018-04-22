//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Quicksilver.Obfuscation
//{
//    class RandomObfuscator
//    {
//        public static byte[] decrypt(byte[] input, int seed)
//        {
//           // Quicksilver.Random r = new Quicksilver.Random(seed);
//            byte[] buffer = new byte[input.Length];
//            List<byte> rtrn = new List<byte>();
//            //r.NextBytes(buffer);
//            //int rp = 0;
//            //for (int i = 0; i < input.Length; i += 2)
//            //{
//            //    short ch = input[i + 1];
//            //    if (input[i] == 0)
//            //    {

//            //    }
//            //    else
//            //    {
//            //        ch = (short)(input[i + 1] + (256 * input[i]));
//            //    }
//            //    if (buffer[i / 2] != 0)
//            //    {
//            //        ch = (byte)(ch / buffer[i / 2]);

//            //        rtrn.Add((byte)ch);
//            //    }
//            //    rp++;
//            //}
//            //rp = 0;
//            return rtrn.ToArray();
//        }
//        public static byte[] encrypt(byte[] input, int seed)
//        {
//            //Quicksilver.Random r = new Quicksilver.Random(seed);
//            byte[] buffer = new byte[input.Length];
//            List<byte> rtrn = new List<byte>();
//            //r.NextBytes(buffer);
//            int i = 0;
//            foreach (byte b in input)
//            {
//                Int16 tmp = (Int16)(b * buffer[i]);
//                byte[] temp = GetBytes(tmp);
//                rtrn.Add(temp[0]);
//                rtrn.Add(temp[1]);
//                i++;
//            }
//            return rtrn.ToArray();

//        }
//        public static byte[] GetBytes(short input)
//        {
//            return new byte[] { (byte)(input & 0x1100), (byte)(input & 0x0011) };
//        }
//    }
//}
