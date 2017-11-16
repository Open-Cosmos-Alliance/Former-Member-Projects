/* GlobalEnvironment.cs - manages Environment Variables
 * Copyright (C) 2012 NoobOS
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
using NoobOS.FileSystem.NoobFileSystem;

namespace NoobOS.Environment
{
    class GlobalEnvironment
    {
        /// <summary>
        /// The current Environment variable
        /// </summary>
        public static GlobalEnvironment Current = null;

        private List<EnvVariable> _Variables = new List<EnvVariable>();

        /// <summary>
        /// All the current Environment variables
        /// </summary>
        public EnvVariable[] Variables
        {
            get
            {
                return _Variables.ToArray();
            }
        }

        /// <summary>
        /// Creates a custom GlobalEnvironment object basing it on the Account
        /// </summary>
        /// <param name="acc">The Account variable to base the GlobalEnvironment to</param>
        public GlobalEnvironment(Account acc)
        {
            _Variables.Add(new EnvVariable("HOST", "NoobOS"));
            _Variables.Add(new EnvVariable("USER", acc.Username));
            _Variables.Add(new EnvVariable("CURRENTDIR", acc.HomeDir));
        }

        /// <summary>
        /// Just assing a new GlobalEnvironment object to static variable "Current"
        /// </summary>
        /// <param name="acc">Account to use</param>
        public static void Init(Account acc)
        {
            Current = new GlobalEnvironment(acc);
        }

        /// <summary>
        /// Get value from the Key passed to it
        /// </summary>
        /// <param name="str">The Key of wich get the Value</param>
        public String this[String str] 
        {
            get
            {
                for (int i = 0; i < Variables.Length; i++)
                {
                    if (Variables[i].Key == str.ToUpper())
                    {
                        return Variables[i].Value;
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < Variables.Length; i++)
                {
                    if (Variables[i].Key == str.ToUpper())
                    {
                        Variables[i].SetValue(value);
                        return;
                    }
                }
            }
        }
    }
}
