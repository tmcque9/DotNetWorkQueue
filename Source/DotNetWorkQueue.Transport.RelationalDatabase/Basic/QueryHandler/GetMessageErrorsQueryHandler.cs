﻿using System;
using System.Collections.Generic;
using System.Text;
using DotNetWorkQueue.Transport.RelationalDatabase.Basic.Query;
using DotNetWorkQueue.Validation;

namespace DotNetWorkQueue.Transport.RelationalDatabase.Basic.QueryHandler
{
    internal class GetMessageErrorsQueryHandler : IQueryHandler<GetMessageErrorsQuery, Dictionary<string, int>>
    {
        private readonly IPrepareQueryHandler<GetMessageErrorsQuery, Dictionary<string, int>> _prepareQuery;
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IReadColumn _readColumn;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetErrorRetryCountQueryHandler" /> class.
        /// </summary>
        /// <param name="prepareQuery">The prepare query.</param>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="readColumn">The read column.</param>
        public GetMessageErrorsQueryHandler(IPrepareQueryHandler<GetMessageErrorsQuery, Dictionary<string, int>> prepareQuery,
            IDbConnectionFactory connectionFactory,
            IReadColumn readColumn)
        {
            Guard.NotNull(() => prepareQuery, prepareQuery);
            Guard.NotNull(() => connectionFactory, connectionFactory);
            Guard.NotNull(() => readColumn, readColumn);

            _prepareQuery = prepareQuery;
            _connectionFactory = connectionFactory;
            _readColumn = readColumn;
        }
        /// <inheritdoc />
        public Dictionary<string, int> Handle(GetMessageErrorsQuery query)
        {
            var returnData = new Dictionary<string, int>();
            using (var connection = _connectionFactory.Create())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    _prepareQuery.Handle(query, command, CommandStringTypes.GetMessageErrors);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var errorType = _readColumn.ReadAsString(CommandStringTypes.GetMessageErrors, 0, reader).Trim();
                            var count = _readColumn.ReadAsInt32(CommandStringTypes.GetMessageErrors, 1, reader);
                            returnData.Add(errorType, count);
                        }
                    }
                }
            }

            return returnData;
        }
    }
}