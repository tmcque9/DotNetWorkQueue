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
namespace DotNetWorkQueue.Transport.Shared.Message
{
    /// <summary>
    /// Transport message handling actions
    /// </summary>
    /// <seealso cref="DotNetWorkQueue.Transport.Shared.ITransportHandleMessage" />
    public class TransportHandleMessage : ITransportHandleMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransportHandleMessage"/> class.
        /// </summary>
        /// <param name="commitMessage">The commit message.</param>
        /// <param name="rollbackMessage">The rollback message.</param>
        public TransportHandleMessage(ITransportCommitMessage commitMessage,
            ITransportRollbackMessage rollbackMessage)
        {
            Guard.NotNull(() => commitMessage, commitMessage);
            Guard.NotNull(() => rollbackMessage, rollbackMessage);

            RollbackMessage = rollbackMessage;
            CommitMessage = commitMessage;
        }
        /// <summary>
        /// Gets the commit message module.
        /// </summary>
        /// <value>
        /// The commit message module.
        /// </value>
        public ITransportCommitMessage CommitMessage { get; }
        /// <summary>
        /// Gets the rollback message module.
        /// </summary>
        /// <value>
        /// The rollback message module.
        /// </value>
        public ITransportRollbackMessage RollbackMessage { get; }
    }
}
