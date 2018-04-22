using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.Core.GUI.Fonts.Minecraftia_AndrewTyler
{
    public class Font : Fonts.Font
    {
        public override int Width { get { return 5; } }
        public override int Height { get { return 7; } }

        #region Uppercase letters
        public uint[] A = new uint[] 
        {
            0,1,1,1,0,
            1,0,0,0,1,
            1,0,0,0,1,
            1,1,1,1,1,
            1,0,0,0,1,
            1,0,0,0,1,
            1,0,0,0,1
        };
        #endregion
    }
}
