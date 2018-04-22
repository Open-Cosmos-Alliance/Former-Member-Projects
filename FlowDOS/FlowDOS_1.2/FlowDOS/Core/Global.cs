using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    class Global
    {
        public static Users.User CurrentUser;

        public static string CurrentDirectory;

        public static IO.Filesystem.Filesystem CurrentFS;
    }
}
