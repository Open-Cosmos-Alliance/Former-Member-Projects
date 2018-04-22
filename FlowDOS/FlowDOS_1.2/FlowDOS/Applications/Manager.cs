using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Applications
{
    class Manager
    {
        public static List<Application> Apps;

        public void Init()
        {
            Apps = new List<Application>();
            Register(new keymgr());
            Register(new sudo());
        }

        public static void Register(Application app)
        {
            //app.ID = Apps.Count + 1;           
            Apps.Add(app);
        }
        
        public static bool Run(string command, string[] r, Users.User usr)
        {
            Console.WriteLine(command);
            Console.WriteLine(r);
            Console.WriteLine(usr.ToString());
            Console.WriteLine(Apps.Count);
            for (int i = 0; i < Apps.Count - 1; i++)
            {
                Console.WriteLine("test");
                Console.WriteLine(Apps[i].Name);
            }
            return false;
        }
    }
}
