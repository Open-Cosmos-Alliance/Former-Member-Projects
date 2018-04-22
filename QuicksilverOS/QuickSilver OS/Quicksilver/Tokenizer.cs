using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver
{
    class Tokenizer
    {
        public const char split = ' ';
        public const char quote = '"';
        public static List<string> getTokens(string s) {
            bool isinquotes = false;
            List<string> tokens = new List<string> { "" };
            string temp = "";
            foreach(char c in s) {
                temp = tokens[tokens.Count - 1];
                if (c == quote) { isinquotes = !isinquotes; }
                else if (c == split && isinquotes == false) { tokens.Add(""); }
                else { tokens[tokens.Count - 1] += c; }
            }
            return tokens;
        }
    }
}
