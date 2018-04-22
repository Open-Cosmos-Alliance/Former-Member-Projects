using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Programs
{
    class FlowScript
    {
      
        //static Stack<object> stack = new Stack<object>();
        static List<object> stack = new List<object>();
        public static int Run(string code)
        {
            ///bool jump = false;
            //object[] variables = new object[1000000];
            //bool CALL = true;
            //il = il.Replace((ChrW(13)).ToString(), "")
            //string lbl = "";
            //int line = 0;
        Begin:
            for (int i = 0; i < code.Split('\n').Length - 1; i++)
            {
                string cmd = code.Split('\n')[i];
            //}
            //foreach (string cmd in code.Split('\n'))
            //{
                //If cmd.StartsWith(" ") Then

                
                //End If
                if (cmd.Substring(0, 6) == "push ")
                {
                    stack.Add(cmd.Substring(5));
                }
                if (cmd.Substring(0, 7) == "push$ ")
                {
                    stack.Add(cmd.Substring(6));
                }
                /*if (cmd.Substring(0, 3) == "prt")
                {
                    //stack.Pop()
                    for (int j = 0; i < stack.Count; i++)
			{
			 //Console.WriteLine(stack.ElementAt(j));
			}
                    
                    stack.Clear();
                    Console.WriteLine();
                 
                }*/
                /*if (cmd == "hlt")
                {
                    Console.Read();

                    return 0;
                }*/
                if (cmd.Substring(0, 6) == "wait ")
                {
                    //int msTime = (int)cmd.Substring(5);

                    //Time.Wait(msTime);
                    Console.WriteLine("WAIT function not implemented!");
                }
                /*if (cmd.Substring(0, 6) == "show ")
                {
                    string message1 = cmd.Substring(5);
                    bool inch = false;
                    Int32 chn = 0;
                    string message = null;
                    for (int k = 0; k < message1.ToCharArray().Length - 1; k++)
                    {
                        
                    //}
                    //foreach (char ch in message1)
                    //{
                        if (!inch)
                        {
                            if (message1[k] == '#')
                            {
                                inch = true;
                            }
                            else
                            {
                                message = message + message1[k];
                            }
                        }
                        else
                        {
                            if (char.IsNumber(message1[k]))
                            {
                                chn = chn + message1[k];
                            }
                            else if (message1[k] == '#')
                            {
                                message = message + (char)(byte)chn;
                                chn = 0;
                                inch = false;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("PROGRAM SENT:");
                    Console.WriteLine(message);
                    //MessageBox.Show("Program sent:" + '\n' + '\n' + message);
                }*/
                if (cmd == "wfk")
                {
                    Console.ReadKey();
                    //return 0;
                }
                if (cmd == "cls")
                {
                    Console.Clear();
                }
                ///Skip();
                Console.Write("");
                //line = line + 1;
            }
        return 0;
        }


        #region "Keywords"
	private static Int32 Comment = 1;
	private static Int32 Push = 2;
	private static Int32 PushString = 3;
	private static Int32 Print = 4;
	private static Int32 Halt = 5;
	private static Int32 WaitForKey = 6;
	private static Int32 ClearScreen = 7;
	private static Int32 WaitTime = 8;
	private static Int32 Show = 9;
	#endregion


        public static byte[] Compile(string code)
        {
            byte[] result = null;
            GruntyOS.IO.BinaryWriter wr = new GruntyOS.IO.BinaryWriter(new GruntyOS.IO.MemoryStream(result));
            // Mark it as FlowScript file
            wr.Write("FLOWSCRIPT");
            int abc = 0;
            for (int i = 0; i < code.Split('\n').Length - 1; i++)
            {
                string cmd = code.Split('\n')[i];
                if (cmd.Substring(0, 6) == "push ")
                {
                    wr.Write(Push);
                    if (Int32.TryParse(cmd.Substring(5), out abc))
                    {
                        wr.Write(cmd.Substring(5));
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Syntax error : trying to push a non-integer object");
                    }
                }
                if (cmd.Substring(0, 7) == "push$ ")
                {
                    wr.Write(PushString);
                    wr.Write(cmd.Substring(6));
                }
                if (cmd == "wfk")
                {
                    wr.Write(WaitForKey);
                }
                if (cmd == "cls")
                {
                    wr.Write(ClearScreen);
                }
            }
            return result;
        }
    }
}
