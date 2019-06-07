﻿using System;
using DotNetWorkQueue.Transport.PostgreSQL.Schema;

namespace DotNetWorkQueue.Transport.PostgreSQL.Basic
{
    /// <inheritdoc />
    public class PostgreSqlJobQueueCreation : IJobQueueCreation
    {
        private readonly PostgreSqlMessageQueueCreation _queueCreation;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSqlJobQueueCreation"/> class.
        /// </summary>
        /// <param name="queueCreation">The queue creation.</param>
        public PostgreSqlJobQueueCreation(PostgreSqlMessageQueueCreation queueCreation)
        {
            _queueCreation = queueCreation;
        }
        /// <inheritdoc />
        public bool IsDisposed => _queueCreation.IsDisposed;

        /// <inheritdoc />
        public ICreationScope Scope => _queueCreation.Scope;

        /// <summary>
        /// Gets or sets the options for the queue transport.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public PostgreSqlMessageQueueTransportOptions Options => _queueCreation.Options;

        /// <inheritdoc />
        public QueueCreationResult CreateJobSchedulerQueue(Action<IContainer> registerService, string queue, string connection, Action<IContainer> setOptions = null, bool enableRoute = false)
        {
            if (_queueCreation.Options.AdditionalColumns.Count == 0)
            {
                _queueCreation.Options.AdditionalColumns.Add(new Column("JobName", ColumnTypes.Varchar, 255, false));
                var constraint = new Constraint($"IX_{queue}JobName", ConstraintType.Constraint,
                    "JobName") {Unique = true};
                _queueCreation.Options.AdditionalConstraints.Add(constraint);
            }
            if (enableRoute)
            {
                _queueCreation.Options.EnableRoute = true;
            }
            return _queueCreation.CreateQueue();
        }

        /// <inheritdoc />
        public QueueRemoveResult RemoveQueue()
        {
            return _queueCreation.RemoveQueue();
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _queueCreation.Dispose();
                }
                _disposedValue = true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
