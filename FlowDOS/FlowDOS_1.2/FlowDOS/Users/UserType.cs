using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Users
{
    public enum UserType
    {
        User = 1,
        Root = 2,
        Admin = 4,
        Guest = 8
    }
    public class UserTypeExtensions
    {
        public static UserType GetTypeFromInt(int i)
        {
            return (UserType)i;
        }
    }
}
