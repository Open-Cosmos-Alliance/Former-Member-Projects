using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Font
{
    class Chars
    {
        #region Letters
       public static uint[] A = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,1,1,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1
    };

       public static uint[] B = new uint[] {
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,1,1,0
    };

       public static uint[] C = new uint[] {
            0,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            0,1,1,1
    };

       public static uint[] D = new uint[] {
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,1,1,0
    };

       public static uint[] E = new uint[] {
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,1,1,1
    };

       public static uint[] F = new uint[] {
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0
    };

       public static uint[] G = new uint[] {
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,1,1,
            1,0,0,1,
            1,1,1,1
    };

       public static uint[] H = new uint[] {
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,1,1,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1
    };

       public static uint[] I = new uint[] {
            1,
            1,
            1,
            1,
            1,
            1,
            1
    };

        public static uint[] J = new uint[] {
            1,1,1,1,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            1,1,1,0
    };

        public static uint[] K = new uint[] {
            1,0,0,1,
            1,0,1,0,
            1,1,0,0,
            1,1,0,0,
            1,1,0,0,
            1,0,1,0,
            1,0,0,1
    };

        public static uint[] L = new uint[] {
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0,
            1,1,1,1
    };

        public static uint[] M = new uint[] {
            1,0,0,0,1,
            1,1,0,1,1,
            1,0,1,0,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1
    };

        public static uint[] N = new uint[] {
            1,0,0,1,
            1,1,0,1,
            1,0,1,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1
    };

        public static uint[] O = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] P = new uint[] {
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,1,1,0,
            1,0,0,0,
            1,0,0,0,
            1,0,0,0
    };

        public static uint[] Q = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,1,0,
            0,1,0,1
    };

        public static uint[] R = new uint[] {
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1
    };

        public static uint[] S = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,0,
            0,1,1,0,
            0,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] T = new uint[] {
            1,1,1,1,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0
    };

        public static uint[] U = new uint[] {
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] V = new uint[] {
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,1,1,1,
            0,1,1,0
    };

        public static uint[] W = new uint[] {
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,1,0,1,
            1,1,1,1,1,
            0,1,0,1,0
    };

        public static uint[] X = new uint[] {
            1,0,0,1,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            0,1,1,0,
            1,0,0,1
    };

        public static uint[] Y = new uint[] {
            1,0,0,1,
            0,1,0,1,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,0,0
    };

        public static uint[] Z = new uint[] {
            1,1,1,1,
            0,0,0,1,
            0,0,1,0,
            0,1,1,0,
            1,0,0,0,
            1,0,0,0,
            1,1,1,1
    };
        #endregion

        #region Numbers
        public static uint[] Zero = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] One = new uint[] {
            0,0,1,0,
            0,1,1,0,
            1,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            1,1,1,1
    };

        public static uint[] Two = new uint[] {
            0,1,1,0,
            1,0,0,1,
            0,0,0,1,
            0,0,1,0,
            0,1,0,0,
            1,0,0,0,
            1,1,1,1
    };

        public static uint[] Three = new uint[] {
            1,1,1,1,
            0,0,0,1,
            0,0,1,0,
            0,1,1,0,
            0,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] Four = new uint[] {
            0,0,1,0,
            0,1,0,0,
            1,0,1,0,
            1,1,1,1,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0
    };

        public static uint[] Five = new uint[] {
            1,1,1,1,
            1,0,0,0,
            1,0,0,0,
            1,1,1,0,
            0,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] Six = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,0,
            1,1,1,0,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] Seven = new uint[] {
            1,1,1,1,
            0,0,0,1,
            0,0,0,1,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0,
            0,0,1,0
    };

        public static uint[] Eight = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0
    };

        public static uint[] Nine = new uint[] {
            0,1,1,0,
            1,0,0,1,
            1,0,0,1,
            0,1,1,0,
            0,0,0,1,
            1,0,0,1,
            0,1,1,0
    };
        #endregion

        #region Symbols
        public static uint[] TwoPoints = new uint[] {
            0,
            0,
            1,
            0,
            1,
            0,
            0
    };
        public static uint[] Slash = new uint[] {
            0,0,0,1,
            0,0,1,0,
            0,0,1,0,
            0,1,0,0,
            0,1,0,0,
            1,0,0,0,
            1,0,0,0
    };
        public static uint[] AntiSlash = new uint[] {
            1,0,0,0,
            0,1,0,0,
            0,1,0,0,
            0,0,1,0,
            0,0,1,0,
            0,0,0,1,
            0,0,0,1
    };
        public static uint[] SpaceOld = new uint[] {
            0,0,0,0,
            0,0,0,0,
            0,0,0,0,
            0,0,0,0,
            0,0,0,0,
            0,0,0,0,
            0,0,0,0
    };
        public static uint[] Space = new uint[] {
            0,
            0,
            0,
            0,
            0,
            0,
            0
    };
        #endregion

    }
}
