using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Environment
    {
        /*public static Account ActualUser { get; set; }
        public static List<Account> Accounts;

        public static void SetUser(Account acc)
        {
            ActualUser = acc;
            Accounts.Add(acc);
        }

        public static void CreateUser(Account acc)
        {
            ActualUser = acc;
            Accounts.Add(acc);
        }

        
        public static void CreateUser(string n, string pass, UserType type = UserType.Normal)
        {
            ActualUser = new Account(n, pass, type);
            Accounts.Add(new Account(n, pass, type));
        }*/
        public static string CurrentDir = "/home/root";

       // public static FlowFS.FlowFS FS = null;
    }
}
