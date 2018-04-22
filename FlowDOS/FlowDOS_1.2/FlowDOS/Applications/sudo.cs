using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Applications
{
    class sudo : Application
    {
        public sudo() { this.Name = "sudo"; this.Usage = "sudo <app name> <args>"; }
        public override void Run(string[] args, Users.User usr)
        {
            if (args.Length == 0)
            {
                IO.Out.printf("ERROR: Missing argument\nUsage: %s\n", this.Usage);
            }
            else if (args.Length == 1)
            {
                string appname = args[0];
                if (args[1] != null)
                {
                    string[] args1 = args[1].Split(' ');
                    Manager.Run(appname, args1, Users.SysUsers.Root);
                }
                else
                {
                    Manager.Run(appname, null, Users.SysUsers.Root);
                }
            }
            else if (args.Length > 1)
            {
                IO.Out.printf("ERROR: Too much arguments\nUsage: %s\n", this.Usage);
            }
        }
    }
}
