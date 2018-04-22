using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Applications
{
    abstract class Application
    {
        public string Name;
        public string Usage;
        public Users.User User;
        public int ID;

        public abstract void Run(string[] args, Users.User usr);
        public void CallHelp()
        {
            this.Help();
        }
        public virtual void Help()
        {
            IO.Out.printf("No help available.");
        }
    }
}
