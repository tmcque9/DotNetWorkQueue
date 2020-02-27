﻿// ---------------------------------------------------------------------
//This file is part of DotNetWorkQueue
//Copyright © 2015-2020 Brian Lehnen
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// ---------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Threading;

namespace DotNetWorkQueue.Queue
{
    /// <summary>
    /// Waits for an action to complete
    /// </summary>
    internal static class WaitForDelegate
    {
        /// <summary>
        /// Waits for an action to complete
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Wait(Func<bool> action, TimeSpan? timeout = null)
        {
            Stopwatch timer = null;
            if (timeout.HasValue)
            {
                timer = new Stopwatch();
                timer.Start();
            }
            while (action.Invoke())
            {
                Thread.Sleep(30);
                if (timer != null && timer.Elapsed >= timeout.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
