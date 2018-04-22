using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// <value>0: Root</value>
        /// <value>1: Limited</value>
        /// </summary>
        public int Permissions { get; set; }
    }
}
