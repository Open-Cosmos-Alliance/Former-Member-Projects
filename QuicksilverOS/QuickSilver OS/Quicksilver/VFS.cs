/* Copyright (C) 2013 GruntXProductions
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDOS
{
    public class Drive
    {
        public GruntyOS.HAL.GLNFS Filesystem;
        public string DeviceFile;
    }
    public class VirtualFileSystem : GruntyOS.HAL.StorageDevice
    {
        private List<Drive> fileSystems = new List<Drive>();
        public void AddDrive(Drive fileSystem)
        {
            fileSystems.Add(fileSystem);
        }
        public GruntyOS.HAL.GLNFS getDrive(string path)
        {
            int DriveNum = (int)(((byte)path.ToCharArray()[0]) - 65);
            return fileSystems[DriveNum].Filesystem;
        }
        public string GetDeviceHandle(string path)
        {
            int DriveNum = (int)(((byte)path.ToCharArray()[0]) - 65);
            return fileSystems[DriveNum].DeviceFile;
        }
        public override void Chmod(string f, string perms)
        {
            string RealPath = f.Substring(3);
            getDrive(f).Chmod(RealPath, perms);
        }
        public override void Delete(string Path)
        {
            string RealPath = Path.Substring(3);
            getDrive(Path).Delete(RealPath);
        }
        public override void Chown(string f, string perms)
        {

            string RealPath = f.Substring(2);
            getDrive(f).Chmod(RealPath, perms);
        }
        public override GruntyOS.HAL.fsEntry[] getLongList(string dir)
        {
            string RealPath = dir.Substring(2);
            return getDrive(dir).getLongList(RealPath);
        }
        public override string[] ListDirectories(string dir)
        {
            string RealPath = dir.Substring(2);
            return getDrive(dir).ListDirectories(RealPath);
        }
        public override string[] ListFiles(string dir)
        {

            string RealPath = dir.Substring(2);

            return getDrive(dir).ListFiles(RealPath);
        }
        public override string[] ListJustFiles(string dir)
        {

            string RealPath = dir.Substring(2);
            return getDrive(dir).ListJustFiles(RealPath);
        }
        public override void makeDir(string name, string owner)
        {
            string RealPath = name.Substring(0);
            getDrive(name).makeDir(RealPath, owner);
        }
        public override void Move(string s1, string s2)
        {
            throw new NotImplementedException();
        }
        public override byte[] readFile(string name)
        {
            string RealPath = name.Substring(2);
            return getDrive(name).readFile(RealPath);
        }
        public override void saveFile(byte[] data, string name, string owner)
        {
            string RealPath = name.Substring(2);
            getDrive(name).saveFile(data, RealPath, owner);
        }
    }
}

