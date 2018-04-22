using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Encryption
{
    class FlowCrypt
    {
        public static string CryptString(string str, string password)
        {
            if (password.Length < 8)
            {
                throw new Exception("Password too short! Must be at least 8 chars.");
            }
            string ret = "";
            int i = 0;
            g:
            if (ret.Length <= str.Length)
            {
                switch (str[i])
                {
                    #region Lower case
                    case 'a':
                        ret += (char)(((byte)password[2]) + (byte)3 + (byte)str[i]);
                        break;
                    case 'b':
                        ret += (char)(((byte)password[4]) + (byte)9 + (byte)str[i]);
                        break;
                    case 'c':
                        ret += (char)(((byte)password[5]) + (byte)1 + (byte)str[i]);
                        break;
                    case 'd':
                        ret += (char)(((byte)password[7]) + (byte)4 + (byte)str[i]);
                        break;
                    case 'e':
                        ret += (char)(((byte)password[3]) + (byte)8 + (byte)str[i]);
                        break;
                    case 'f':
                        ret += (char)(((byte)password[7]) + (byte)0 + (byte)str[i]);
                        break;
                    case 'g':
                        ret += (char)(((byte)password[0]) + (byte)1 + (byte)str[i]);
                        break;
                    case 'h':
                        ret += (char)(((byte)password[6]) + (byte)8 + (byte)str[i]);
                        break;
                    case 'i':
                        break;
                    case 'j':
                        break;
                    case 'k':
                        break;
                    case 'l':
                        break;
                    case 'm':
                        break;
                    case 'n':
                        break;
                    case 'o':
                        break;
                    case 'p':
                        break;
                    case 'q':
                        break;
                    case 'r':
                        break;
                    case 's':
                        break;
                    case 't':
                        break;
                    case 'u':
                        break;
                    case 'v':
                        break;
                    case 'w':
                        break;
                    case 'x':
                        break;
                    case 'y':
                        break;
                    case 'z':
                        break;
                    #endregion
                    #region Upper case
                    case 'A':
                        break;
                    case 'B':
                        break;
                    case 'C':
                        break;
                    case 'D':
                        break;
                    case 'E':
                        break;
                    case 'F':
                        break;
                    case 'G':
                        break;
                    case 'H':
                        break;
                    case 'I':
                        break;
                    case 'J':
                        break;
                    case 'K':
                        break;
                    case 'L':
                        break;
                    case 'M':
                        break;
                    case 'N':
                        break;
                    case 'O':
                        break;
                    case 'P':
                        break;
                    case 'Q':
                        break;
                    case 'R':
                        break;
                    case 'S':
                        break;
                    case 'T':
                        break;
                    case 'U':
                        break;
                    case 'V':
                        break;
                    case 'W':
                        break;
                    case 'X':
                        break;
                    case 'Y':
                        break;
                    case 'Z':
                        break;
                    #endregion
                    default:
                        ret += (char)(((byte)password[2]) + (byte)3 + (byte)str[i]);
                        break;
                }
            }
            goto g;
            return ret;
        }
    }
}
