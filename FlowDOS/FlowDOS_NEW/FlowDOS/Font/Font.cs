using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Font
{
    class Font
    {
        public static void DrawText(/*ref Cosmos.Hardware.VGAScreen sc, */int x, int y, string text, int color = 1)
        {

            for (int i = 0; i <= text.Length - 1; i++)
            {
                switch (Char.ToUpper(text[i]))
                {
                    case 'A':
                        FlowDOS.Kernel.DrawFrameColor(Chars.A, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'B':
                        FlowDOS.Kernel.DrawFrameColor(Chars.B, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'C':
                        FlowDOS.Kernel.DrawFrameColor(Chars.C, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'D':
                        FlowDOS.Kernel.DrawFrameColor(Chars.D, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'E':
                        FlowDOS.Kernel.DrawFrameColor(Chars.E, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'F':
                        FlowDOS.Kernel.DrawFrameColor(Chars.F, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'G':
                        FlowDOS.Kernel.DrawFrameColor(Chars.G, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'H':
                        FlowDOS.Kernel.DrawFrameColor(Chars.H, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'I':
                        FlowDOS.Kernel.DrawFrameColor(Chars.I, 1, 7, x, y, color);
                        x = x + 2;
                        break;
                    case 'J':
                        FlowDOS.Kernel.DrawFrameColor(Chars.J, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'K':
                        FlowDOS.Kernel.DrawFrameColor(Chars.K, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'L':
                        FlowDOS.Kernel.DrawFrameColor(Chars.L, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'M':
                        FlowDOS.Kernel.DrawFrameColor(Chars.M, 5, 7, x, y, color);
                        x = x + 6;
                        break;
                    case 'N':
                        FlowDOS.Kernel.DrawFrameColor(Chars.N, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'O':
                        FlowDOS.Kernel.DrawFrameColor(Chars.O, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'P':
                        FlowDOS.Kernel.DrawFrameColor(Chars.P, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'Q':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Q, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'R':
                        FlowDOS.Kernel.DrawFrameColor(Chars.R, 4, 7, x, y, color);
                        x = x + 6;
                        break;
                    case 'S':
                        FlowDOS.Kernel.DrawFrameColor(Chars.S, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'T':
                        FlowDOS.Kernel.DrawFrameColor(Chars.T, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'U':
                        FlowDOS.Kernel.DrawFrameColor(Chars.U, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'V':
                        FlowDOS.Kernel.DrawFrameColor(Chars.V, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'W':
                        FlowDOS.Kernel.DrawFrameColor(Chars.W, 5, 7, x, y, color);
                        x = x + 6;
                        break;
                    case 'X':
                        FlowDOS.Kernel.DrawFrameColor(Chars.X, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'Y':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Y, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case 'Z':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Z, 4, 7, x, y, color);
                        x = x + 5;
                        break;

                    case '0':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Zero, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '1':
                        FlowDOS.Kernel.DrawFrameColor(Chars.One, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '2':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Two, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '3':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Three, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '4':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Four, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '5':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Five, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '6':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Six, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '7':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Seven, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '8':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Eight, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '9':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Nine, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case ':':
                        FlowDOS.Kernel.DrawFrameColor(Chars.TwoPoints, 1, 7, x, y, color);
                        x = x + 2;
                        break;
                    case '/':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Slash, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case '\\':
                        FlowDOS.Kernel.DrawFrameColor(Chars.AntiSlash, 4, 7, x, y, color);
                        x = x + 5;
                        break;
                    case ' ':
                        FlowDOS.Kernel.DrawFrameColor(Chars.Space, 1, 7, x, y, color);
                        x = x + 2;
                        break; 
                }
            }
        }
    }
}
