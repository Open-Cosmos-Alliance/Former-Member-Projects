using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Apps
{
    public class AppManager
    {
        List<App> apps = new List<App>();

        public AppManager()
        {
            apps.Add(new TextEditor());
        }

        public static void Run(string appName)
        {
            if (appName == "textedit")
            {
                App tx = new TextEditor();
                tx.Initialize();
                tx.Execute();
            }
        }
    }
}
