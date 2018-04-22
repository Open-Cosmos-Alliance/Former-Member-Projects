using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchcraftOS.Witchcraftfx.GUI
{
    public static class Font
    {
        //Declare all the font uint[]

        public static uint[] a = new uint[8];
        public static uint[] b = new uint[8];
        public static uint[] c = new uint[8];
        public static uint[] d = new uint[8];
        public static uint[] e = new uint[8];
        public static uint[] f = new uint[8];
        public static uint[] g = new uint[8];
        public static uint[] h = new uint[8];
        public static uint[] i = new uint[8];
        public static uint[] j = new uint[8];
        public static uint[] k = new uint[8];
        public static uint[] l = new uint[8];
        public static uint[] m = new uint[8];
        public static uint[] n = new uint[8];
        public static uint[] o = new uint[8];
        public static uint[] p = new uint[8];
        public static uint[] q = new uint[8];
        public static uint[] r = new uint[8];
        public static uint[] s = new uint[8];
        public static uint[] t = new uint[8];
        public static uint[] u = new uint[8];
        public static uint[] v = new uint[8];
        public static uint[] w = new uint[8];
        public static uint[] sx = new uint[8];
        public static uint[] sy = new uint[8];
        public static uint[] z = new uint[8];
        public static uint[] n0 = new uint[8]; //remember the "n" before the number!
        public static uint[] n1 = new uint[8];
        public static uint[] n2 = new uint[8];
        public static uint[] n3 = new uint[8];
        public static uint[] n4 = new uint[8];
        public static uint[] n5 = new uint[8];
        public static uint[] n6 = new uint[8];
        public static uint[] n7 = new uint[8];
        public static uint[] n8 = new uint[8];
        public static uint[] n9 = new uint[8];
        public static uint[] colon = new uint[8];
        public static uint[] semicolon = new uint[8];
        public static uint[] slash = new uint[8];
        public static uint[] quote = new uint[8]; //what are these called? btw Im not english.
        public static uint[] backslash = new uint[8];
        public static uint[] arrowright = new uint[8];
        public static uint[] arrowleft = new uint[8];
        public static uint[] space = new uint[8];
        public static uint[] comma = new uint[8];
        public static uint[] dot = new uint[8];
        public static uint[] excla = new uint[8];
        public static uint[] icon = new uint[10];
        public static uint wx, wy;


        public static void SetupFont()
        {
            #region load font
            //Set all the font variables
            a[0] = 2222222;
            a[1] = 2222222;
            a[2] = 2211222;
            a[3] = 2122122;
            a[4] = 2122122;
            a[5] = 2122122;
            a[6] = 2211212;
            a[7] = 2222222;
            a[8] = 2222222;



            b[0] = 2222222;
            b[1] = 2122222;
            b[2] = 2111222;
            b[3] = 2122122;
            b[4] = 2122122;
            b[5] = 2122122;
            b[6] = 2111222;
            b[7] = 2222222;
            b[8] = 2222222;

            c[0] = 2222222;
            c[1] = 2222222;
            c[2] = 2221122;
            c[3] = 2212222;
            c[4] = 2212222;
            c[5] = 2212222;
            c[6] = 2221122;
            c[7] = 2222222;
            c[8] = 2222222;


            d[0] = 2222222;
            d[1] = 2222122;
            d[2] = 2211122;
            d[3] = 2122122;
            d[4] = 2122122;
            d[5] = 2122122;
            d[6] = 2211122;
            d[7] = 2222222;
            d[8] = 2222222;



            e[0] = 2222222;
            e[1] = 2222222;
            e[2] = 2211222;
            e[3] = 2122122;
            e[4] = 2111122;
            e[5] = 2122222;
            e[6] = 2211222;
            e[7] = 2222222;
            e[8] = 2222222;


            f[0] = 2222222;
            f[1] = 2221222;
            f[2] = 2212222;
            f[3] = 2211222;
            f[4] = 2212222;
            f[5] = 2212222;
            f[6] = 2212222;
            f[7] = 2222222;
            f[8] = 2222222;

            g[0] = 2222222;
            g[1] = 2211222;
            g[2] = 2122122;
            g[3] = 2122122;
            g[4] = 2211122;
            g[5] = 2222122;
            g[6] = 2222122;
            g[7] = 2122122;
            g[8] = 2211222;


            h[0] = 2222222;
            h[1] = 2122222;
            h[2] = 2122222;
            h[3] = 2111222;
            h[4] = 2122122;
            h[5] = 2122122;
            h[6] = 2122122;
            h[7] = 2222222;
            h[8] = 2222222;

            i[0] = 2222222;
            i[1] = 2221222;
            i[2] = 2222222;
            i[3] = 2221222;
            i[4] = 2221222;
            i[5] = 2221222;
            i[6] = 2221222;
            i[7] = 2222222;
            i[8] = 2222222;


            j[0] = 2222222;
            j[1] = 2222122;
            j[2] = 2222222;
            j[3] = 2222122;
            j[4] = 2222122;
            j[5] = 2222122;
            j[6] = 2222122;
            j[7] = 2122122;
            j[8] = 2211222;



            k[0] = 2222222;
            k[1] = 2212222;
            k[2] = 2212222;
            k[3] = 2212122;
            k[4] = 2211222;
            k[5] = 2212122;
            k[6] = 2212122;
            k[7] = 2222222;
            k[8] = 2222222;


            l[0] = 2222222;
            l[1] = 2221222;
            l[2] = 2221222;
            l[3] = 2221222;
            l[4] = 2221222;
            l[5] = 2221222;
            l[6] = 2221222;
            l[7] = 2222222;
            l[8] = 2222222;


            m[0] = 2222222;
            m[1] = 2222222;
            m[2] = 2112122;
            m[3] = 2121212;
            m[4] = 2121212;
            m[5] = 2121212;
            m[6] = 2121212;
            m[7] = 2222222;
            m[8] = 2222222;

            n[0] = 2222222;
            n[1] = 2222222;
            n[2] = 2111222;
            n[3] = 2122122;
            n[4] = 2122122;
            n[5] = 2122122;
            n[6] = 2122122;
            n[7] = 2222222;
            n[8] = 2222222;

            o[0] = 2222222;
            o[1] = 2222222;
            o[2] = 2211222;
            o[3] = 2122122;
            o[4] = 2122122;
            o[5] = 2122122;
            o[6] = 2211222;
            o[7] = 2222222;
            o[8] = 2222222;

            p[0] = 2222222;
            p[1] = 2222222;
            p[2] = 2211222;
            p[3] = 2122122;
            p[4] = 2122122;
            p[5] = 2122122;
            p[6] = 2111222;
            p[7] = 2122222;
            p[8] = 2122222;


            q[0] = 2222222;
            q[1] = 2222222;
            q[2] = 2211122;
            q[3] = 2122122;
            q[4] = 2122122;
            q[5] = 2211122;
            q[6] = 2222122;
            q[7] = 2221112;
            q[8] = 2222122;

            r[0] = 2222222;
            r[1] = 2222222;
            r[2] = 2212122;
            r[3] = 2211222;
            r[4] = 2212222;
            r[5] = 2212222;
            r[6] = 2212222;
            r[7] = 2222222;
            r[8] = 2222222;

            s[0] = 2222222;
            s[1] = 2222222;
            s[2] = 2211122;
            s[3] = 2122222;
            s[4] = 2211222;
            s[5] = 2222122;
            s[6] = 2111222;
            s[7] = 2222222;
            s[8] = 2222222;

            t[0] = 2222222;
            t[1] = 2221222;
            t[2] = 2221222;
            t[3] = 2211122;
            t[4] = 2221222;
            t[5] = 2221222;
            t[6] = 2221222;
            t[7] = 2222222;
            t[8] = 2222222;

            u[0] = 2222222;
            u[1] = 2222222;
            u[2] = 2122122;
            u[3] = 2122122;
            u[4] = 2122122;
            u[5] = 2122122;
            u[6] = 2211222;
            u[7] = 2222222;
            u[8] = 2222222;

            v[0] = 2222222;
            v[1] = 2222222;
            v[2] = 2212122;
            v[3] = 2212122;
            v[4] = 2212122;
            v[5] = 2212122;
            v[6] = 2221222;
            v[7] = 2222222;
            v[8] = 2222222;


            w[0] = 2222222;
            w[1] = 2222222;
            w[2] = 2122212;
            w[3] = 2122212;
            w[4] = 2122212;
            w[5] = 2121212;
            w[6] = 2212122;
            w[7] = 2222222;
            w[8] = 2222222;


            sx[0] = 2222222;
            sx[1] = 2222222;
            sx[2] = 2212122;
            sx[3] = 2212122;
            sx[4] = 2221222;
            sx[5] = 2212122;
            sx[6] = 2212122;
            sx[7] = 2222222;
            sx[8] = 2222222;


            sy[0] = 2222222;
            sy[1] = 2222222;
            sy[2] = 2122122;
            sy[3] = 2122122;
            sy[4] = 2211122;
            sy[5] = 2222122;
            sy[6] = 2111222;
            sy[7] = 2222222;
            sy[8] = 2222222;


            z[0] = 2222222;
            z[1] = 2222222;
            z[2] = 2111122;
            z[3] = 2222122;
            z[4] = 2211222;
            z[5] = 2122222;
            z[6] = 2111122;
            z[7] = 2222222;
            z[8] = 2222222;



            space[0] = 2222222;
            space[1] = 2222222;
            space[2] = 2222222;
            space[3] = 2222222;
            space[4] = 2222222;
            space[5] = 2222222;
            space[6] = 2222222;
            space[7] = 2222222;
            space[8] = 2222222;



            comma[0] = 2222222;
            comma[1] = 2222222;
            comma[2] = 2222222;
            comma[3] = 2222222;
            comma[4] = 2222222;
            comma[5] = 2222222;
            comma[6] = 2221222;
            comma[7] = 2212222;
            comma[8] = 2222222;


            dot[0] = 2222222;
            dot[1] = 2222222;
            dot[2] = 2222222;
            dot[3] = 2222222;
            dot[4] = 2222222;
            dot[5] = 2222222;
            dot[6] = 2221222;
            dot[7] = 2222222;
            dot[8] = 2222222;

            excla[0] = 2222222;
            excla[1] = 2221222;
            excla[2] = 2221222;
            excla[3] = 2221222;
            excla[4] = 2221222;
            excla[5] = 2222222;
            excla[6] = 2221222;
            excla[7] = 2222222;
            excla[8] = 2222222;

            n0[0] = 2222222;
            n0[1] = 2211122;
            n0[2] = 2122212;
            n0[3] = 2112212;
            n0[4] = 2121212;
            n0[5] = 2122112;
            n0[6] = 2211122;
            n0[7] = 2222222;
            n0[8] = 2222222;

            n1[0] = 2222222;
            n1[1] = 2221222;
            n1[2] = 2211222;
            n1[3] = 2221222;
            n1[4] = 2221222;
            n1[5] = 2221222;
            n1[6] = 2211122;
            n1[7] = 2222222;
            n1[8] = 2222222;

            n2[0] = 2222222;
            n2[1] = 2211122;
            n2[2] = 2122212;
            n2[3] = 2222122;
            n2[4] = 2221222;
            n2[5] = 2212222;
            n2[6] = 2111112;
            n2[7] = 2222222;
            n2[8] = 2222222;


            n3[0] = 2222222;
            n3[1] = 2111222;
            n3[2] = 2222122;
            n3[3] = 2111222;
            n3[4] = 2222122;
            n3[5] = 2222122;
            n3[6] = 2111222;
            n3[7] = 2222222;
            n3[8] = 2222222;

            n4[0] = 2222222;
            n4[1] = 2121222;
            n4[2] = 2121222;
            n4[3] = 2121222;
            n4[4] = 2111222;
            n4[5] = 2221222;
            n4[6] = 2221222;
            n4[7] = 2222222;
            n4[8] = 2222222;


            n5[0] = 2222222;
            n5[1] = 2111122;
            n5[2] = 2122222;
            n5[3] = 2122222;
            n5[4] = 2111222;
            n5[5] = 2222122;
            n5[6] = 2111222;
            n5[7] = 2222222;
            n5[8] = 2222222;

            n6[0] = 2222222;
            n6[1] = 2211222;
            n6[2] = 2122122;
            n6[3] = 2122222;
            n6[4] = 2111222;
            n6[5] = 2122122;
            n6[6] = 2211222;
            n6[7] = 2222222;
            n6[8] = 2222222;

            n7[0] = 2222222;
            n7[1] = 2111122;
            n7[2] = 2222122;
            n7[3] = 2221222;
            n7[4] = 2212222;
            n7[5] = 2212222;
            n7[6] = 2212222;
            n7[7] = 2222222;
            n7[8] = 2222222;


            n8[0] = 2222222;
            n8[1] = 2211222;
            n8[2] = 2122122;
            n8[3] = 2211222;
            n8[4] = 2122122;
            n8[5] = 2122122;
            n8[6] = 2211222;
            n8[7] = 2222222;
            n8[8] = 2222222;

            n9[0] = 2222222;
            n9[1] = 2211222;
            n9[2] = 2122122;
            n9[3] = 2122122;
            n9[4] = 2211122;
            n9[5] = 2222122;
            n9[6] = 2222122;
            n9[7] = 2222222;
            n9[8] = 2222222;

            slash[0] = 2222212;
            slash[1] = 2222212;
            slash[2] = 2222122;
            slash[3] = 2222122;
            slash[4] = 2221222;
            slash[5] = 2212222;
            slash[6] = 2212222;
            slash[7] = 2122222;
            slash[8] = 2122222;



            backslash[0] = 2122222;
            backslash[1] = 2122222;
            backslash[2] = 2212222;
            backslash[3] = 2212222;
            backslash[4] = 2221222;
            backslash[5] = 2222122;
            backslash[6] = 2222122;
            backslash[7] = 2222212;
            backslash[8] = 2222212;


            arrowright[0] = 2222222;
            arrowright[1] = 2222222;
            arrowright[2] = 2212222;
            arrowright[3] = 2221222;
            arrowright[4] = 2222122;
            arrowright[5] = 2221222;
            arrowright[6] = 2212222;
            arrowright[7] = 2222222;
            arrowright[8] = 2222222;

            arrowleft[0] = 2222222;
            arrowleft[1] = 2222222;
            arrowleft[2] = 2222122;
            arrowleft[3] = 2221222;
            arrowleft[4] = 2212222;
            arrowleft[5] = 2221222;
            arrowleft[6] = 2222122;
            arrowleft[7] = 2222222;
            arrowleft[8] = 2222222;


            colon[0] = 2222222;
            colon[1] = 2222222;
            colon[2] = 2222222;
            colon[3] = 2221222;
            colon[4] = 2222222;
            colon[5] = 2222222;
            colon[6] = 2221222;
            colon[7] = 2222222;
            colon[8] = 2222222;

            semicolon[0] = 2222222;
            semicolon[1] = 2222222;
            semicolon[2] = 2222222;
            semicolon[3] = 2221222;
            semicolon[4] = 2222222;
            semicolon[5] = 2222222;
            semicolon[6] = 2221222;
            semicolon[7] = 2212222;
            semicolon[8] = 2222222;

            quote[0] = 2212122;
            quote[1] = 2212122;
            quote[2] = 2222222;
            quote[3] = 2222222;
            quote[4] = 2222222;
            quote[5] = 2222222;
            quote[6] = 2222222;
            quote[7] = 2222222;
            quote[8] = 2222222;

            icon[0] = 2222211222;
            icon[1] = 2222112222;
            icon[2] = 2222122222;
            icon[3] = 2211211222;
            icon[4] = 2111111122;
            icon[5] = 1111111112;
            icon[6] = 1111111222;
            icon[7] = 1111111222;
            icon[8] = 1111111112;
            icon[9] = 2111111122;
            icon[10] = 2211211222;
            #endregion
        }

        public static void drawText(string text, uint x, uint y, uint color, ref Cosmos.Hardware.VGAScreen screen)
        {

            for (int count = 0; count <= text.Length; count++)
            {
                drawLetter(text.Substring(count, 1), x, y, color, ref screen);
                x += 7;

            }
        }
        public static void drawText(string text, uint x, uint y, uint color, ref Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII screen)
        {

            for (int count = 0; count <= text.Length; count++)
            {
                drawLetter(text.Substring(count, 1), (ushort)x, (ushort)y, color, ref screen);
                x += 7;

            }
        }

        public static void drawLetter(string letter, uint x, uint y, uint color, ref Cosmos.Hardware.VGAScreen screen)
        {

            if (letter.Length != 1)
            {

            }
            else
            {

                //Do the magic
                if (letter == " ")
                {
                    drawArray(space, x, y, color, ref screen);
                }
                else if (letter == "/")
                {
                    drawArray(slash, x, y, color, ref screen);
                }
                else if (letter == @"""")
                {
                    drawArray(quote, x, y, color, ref screen);
                }
                else if (letter == @"\")
                {
                    drawArray(backslash, x, y, color, ref screen);
                }
                else if (letter == ":")
                {
                    drawArray(colon, x, y, color, ref screen);
                }
                else if (letter == ";")
                {
                    drawArray(semicolon, x, y, color, ref screen);
                }
                else if (letter == ">")
                {
                    drawArray(arrowright, x, y, color, ref screen);
                }
                else if (letter == "<")
                {
                    drawArray(arrowleft, x, y, color, ref screen);
                }
                else if (letter == ",")
                {
                    drawArray(comma, x, y, color, ref screen);
                }
                else if (letter == ".")
                {
                    drawArray(dot, x, y, color, ref screen);
                }
                else if (letter == "!")
                {
                    drawArray(excla, x, y, color, ref screen);
                }
                else if (letter == "a")
                {
                    drawArray(a, x, y, color, ref screen);
                }
                else if (letter == "b")
                {
                    drawArray(b, x, y, color, ref screen);
                }
                else if (letter == "c")
                {
                    drawArray(c, x, y, color, ref screen);
                }
                else if (letter == "d")
                {
                    drawArray(d, x, y, color, ref screen);
                }
                else if (letter == "e")
                {
                    drawArray(e, x, y, color, ref screen);
                }
                else if (letter == "f")
                {
                    drawArray(f, x, y, color, ref screen);
                }
                else if (letter == "g")
                {
                    drawArray(g, x, y, color, ref screen);
                }
                else if (letter == "h")
                {
                    drawArray(h, x, y, color, ref screen);
                }
                else if (letter == "i")
                {
                    drawArray(i, x, y, color, ref screen);
                }
                else if (letter == "j")
                {
                    drawArray(j, x, y, color, ref screen);
                }
                else if (letter == "k")
                {
                    drawArray(k, x, y, color, ref screen);
                }
                else if (letter == "l")
                {
                    drawArray(l, x, y, color, ref screen);
                }
                else if (letter == "m")
                {
                    drawArray(m, x, y, color, ref screen);
                }
                else if (letter == "n")
                {
                    drawArray(n, x, y, color, ref screen);
                }
                else if (letter == "o")
                {
                    drawArray(o, x, y, color, ref screen);
                }
                else if (letter == "p")
                {
                    drawArray(p, x, y, color, ref screen);
                }
                else if (letter == "q")
                {
                    drawArray(q, x, y, color, ref screen);
                }
                else if (letter == "r")
                {
                    drawArray(r, x, y, color, ref screen);
                }
                else if (letter == "s")
                {
                    drawArray(s, x, y, color, ref screen);
                }
                else if (letter == "t")
                {
                    drawArray(t, x, y, color, ref screen);
                }
                else if (letter == "u")
                {
                    drawArray(u, x, y, color, ref screen);
                }
                else if (letter == "v")
                {
                    drawArray(v, x, y, color, ref screen);
                }
                else if (letter == "w")
                {
                    drawArray(w, x, y, color, ref screen);
                }
                else if (letter == "x")
                {
                    drawArray(sx, x, y, color, ref screen);
                }
                else if (letter == "y")
                {
                    drawArray(sy, x, y, color, ref screen);
                }
                else if (letter == "z")
                {
                    drawArray(z, x, y, color, ref screen);
                }
                else if (letter == "0")
                {
                    drawArray(n0, x, y, color, ref screen);
                }
                else if (letter == "1")
                {
                    drawArray(n1, x, y, color, ref screen);
                }
                else if (letter == "2")
                {
                    drawArray(n2, x, y, color, ref screen);
                }
                else if (letter == "3")
                {
                    drawArray(n3, x, y, color, ref screen);
                }
                else if (letter == "4")
                {
                    drawArray(n4, x, y, color, ref screen);
                }
                else if (letter == "5")
                {
                    drawArray(n5, x, y, color, ref screen);
                }
                else if (letter == "6")
                {
                    drawArray(n6, x, y, color, ref screen);
                }
                else if (letter == "7")
                {
                    drawArray(n7, x, y, color, ref screen);
                }
                else if (letter == "8")
                {
                    drawArray(n8, x, y, color, ref screen);
                }
                else if (letter == "9")
                {
                    drawArray(n9, x, y, color, ref screen);
                }
            }
        }
        public static void drawLetter(string letter, ushort x, ushort y, uint color, ref Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII screen)
        {

            if (letter.Length != 1)
            {

            }
            else
            {

                //Do the magic
                if (letter == " ")
                {
                    drawArray(space, x, y, color, ref screen);
                }
                else if (letter == "/")
                {
                    drawArray(slash, x, y, color, ref screen);
                }
                else if (letter == @"""")
                {
                    drawArray(quote, x, y, color, ref screen);
                }
                else if (letter == @"\")
                {
                    drawArray(backslash, x, y, color, ref screen);
                }
                else if (letter == ":")
                {
                    drawArray(colon, x, y, color, ref screen);
                }
                else if (letter == ";")
                {
                    drawArray(semicolon, x, y, color, ref screen);
                }
                else if (letter == ">")
                {
                    drawArray(arrowright, x, y, color, ref screen);
                }
                else if (letter == "<")
                {
                    drawArray(arrowleft, x, y, color, ref screen);
                }
                else if (letter == ",")
                {
                    drawArray(comma, x, y, color, ref screen);
                }
                else if (letter == ".")
                {
                    drawArray(dot, x, y, color, ref screen);
                }
                else if (letter == "!")
                {
                    drawArray(excla, x, y, color, ref screen);
                }
                else if (letter == "a")
                {
                    drawArray(a, x, y, color, ref screen);
                }
                else if (letter == "b")
                {
                    drawArray(b, x, y, color, ref screen);
                }
                else if (letter == "c")
                {
                    drawArray(c, x, y, color, ref screen);
                }
                else if (letter == "d")
                {
                    drawArray(d, x, y, color, ref screen);
                }
                else if (letter == "e")
                {
                    drawArray(e, x, y, color, ref screen);
                }
                else if (letter == "f")
                {
                    drawArray(f, x, y, color, ref screen);
                }
                else if (letter == "g")
                {
                    drawArray(g, x, y, color, ref screen);
                }
                else if (letter == "h")
                {
                    drawArray(h, x, y, color, ref screen);
                }
                else if (letter == "i")
                {
                    drawArray(i, x, y, color, ref screen);
                }
                else if (letter == "j")
                {
                    drawArray(j, x, y, color, ref screen);
                }
                else if (letter == "k")
                {
                    drawArray(k, x, y, color, ref screen);
                }
                else if (letter == "l")
                {
                    drawArray(l, x, y, color, ref screen);
                }
                else if (letter == "m")
                {
                    drawArray(m, x, y, color, ref screen);
                }
                else if (letter == "n")
                {
                    drawArray(n, x, y, color, ref screen);
                }
                else if (letter == "o")
                {
                    drawArray(o, x, y, color, ref screen);
                }
                else if (letter == "p")
                {
                    drawArray(p, x, y, color, ref screen);
                }
                else if (letter == "q")
                {
                    drawArray(q, x, y, color, ref screen);
                }
                else if (letter == "r")
                {
                    drawArray(r, x, y, color, ref screen);
                }
                else if (letter == "s")
                {
                    drawArray(s, x, y, color, ref screen);
                }
                else if (letter == "t")
                {
                    drawArray(t, x, y, color, ref screen);
                }
                else if (letter == "u")
                {
                    drawArray(u, x, y, color, ref screen);
                }
                else if (letter == "v")
                {
                    drawArray(v, x, y, color, ref screen);
                }
                else if (letter == "w")
                {
                    drawArray(w, x, y, color, ref screen);
                }
                else if (letter == "x")
                {
                    drawArray(sx, x, y, color, ref screen);
                }
                else if (letter == "y")
                {
                    drawArray(sy, x, y, color, ref screen);
                }
                else if (letter == "z")
                {
                    drawArray(z, x, y, color, ref screen);
                }
                else if (letter == "0")
                {
                    drawArray(n0, x, y, color, ref screen);
                }
                else if (letter == "1")
                {
                    drawArray(n1, x, y, color, ref screen);
                }
                else if (letter == "2")
                {
                    drawArray(n2, x, y, color, ref screen);
                }
                else if (letter == "3")
                {
                    drawArray(n3, x, y, color, ref screen);
                }
                else if (letter == "4")
                {
                    drawArray(n4, x, y, color, ref screen);
                }
                else if (letter == "5")
                {
                    drawArray(n5, x, y, color, ref screen);
                }
                else if (letter == "6")
                {
                    drawArray(n6, x, y, color, ref screen);
                }
                else if (letter == "7")
                {
                    drawArray(n7, x, y, color, ref screen);
                }
                else if (letter == "8")
                {
                    drawArray(n8, x, y, color, ref screen);
                }
                else if (letter == "9")
                {
                    drawArray(n9, x, y, color, ref screen);
                }
            }
        }

        public static void drawArray(uint[] letter, uint x, uint y, uint color, ref Cosmos.Hardware.VGAScreen screen)
        {
            for (int i = 0; i <= letter.Length; i++) //This is the Y
            {

                for (int j = 0; j <= letter.GetLength(0); j++) //This is X
                {
                    if (letter[i].ToString().Substring(j, 1) == "1")
                    {
                        screen.SetPixel320x200x8(x + (uint)j, y + (uint)i, color);
                    }
                    else if (letter[i].ToString().Substring(j, 1) == "2")
                    {

                    }
                    else
                    {
                        break;
                    }

                }

            }
        }
        public static void drawArray(uint[] letter, ushort x, ushort y, uint color, ref Cosmos.Hardware.Drivers.PCI.Video.VMWareSVGAII screen)
        {
            for (ushort i = 0; i <= letter.Length; i++) //This is the Y
            {

                for (ushort j = 0; j <= letter.GetLength(0); j++) //This is X
                {
                    if (letter[i].ToString().Substring(j, 1) == "1")
                    {
                        screen.SetPixel((ushort)((uint)x + (uint)j), (ushort)((uint)y + (uint)i), color);
                    }
                    else if (letter[i].ToString().Substring(j, 1) == "2")
                    {

                    }
                    else
                    {
                        break;
                    }

                }

            }
        }
    }



    public static class font_old
    {
        public static void DrawWord(string word, uint X, uint Y)
        {
            uint translateX = 0;
            foreach (char str in word.ToLower())
            {
                DrawChar(str, X + translateX, Y);
                translateX += 6;
            }
        }
        public static void DrawChar(char c, uint X, uint Y)
        {
            /*
            if (c == '.')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '0')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '1')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '2')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '3')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '4')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '5')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '6')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '7')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '8')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == '9')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'a')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'b')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'c')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'd')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'e')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'f')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'g')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'h')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'i')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'j')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'k')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'l')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'm') // ? ^^
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'n')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'o')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'p')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'q')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 255);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'r')
            {
                // Gemacht mit CosmosFontCreator
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 's')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 't')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'u')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'v') // ? ^^
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'w') // ? ^^
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 0);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'x')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 255);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            else if (c == 'y')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 0);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 255);
                screen.SetPixel320x200x8(X + 0, Y + 4, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 0);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 255);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 0);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 255);
            }
            else if (c == 'z')
            {
                screen.SetPixel320x200x8(X + 0, Y + 0, 0);
                screen.SetPixel320x200x8(X + 0, Y + 1, 255);
                screen.SetPixel320x200x8(X + 0, Y + 2, 255);
                screen.SetPixel320x200x8(X + 0, Y + 3, 0);
                screen.SetPixel320x200x8(X + 0, Y + 4, 0);
                screen.SetPixel320x200x8(X + 1, Y + 0, 0);
                screen.SetPixel320x200x8(X + 1, Y + 2, 255);
                screen.SetPixel320x200x8(X + 1, Y + 2, 0);
                screen.SetPixel320x200x8(X + 1, Y + 3, 255);
                screen.SetPixel320x200x8(X + 1, Y + 4, 0);
                screen.SetPixel320x200x8(X + 2, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 255);
                screen.SetPixel320x200x8(X + 2, Y + 2, 0);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
                screen.SetPixel320x200x8(X + 3, Y + 0, 0);
                screen.SetPixel320x200x8(X + 2, Y + 1, 0);
                screen.SetPixel320x200x8(X + 2, Y + 2, 255);
                screen.SetPixel320x200x8(X + 2, Y + 3, 255);
                screen.SetPixel320x200x8(X + 2, Y + 4, 0);
            }
            */
        }
    }
}
