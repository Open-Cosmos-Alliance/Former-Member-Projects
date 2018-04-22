using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Applications
{
    class keymgr : Application
    {
        public keymgr() { this.Name = "keymgr"; this.Usage = "keymgr <de|fr|[en|us]>"; }
        public override void Run(string[] args, Users.User usr)
        {
            if (args.Length == 0)
            {
                IO.Out.printf("ERROR: Missing argument\nUsage: %s\n", this.Usage);
            }
            else if (args.Length == 1)
            {
                string arg = args[0].ToLower();
                bool success = true;
                if (arg == "de" || arg == "qwertz") Drivers.Keyboard.KeyLayout.QWERTZ();
                else if (arg == "en" || arg == "us" || arg == "qwerty") Drivers.Keyboard.KeyLayout.QWERTY();
                else if (arg == "fr" || arg == "azerty") Drivers.Keyboard.KeyLayout.AZERTY();
                else success = false;
                if (success) IO.Out.printf("Changed keylayout to %s\n", arg);
                else
                {
                    IO.Out.printf("ERROR: Invalid argument \"%s\"\nUsage: %s\n", arg, this.Usage);
                }
            }
            else if (args.Length > 1)
            {
                IO.Out.printf("ERROR: Too much arguments\nUsage: %s\n", this.Usage);
            }
        }
    }
}
