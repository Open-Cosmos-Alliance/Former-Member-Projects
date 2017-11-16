/* Extensions.cs - useful utilities for NoobOS
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
#region Namespace Imports

using System;
using System.Collections.Generic;

#endregion

namespace NoobOS
{
	/// <summary>
	/// Defines extension methods to be used across the system.
	/// </summary>
	public static class Extensions
	{
		#region String Utils

		/// <summary>
		/// Splits the string at the index where first white space is found.
		/// </summary>
		/// <param name="source">the source string</param>
		/// <returns>splitted string array.</returns>
		public static String[] SplitAtFirstSpace(this String source)
		{
			if (String.IsNullOrEmpty(source)) return new String[] { };

			var index = source.IndexOf(' ');

			if (index == -1) return new String[] { source };

			return new String[] { source.Substring(0, index), source.Substring(index + 1) };
		}

		/// <summary>
		/// Compares two instances of string.
		/// </summary>
		/// <param name="source">the source string</param>
		/// <param name="target">the other instance.</param>
		/// <returns>true if the two instances are considered equal, false otherwise.</returns>
		public static bool IsEqual(this String source, String target)
		{
			if (source == null && target == null) return true; // both instances are null.

			if (String.IsNullOrEmpty(source) && String.IsNullOrEmpty(target)) return true; // both instances are empty.

			return source.ToLower() == target.ToLower();
		}

		#endregion

		#region Collection Utils

		/// <summary>
		/// Applies the specified action for every element in the source collection.
		/// </summary>
		/// <typeparam name="T">type of element collection</typeparam>
		/// <param name="source">source collection</param>
		/// <param name="action">action to be invoked on each element</param>
		public static void ForEeach(this Array source, Action<Object> action)
		{
			var enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				action.Invoke(enumerator.Current);
			}
		}

		#endregion
	}
}
