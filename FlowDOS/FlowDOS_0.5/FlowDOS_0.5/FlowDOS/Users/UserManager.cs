using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    class UserManager
    {
        public static List<User> users = new List<User>();

        public static bool LoggedIn = false;

        public static int Init()
        {
            string usrs1;
            string[] usrs;
            usrs1 = FS.ReadFile("/sys/passwd");
            if (!string.IsNullOrWhiteSpace(usrs1))
            {
                usrs = usrs1.Split('\n');

                foreach (string s in usrs)
                {
                    string nm;
                    string pass;
                    if (!s.Contains(':'))
                    {
                        return 3;
                    }
                    else
                    {
                        if (s.Length != 1)
                        {
                            nm = s.Split(':')[0];
                            pass = s.Split(':')[1];


                            users.Add(new User() { Name = nm, Password = pass });
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }

                return 0;
            }
            else
            {
                return 2;
            }
            return 1;
        }

        public static int Login(string username, string passwd)
        {
            bool exists = false;
            foreach (User u in users)
            {
                if (u.Name == username)
                {
                    exists = true;
                    if (u.Password == passwd)
                    {
                        Environment.CurrentUser = u;
                        GruntyOS.CurrentUser.Username = u.Name;
                        GruntyOS.CurrentUser.Privilages = (byte)u.Permissions;
                        Kernel.CurrentUser = u.Name;
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            if (!exists)
            {
                return 1;
            }
            return 1;
        }

        public static int CreateAccount(string name, string pass)
        {
            foreach (User u in users)
            {
                if (u.Name == name)
                {
                    return 2;
                }
                else
                {

                }
            }
            return 1;
        }

        
    }
}
