﻿using System.Data;
using DotNetWorkQueue.Transport.RelationalDatabase;
using DotNetWorkQueue.Transport.RelationalDatabase.Basic.Query;
using DotNetWorkQueue.Transport.SQLite.Shared.Basic;
using DotNetWorkQueue.Validation;

namespace DotNetWorkQueue.Transport.SQLite.Shared.Decorator
{
    /// <summary>
    /// 
    /// </summary>
    public class DoesJobExistDecorator : IQueryHandler<DoesJobExistQuery<IDbConnection, IDbTransaction>, QueueStatuses>
    {
        private readonly IConnectionInformation _connectionInformation;
        private readonly IQueryHandler<DoesJobExistQuery<IDbConnection, IDbTransaction>, QueueStatuses> _decorated;
        private readonly DatabaseExists _databaseExists;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetColumnNamesFromTableDecorator" /> class.
        /// </summary>
        /// <param name="connectionInformation">The connection information.</param>
        /// <param name="decorated">The decorated.</param>
        /// <param name="databaseExists">The database exists.</param>
        public DoesJobExistDecorator(IConnectionInformation connectionInformation,
            IQueryHandler<DoesJobExistQuery<IDbConnection, IDbTransaction>, QueueStatuses> decorated,
            DatabaseExists databaseExists)
        {
            Guard.NotNull(() => decorated, decorated);
            Guard.NotNull(() => connectionInformation, connectionInformation);
            Guard.NotNull(() => databaseExists, databaseExists);
            _connectionInformation = connectionInformation;
            _decorated = decorated;
            _databaseExists = databaseExists;
        }

        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public QueueStatuses Handle(DoesJobExistQuery<IDbConnection, IDbTransaction> query)
        {
            return !_databaseExists.Exists(_connectionInformation.ConnectionString) ? QueueStatuses.NotQueued : _decorated.Handle(query);
        }
    }
}
