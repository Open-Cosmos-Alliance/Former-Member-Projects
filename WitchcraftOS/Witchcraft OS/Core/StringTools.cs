using System;

namespace WitchcraftOS.Witchcraftfx.Core
{
    public static class StringTools
    {
        public static bool _StartsWith(this string __str, string __expression)
        {
            string str = "";
            for (int i = 0; i < (__expression.Length); i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }
        public static bool _EndsWith(this string __str, string __expression)
        {
            string str = "";
            for (int i = ((__str.Length - 1) - (__expression.Length - 1)); i == (__str.Length - 1); i++)
            {
                str += __str[i];
                if (str == __expression) return true;
            }
            return false;
        }
    }
}
