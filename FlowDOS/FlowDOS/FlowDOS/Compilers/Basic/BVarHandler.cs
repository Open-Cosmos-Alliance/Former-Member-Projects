using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Compilers.Basic
{
    class BVarHandler
    {
        //Wrote By: Matt, For the PearOs team.

        private List<BVar> vars = new List<BVar>();
        private int index = 0;
        private int i;
        public void AddVar(string name, string data)
        {
            BVar newvar = new BVar();
            newvar.name = name;
            newvar.data = data;
            newvar.index = this.index;
            this.vars.Add(newvar);
            this.index += 1;
        }

        public void RemoveVar(string name)
        {
            for (i = 0; i < this.vars.Count; i++)
            {
                if (this.vars[i].name == name)
                {
                    this.vars.RemoveAt(i);
                    break;
                }
            }
        }

        public BVar ReturnVar(string name)
        {
            
            for (i = 0; i < this.vars.Count; i++)
            {
                if (this.vars[i].name == name)
                {
                    return this.vars[i];
                }
            }
            return new BVar();
        }
    }
}
