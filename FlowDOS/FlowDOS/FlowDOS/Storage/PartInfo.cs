using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvyOS.Storage
{
    /*
EnvyOS (Pear OS) code, Copyright (C) 2010-2013 The EnvyOS (Pear OS) Project
This code comes with ABSOLUTELY NO WARRANTY. This is free software,
and you are welcome to redistribute it under certain conditions, see
http://www.gnu.org/licenses/gpl-2.0.html for details.
*/
    public class PartInfo
    {
        public readonly byte SystemID;
        public readonly UInt32 StartSector;
        public readonly UInt32 SectorCount;

        /// <summary>
        /// Creates a new PartitionInfo
        /// </summary>
        /// <param name="aSystemID">The current partition ID</param>
        /// <param name="aStartSector">The partition start sector</param>
        /// <param name="aSectorCount">The partition number of sectors</param>
        public PartInfo(byte aSystemID, UInt32 aStartSector, UInt32 aSectorCount)
        {
            SystemID = aSystemID;
            StartSector = aStartSector;
            SectorCount = aSectorCount;
        }
    }
}
