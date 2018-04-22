using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Commands
{
    abstract class CommandBase
    {
        public bool hasShortname
        {
            get
            {
                return (shortname().Length > 0);
            }
        }

        public abstract string name();

        public virtual string shortname()
        {
            return "";
        }

        public abstract string help();
        public virtual string argFormat()
        {
            return name();
        }
        public abstract void execute(string[] args);
    }
}
