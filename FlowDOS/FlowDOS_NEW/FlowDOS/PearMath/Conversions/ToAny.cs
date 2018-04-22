using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iPear.Core.PearMath.Conversions
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    class ToAnything
    {
        //Wrote by: Matt, for the PearOs team.

        public static string ConvertASCIIToDecimal(string letter)
        {
            return ((byte)letter[0]).ToString();
        }
        public static string ConvertDecimalToASCII(int number)
        {
            return ((char)number).ToString();
        }
        public static string ConvertHexToDecimal(string hex)
        {
            return FromAny(hex, 16).ToString();
        }
        public static string ConvertDecimalToHex(int number)
        {
            return ToAny(number, 16);
        }
        public static string ConvertBinaryToDecimal(string binary)
        {
            return BaseToDecimal(binary, 2).ToString();
        }
        public static string ConvertDecimalToBinary(int number)
        {
            string news = DecimalToBase(number, 2);
            return news;
        }
        public static string ConvertDecimalToBinarySpecial(int number,int width)
        {
            string news = DecimalToBase(number, 2);
            while (news.Length < width)
            {
                string test = "";
                test += "0";
                test += news;
                news = test;
            }
            return news;
        }
        public static string ConvertHexToBinary(string hex)
        {
            return ConvertDecimalToBinary(int.Parse(ConvertHexToDecimal(hex)));
        }
        public static string ConvertHexToBinarySpecial(string hex,int width)
        {
            return ConvertDecimalToBinarySpecial(int.Parse(ConvertHexToDecimal(hex)),width);
        }
        #region " Methods "
        private static string DIGITS = "0123456789ABCDEF";
        private static int FromAny(string s, int radix)
        {
            int res = 0;
            char[] sa = s.ToCharArray();
            int sign = 1;
            for (int i = 0; i < s.Length; i++)
            {
                if (sa[i] != '-')
                {
                    res = res * radix + DIGITS.IndexOf(sa[i]);
                }
                else
                {
                    sign = -1;
                }
            }
            return sign * res;
        }
        private static string ToAny(int i, int radix)
        {
            if(i >= 0)
            {
            string res = "";
            int tmp = i;
            while (tmp > 0) {
            res = DIGITS.ToCharArray()[tmp % radix] + res;
            tmp = tmp / radix;
            }
            return res;
            }
            else
            {
            return "-" + ToAny(-i, radix);
            }
         }
        
        private const int DecimalRadix = 10;
        private const int MaxBit = 16;
        private static char[] _hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static int[] _hexValues = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

       private static int BaseToDecimal(string completeHex, int radix)
        {
            int value = 0;
            int product = 1;
            for (int i = completeHex.Length - 1; i >= 0; i--, product = product * radix)
            {
                char hex = completeHex[i];
                int hexValue = -1;

                for (int j = 0; j < _hexChars.Length - 1; j++)
                {
                    if (_hexChars[j] == hex)
                    {
                        hexValue = _hexValues[j];
                        break;
                    }
                }

                value += (hexValue * product);
            }
            return value;
        }


        private static string DecimalToBase(int decimalValue, int radix)
        {
            string completeHex = "";

            int[] remainders = new int[MaxBit];
            int maxBit = MaxBit;

            for (; decimalValue > 0; decimalValue = decimalValue / radix)
            {
                maxBit = maxBit - 1;

                remainders[maxBit] = decimalValue % radix;
            }

            for (int i = 0; i < remainders.Length; i++)
            {
                int value = remainders[i];

                if (value >= DecimalRadix)
                {
                    completeHex += _hexChars[value % DecimalRadix];
                }
                else
                {
                    completeHex += value;
                }
            }

            completeHex = completeHex.TrimStart(new char[] { '0' });

            return completeHex;
        }
        #endregion
    }
}
