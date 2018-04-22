using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    class SysUsers
    {
        public static User Root = new User("root", "root", UserType.Root);
    }
}
