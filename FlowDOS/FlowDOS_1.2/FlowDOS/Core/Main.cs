using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Core
{
    public class Main
    {
        public void Init()
        {
            //Core.Filesystem.Init();
            //if (Global.CurrentFS.ReadFile("/sys/hibernate.conf") != null)
            //{
            //    Power.RestoreHibernate();
            //}
            Helper.WriteInfo("FlowDOS started.");

            Users.Manager.Users.Add(new Users.User("zdimension", "abc", Users.UserType.Root));

            Global.CurrentDirectory = "/";

            Helper.WriteInfo("Please log in.");
            while (Global.CurrentUser == null)
            {
                Console.Write("Username: ");
                string u = Console.ReadLine();
                Console.Write("Password: ");
                string p = Console.ReadLine();
                if (Users.Manager.LogIn(u, p))
                {
                    IO.Out.printf("Welcome to FlowDOS %s!\n", u);
                }
                else
                {
                    Helper.WriteError("Incorrect username and/or password.");
                }
            }
            //Filesystem.Init();

            /*if (Global.CurrentFS == null)
            {
                Helper.WriteError("No valid partition found!");
                Helper.WriteInfo("System halted.");
                Cosmos.Core.Global.CPU.Halt();
            }*/

            Shell sh = new Shell();
            sh.Init();
        }
    }
}
