using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.IO
{
    class Out
    {
        public enum replacetype : byte { _string = 0, _int, _double, _char, _percentchar }
        private static int argreplaces, i, pos;
        private static string strBefore, strReplace, strAfter;
        private static List<int> replacepositions = new List<int>();
        private static List<replacetype> replacetypes = new List<replacetype>();
        private static string[] replaceparts;
        public static void printf(string str, params object[] args)
        {
            argreplaces = 0;
            replacepositions.Clear();
            replacetypes.Clear();

            for (i = 0; i < str.Length; ++i)
            {
                if (str[i] == '%')
                {
                    int j = i + 1;
                    if (str[j] == 's')
                    {
                        argreplaces++;
                        replacepositions.Add(i);
                        replacetypes.Add(replacetype._string);
                    }
                    else if (str[j] == 'c')
                    {
                        argreplaces++;
                        replacepositions.Add(i);
                        replacetypes.Add(replacetype._char);
                    }
                    else if (str[j] == 'i')
                    {
                        argreplaces++;
                        replacepositions.Add(i);
                        replacetypes.Add(replacetype._int);
                    }
                    else if (str[j] == 'd' || str[j] == 'f')
                    {
                        argreplaces++;
                        replacepositions.Add(i);
                        replacetypes.Add(replacetype._double);
                    }
                    else if (str[j] == '%')
                    {
                        replacepositions.Add(i);
                        replacetypes.Add(replacetype._percentchar);
                    }
                    ++i;
                }
            }
            if (args.Length != argreplaces)
            {
                printf("Error: You defined %i arguments, but there are %i arguments\n", argreplaces, args.Length);
                return;
            }

            replaceparts = new string[replacepositions.Count];
            for (i = 0; i < replacepositions.Count; i++)
            {
                if (replacetypes[i] == replacetype._string)
                {
                    if (args[i] is string) replaceparts[i] = args[i].ToString();
                }
                else if (replacetypes[i] == replacetype._int)
                {
                    if (args[i] is byte || args[i] is short || args[i] is ushort ||
                        args[i] is int || args[i] is uint || args[i] is UInt16 ||
                        args[i] is UInt32 || args[i] is UInt64) replaceparts[i] = args[i].ToString();
                }
                else if (replacetypes[i] == replacetype._double)
                {
                    if (args[i] is double || args[i] is float || args[i] is long ||
                        args[i] is ulong || args[i] is Single) replaceparts[i] = args[i].ToString();
                }
                else if (replacetypes[i] == replacetype._char)
                {
                    if (args[i] is char || args[i] is byte) replaceparts[i] = ((char)args[i]).ToString();
                }
                else if (replacetypes[i] == replacetype._percentchar)
                {
                    replaceparts[i] = "%";
                }
            }

            pos = 0;
            int count = 0;
            for (i = 0; i < replaceparts.Length; i++)
            {
                pos = replacepositions[i];
                if (count > 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        pos += replaceparts[i - (j + 1)].Length - 2;
                    }
                    //pos += replaceparts[i - 1].Length - 2;
                }
                strBefore = str.Substring(0, pos);
                strReplace = replaceparts[i];
                strAfter = str.Substring(pos + 2);
                str = strBefore + strReplace + strAfter;
                count++;
            }

            Console.Write(str);
        }
    }
}
