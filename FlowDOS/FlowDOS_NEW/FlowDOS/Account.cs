using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        /// <summary>
        /// Create an account.
        /// </summary>
        /// <param name="nm">The user name.</param>
        /// <param name="pass">The user password.</param>
        public Account(string nm, string pass, UserType type = UserType.Normal)
        {
            this.Name = nm;
            this.Password = pass;
            this.Type = type;
        }
    }
}
public enum UserType
{
    Guest = 0,
    Normal = 1,
    Root = 2,
}