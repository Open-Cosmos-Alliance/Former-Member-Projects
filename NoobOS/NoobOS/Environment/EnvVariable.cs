/* EnvVariable.cs - defines environment variables
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

namespace NoobOS.Environment
{
    class EnvVariable
    {
        private string _Key;
        private string _Value;

        /// <summary>
        /// The Key of the current EnvVariable
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
        }

        /// <summary>
        /// The Value of the current EnvVariable
        /// </summary>
        public string Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>
        /// Creates a new EnvVariable object with a key and value pair
        /// </summary>
        /// <param name="k">The key of the new EnvVariable</param>
        /// <param name="v">The value of the new EnvVariable</param>
        public EnvVariable(string k, string v)
        {
            this._Key = k.ToUpper();
            this._Value = v;
        }

        /// <summary>
        /// Sets value for this EnvVariable
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            this._Value = value;
        }
    }
}
