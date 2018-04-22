using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    class Manager
    {
        public static List<User> Users = new List<User>();

        public static void Init()
        {
            string a = Helper.GetString(Global.CurrentFS.ReadFile("/sys/conf/passwd"));
            string[] lines = a.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] abc = lines[i].Split(';');
                string user = abc[0];
                string password = abc[1];
                password = Helper.GetString(new Crypto.Base64Decoder(Helper.GetCharArrayFromString(password)).GetDecoded());
                password = Crypto.MD5Crypt.Decrypt(password, Helper.GetStringFromCharArray((new Crypto.Base64Encoder(Helper.GetBytes(user)).GetEncoded())), true);
                UserType type = UserTypeExtensions.GetTypeFromInt(int.Parse(abc[2]));
                Users.Add(new User(user, password, type));
            }
        }

        public static bool LogIn(string user, string password)
        {
            bool b = false;
            for (int i = 0; i < Users.Count; i++)
            {
                string user1 = Users[i].Name;
                string password1 = Users[i].Password;
                if (user == user1 && password == password1)
                {
                    Global.CurrentUser = Users[i];
                    b = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return b;
        }
    }
}
