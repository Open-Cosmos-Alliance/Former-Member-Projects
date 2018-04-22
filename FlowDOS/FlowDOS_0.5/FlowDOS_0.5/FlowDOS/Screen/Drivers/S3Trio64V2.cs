using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iPear.Core.Screen.Drivers
{
    /*
     * This driver was taken from MOSA and translated into COSMOS compatible code for the Pear OS Project
     * (C) PearOS and MOSA 2012
     * */
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    class S3Trio64V2
    {
        internal struct Register
        {
            internal const ushort VgaEnable = 0x13;
            internal const ushort MiscOutRead = 0x1c;
            internal const ushort MiscOutWrite = 0x12;
            internal const ushort CrtcIndex = 0x24;
            internal const ushort CrtcData = 0x25;
            internal const ushort SequenceIndex = 0x14;
            internal const ushort SequenceData = 0x15;
        }
        internal struct CommandRegister
        {
            internal const ushort CursorBytes = 2084;
            internal const ushort AdvFuncCntl = 0x4738;
        }
        internal enum DisplayModeState
        {
            /// <summary>
            /// Display is turned on
            /// </summary>
            On = 0x00,
            /// <summary>
            /// Display is on standby
            /// </summary>
            StandBy = 0x10,
            /// <summary>
            /// Display is in suspend mode
            /// </summary>
            Suspend = 0x40,
            /// <summary>
            /// Display is turned off
            /// </summary>
            Off = 0x50,
            /// <summary>
            /// Used when current modestate is unknown
            /// </summary>
            Unknown = 0xFF,
        }
        #region Members
        protected Cosmos.Core.MemoryBlock memory;
        // Cannot seem to find COSMOS equivalent for
        // protected IFrameBuffer frameBuffer;
        protected Cosmos.Core.IOPort lfbControllerIndex;
        protected Cosmos.Core.IOPort enhMapControllerIndex;
        protected Cosmos.Core.IOPort vgaEnableController;
        protected Cosmos.Core.IOPort miscOutputReader;
        protected Cosmos.Core.IOPort miscOutputWriter;
        protected Cosmos.Core.IOPort crtcControllerIndex;
        protected Cosmos.Core.IOPort crtcControllerData;
        // IOPort to index sequence registers
        protected Cosmos.Core.IOPort seqControllerIndex;
        // IOPort to write data to the sequence registers
        protected Cosmos.Core.IOPort seqControllerData;
        // THE PORT BELOW IS WRITE-ONLY
        protected Cosmos.Core.IOPort outportWrite;
        #endregion
        #region Properties,PixelPaletteGraphicsDevice
        // Gets the size of the palette
        // Returns: The size of the palette
        public ushort PaletteSize
        {
            get
            {
                return 0;
            }
        }
        // Gets the width
        public ushort Width
        {
            get
            {
                return 0;
            }
        }
        // Gets the height
        public ushort Height
        {
            get
            {
                return 0;
            }
        }
        #endregion
        #region Construction
        public S3Trio64V2()
        {
        }
        #endregion
        #region Hardware Device
        //Sets up this hardware device driver.
        public bool Setup()
        {
            byte portBar = 0;

            vgaEnableController = new Cosmos.Core.IOPort(Register.VgaEnable, portBar);
            miscOutputReader = new Cosmos.Core.IOPort(Register.MiscOutRead, portBar);
            miscOutputWriter = new Cosmos.Core.IOPort(Register.MiscOutWrite, portBar);
            crtcControllerIndex = new Cosmos.Core.IOPort(Register.CrtcIndex, portBar);
            crtcControllerData = new Cosmos.Core.IOPort(Register.CrtcData, portBar);
            seqControllerIndex = new Cosmos.Core.IOPort(Register.SequenceIndex, portBar);
            seqControllerData = new Cosmos.Core.IOPort(Register.SequenceData, portBar);
            // Everything went well
            return true;
        }
        public bool Start()
        {
            
            //Everything went well
            return true;
        }
        #endregion
    }
}
