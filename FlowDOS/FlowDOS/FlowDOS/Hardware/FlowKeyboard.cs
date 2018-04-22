using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Hardware
{
    public  class Keyboard
    {

        /*
         * Wrote By: Matt, For the PearOs team.
         */
        private ConsoleKey i = ConsoleKey.A;
        public string Key = "";
        private Cosmos.Hardware.Keyboard TheKeyboard = new Cosmos.Hardware.Keyboard();
        #region " Keyboard Handlers "
        public void Update()
        {
            this.Key = GetkeyPress();
        }
        public void ClearUpdate()
        {
            this.Key = "";
            return;
        }
        public string GetkeyPress()
        {
            TheKeyboard.GetKey(out i);
            switch (i)
            {
                case ConsoleKey.A:
                    return "a";
                case ConsoleKey.B:
                    return "b";
                case ConsoleKey.C:
                    return "c";
                case ConsoleKey.D:
                    return "d";
                case ConsoleKey.E:
                    return "e";
                case ConsoleKey.F:
                    return "f";
                case ConsoleKey.G:
                    return "g";
                case ConsoleKey.H:
                    return "h";
                case ConsoleKey.I:
                    return "i";
                case ConsoleKey.J:
                    return "j";
                case ConsoleKey.K:
                    return "k";
                case ConsoleKey.L:
                    return "l";
                case ConsoleKey.M:
                    return "m";
                case ConsoleKey.N:
                    return "n";
                case ConsoleKey.O:
                    return "o";
                case ConsoleKey.P:
                    return "p";
                case ConsoleKey.Q:
                    return "q";
                case ConsoleKey.R:
                    return "r";
                case ConsoleKey.S:
                    return "s";
                case ConsoleKey.T:
                    return "t";
                case ConsoleKey.U:
                    return "u";
                case ConsoleKey.V:
                    return "v";
                case ConsoleKey.W:
                    return "w";
                case ConsoleKey.X:
                    return "x";
                case ConsoleKey.Y:
                    return "y";
                case ConsoleKey.Z:
                    return "z";
                case ConsoleKey.Backspace:
                    return "DEL";
                case ConsoleKey.UpArrow:
                    return "UP";
                case ConsoleKey.DownArrow:
                    return "DOWN";
                case ConsoleKey.Spacebar:
                    return " ";
                case ConsoleKey.Enter:
                    return "ENTER";
                case ConsoleKey.LeftArrow:
                    return "LEFT";
                case ConsoleKey.RightArrow:
                    return "RIGHT";
                case ConsoleKey.Escape:
                    return "ESC";
            }
            return "";
        }
        public int ShiftKey()
        {
            if (TheKeyboard.ShiftPressed == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int ControlKey()
        {
            if (TheKeyboard.CtrlPressed == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AltKey()
        {
            if (TheKeyboard.AltPressed == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
