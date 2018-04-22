using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    class User
    {
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string Password { get; set; }

        public User(string name, string password, UserType type)
        {
            this.Name = name;
            this.Password = password;
            this.Type = type;
        }
    }
}
