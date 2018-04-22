using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Compilers.Basic
{
    class BCompiler
    {
        //Wrote by: Matt, for the PearOs team.
        private BInfo basicinfo = new BInfo();
        private BVarHandler varhandler = new BVarHandler();
        private BNext nexthandler = new BNext();
        private BControlHandler controlhandler = new BControlHandler();
        private BWhile whilehandler = new BWhile();

        public string CompileBasic(string code)
        {
            string parameters = "";
            string newcode = "";
            for (int i = 0; i < code.Split('\n').Length; i++)
            {
                string holder = code.Split('\n')[i].ToLower();
                #region " sub "
                if (holder.Substring(0, 3) == "sub")
                {
                    newcode += "#" + holder.Split(' ')[1] + ":\n";
                }
                #endregion
                #region " Dim "
                else if (holder.Substring(0, 3) == "dim" && holder.Length > 4)
                {
                    string name = holder.Split(' ')[1];
                    string type = holder.Split(' ')[3];
                    if (type == "textbox")
                    {
                        controlhandler.AddControl(name, BControl.type.TextBox);
                        newcode += "LDA " + name + "\n";
                        newcode += "LDB TEXTBOX\n";
                        newcode += "CREATE\n";
                    }
                    else if (type == "richtextbox")
                    {
                        controlhandler.AddControl(name, BControl.type.RichTextBox);
                        newcode += "LDA " + name + "\n";
                        newcode += "LDB RICHTEXTBOX\n";
                        newcode += "CREATE\n";
                    }
                    else if (type == "button")
                    {
                        controlhandler.AddControl(name, BControl.type.Button);
                    }
                    else
                    {
                        varhandler.AddVar(name, "0");
                        newcode += "LDA " + varhandler.ReturnVar(name).data + "\n";
                        newcode += "LDX " + varhandler.ReturnVar(name).index.ToString() + "\n";
                        newcode += "STA\n";
                    }

                }
                #endregion
                #region " For "
                else if (holder.Substring(0, 3) == "for")
                {
                    string value1 = "";
                    string value2 = "";
                    value1 = holder.Split(' ')[1];
                    value2 = holder.Split(' ')[3];
                    nexthandler.type = BNext.nexttype.to;
                    nexthandler.line = i + 2;
                    newcode += "LDY " + value1 + "\n";
                    newcode += "INCY\n";
                    newcode += "CMPY " + value2 + "\n";
                }
                else if (holder.Substring(0, 4) == "next")
                {
                    newcode += "JLT " + nexthandler.line.ToString() + "\n";
                }
                #endregion
                #region " Var Swapping "
                else if (holder.Length > 2 && holder.Split(' ')[1] == "=")
                {
                    string var = holder.Split(' ')[0];
                    string data = holder.Split(' ')[2];
                    newcode += "LDX " + varhandler.ReturnVar(var).index.ToString() + "\n";
                    newcode += "LDLOCA\n";
                    newcode += "MOV " + data + " TO " + "$A\n";
                    newcode += "STA\n";
                }
                #endregion
                #region " While 
                //Only support while(true)
                else if (holder.Substring(0, "while true".Length) == "while true")
                {
                    whilehandler.line = i + 1;
                    newcode += "LDY true\n";
                    newcode += "CMPY " + "true\n";
                }
                else if (holder.Substring(0, "endwhile".Length) == "endwhile")
                {
                    newcode += "JEQ " + whilehandler.line.ToString() + "\n";
                }
                else if (holder.Substring(0, "exit while".Length) == "exit while")
                {
                    newcode += "LDY false\n";
                }
                #endregion
            }
            #region " Settings "
            parameters += this.basicinfo.Company + "\n";
            parameters += this.basicinfo.Copyright + "\n";
            parameters += this.basicinfo.Version + "\n";
            parameters += this.basicinfo.MemoryTableSize + "\n";
            #endregion
            string returnc = parameters + newcode;
            return returnc;
        }

       
    }
}
