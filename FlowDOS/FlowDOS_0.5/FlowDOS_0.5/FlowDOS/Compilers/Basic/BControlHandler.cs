using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Compilers.Basic
{
    class BControlHandler
    {
        private List<BControl> Controls = new List<BControl>();

        public void AddControl(string name, BControl.type type)
        {
            BControl newc = new BControl();
            newc.Control_Name = name;
            newc.Control_Type = type;
            this.Controls.Add(newc);
        }
        public void RemoveControl(string name)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Control_Name == name)
                {
                    Controls.RemoveAt(i);
                    break;
                }
            }
        }
        public BControl ReturnControl(string name)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Control_Name == name)
                {
                    return Controls[i];
                }
            }
            return new BControl();
        }
    }
}
