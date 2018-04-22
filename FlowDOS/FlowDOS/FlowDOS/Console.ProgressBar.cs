using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS
{
    public static partial class Console2
    {
        public class ProgressBar
        {
            private bool flicker = true;
            private int value = 0;
            public int Value
            {
                get { return value; }
                set
                {
                    if (value >= 0 && value <= 100)
                    {
                        this.value = value;
                    }
                }
            }
            /// <summary>
            /// Initialize a new ProgressBar
            /// </summary>
            /// <param name="startValue">Value</param>
            /// <param name="Flicker">true = Very cool effect =)</param>
            public ProgressBar(int startValue, bool Flicker = false)
            {
                this.Value = startValue;
                this.flicker = Flicker;
                this.Refresh();
            }
            public void Increment()
            {
                this.Value++;
                this.Refresh();
            }
            public void Decrement()
            {
                this.Value--;
                this.Refresh();
            }
            /// <summary>
            /// INFO: MaxValue is 100 and MinValue is 0.
            /// </summary>
            /// <param name="value"></param>
            public void Draw()
            {
                int ct = System.Console.CursorTop;
                int cl = System.Console.CursorTop;
                System.Console.WriteLine();
                string buffer = "[                                                  ] ";
                System.Console.Write(buffer);
                System.Console.CursorLeft = cl + 1;
                if (flicker)
                {
                    for (int i = 0; i < this.value / 2; i++)
                    {
                        if (this.value / 2 <= 50) System.Console.Write("=");
                    }
                }
                else
                {
                    string __buffer = "";
                    for (int i = 0; i < this.value / 2; i++)
                    {
                        if (this.value / 2 <= 50) __buffer += "=";
                    }
                    System.Console.Write(__buffer);
                }
                System.Console.CursorLeft = cl + 54;
                System.Console.Write(this.value.ToString() + "%");
                System.Console.CursorTop = ct;
                System.Console.CursorLeft = cl;
            }
            private void Refresh()
            {
                this.Draw();
            }
        }
    }
}
