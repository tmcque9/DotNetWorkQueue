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
using DotNetWorkQueue.Validation;

namespace DotNetWorkQueue.Transport.RelationalDatabase.Basic.Query
{
    /// <inheritdoc />
    /// <summary>
    /// Gets the current retry count for the message based on exception type
    /// </summary>
    public class GetErrorRetryCountQuery : IQuery<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetErrorRetryCountQuery"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="queueId">The queue identifier.</param>
        public GetErrorRetryCountQuery(string exceptionType, long queueId)
        {
            Guard.NotNullOrEmpty(() => exceptionType, exceptionType);
            Guard.IsValid(() => queueId, queueId, i => i > 0,
               "queueId must be greater than 0");

            ExceptionType = exceptionType;
            QueueId = queueId;
        }
        /// <summary>
        /// Gets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public string ExceptionType { get;  }
        /// <summary>
        /// Gets the queue identifier.
        /// </summary>
        /// <value>
        /// The queue identifier.
        /// </value>
        public long QueueId { get;  }
    }
}
