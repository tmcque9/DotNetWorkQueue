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
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DotNetWorkQueue.Transport.RelationalDatabase.Basic.Query
{
    /// <inheritdoc />
    /// <summary>
    /// Dequeues a message from the queue.
    /// </summary>
    public class ReceiveMessageQueryAsync<TConnection, TTransaction> : IQuery<Task<IReceivedMessageInternal>>
        where TConnection : IDbConnection
        where TTransaction : IDbTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveMessageQuery{TConnection, TTransaction}" /> class.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public ReceiveMessageQueryAsync(List<string> routes)
        {
            Connection = default(TConnection);
            Transaction = default(TTransaction);
            Routes = routes;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveMessageQuery{TConnection, TTransaction}" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="routes">The routes.</param>
        public ReceiveMessageQueryAsync(TConnection connection, TTransaction transaction, List<string> routes )
        {
            Connection = connection;
            Transaction = transaction;
            Routes = routes;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public TConnection Connection { get; }
        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        public TTransaction Transaction { get;  }
        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <value>
        /// The route.
        /// </value>
        public List<string> Routes { get; }
    }
}
