using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Apps
{
    class TextEditor : App
    {
        string selectedTab = "Tab 1";
        Cosmos.Hardware.TextScreen scr = new Cosmos.Hardware.TextScreen();

        public override void Initialize()
        {
            this.Name = "Text Editor";
        }

        public override void Execute()
        {
            Console.Clear();
            chnTab(1);
            FlowDOS.Hardware.Keyboard brd = new FlowDOS.Hardware.Keyboard();
            //Cosmos.Hardware.Keyboard brd;
            while (brd.Key == "")
            {
            }
            if (brd.Key == "LEFT")
            {
                if (selectedTab == "Tab 1")
                {
                }
                else
                {
                    chnTab(1);
                }
            }
            else if (brd.Key == "RIGHT")
            {
                if (selectedTab == "Tab 2")
                {
                }
                else
                {
                    chnTab(2);
                }
            }
        }

        void drawTabs()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("      Tab 1     ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("      Tab 2     ");

        }

        void chnTab(int tab)
        {
            if (tab == 1)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("      Tab 1     ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("      Tab 2     ");
                selectedTab = "Tab 1";
            }
            else if(tab == 2)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("      Tab 2     ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("      Tab 1     ");
                selectedTab = "Tab 2";
            }
            else
            {
               
            }
        }
    }
}
