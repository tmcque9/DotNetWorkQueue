﻿// ---------------------------------------------------------------------
//This file is part of DotNetWorkQueue
//Copyright © 2017 Brian Lehnen
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
using DotNetWorkQueue.Transport.RelationalDatabase;
using DotNetWorkQueue.Transport.RelationalDatabase.Basic.Query;
using DotNetWorkQueue.Transport.SQLite.Basic;
using DotNetWorkQueue.Validation;

namespace DotNetWorkQueue.Transport.SQLite.Decorator
{
    /// <summary>
    /// 
    /// </summary>
    public class GetColumnNamesFromTableDecorator : IQueryHandler<GetColumnNamesFromTableQuery, List<string>>
    {
        private readonly IConnectionInformation _connectionInformation;
        private readonly IQueryHandler<GetColumnNamesFromTableQuery, List<string>> _decorated;
        /// <summary>
        /// Initializes a new instance of the <see cref="GetColumnNamesFromTableDecorator" /> class.
        /// </summary>
        /// <param name="connectionInformation">The connection information.</param>
        /// <param name="decorated">The decorated.</param>
        public GetColumnNamesFromTableDecorator(IConnectionInformation connectionInformation,
            IQueryHandler<GetColumnNamesFromTableQuery, List<string>> decorated)
        {
            Guard.NotNull(() => decorated, decorated);
            Guard.NotNull(() => connectionInformation, connectionInformation);
            _connectionInformation = connectionInformation;
            _decorated = decorated;
        }
        /// <summary>
        /// Handles the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<string> Handle(GetColumnNamesFromTableQuery query)
        {
            return !DatabaseExists.Exists(_connectionInformation.ConnectionString) ? new List<string>(0) : _decorated.Handle(query);
        }
    }
}