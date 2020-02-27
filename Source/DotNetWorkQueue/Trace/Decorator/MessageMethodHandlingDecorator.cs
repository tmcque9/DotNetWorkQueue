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
using DotNetWorkQueue.Messages;
using OpenTracing;

namespace DotNetWorkQueue.Trace.Decorator
{
    /// <summary>
    /// Tracer for linq message handling
    /// </summary>
    /// <seealso cref="DotNetWorkQueue.IMessageMethodHandling" />
    public class MessageMethodHandlingDecorator: IMessageMethodHandling
    {
        private readonly IMessageMethodHandling _handler;
        private readonly ITracer _tracer;
        private readonly IStandardHeaders _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMethodHandlingDecorator"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="tracer">The tracer.</param>
        /// <param name="headers">The headers.</param>
        public MessageMethodHandlingDecorator(IMessageMethodHandling handler, ITracer tracer, IStandardHeaders headers)
        {
            _handler = handler;
            _tracer = tracer;
            _headers = headers;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _handler.Dispose();
        }

        /// <inheritdoc />
        public bool IsDisposed => _handler.IsDisposed;

        /// <inheritdoc />
        public void HandleExecution(IReceivedMessage<MessageExpression> receivedMessage, IWorkerNotification workerNotification)
        {
            var spanContext = receivedMessage.Headers.Extract(_tracer, _headers);
            if (spanContext != null)
            {
                using (IScope scope = _tracer.BuildSpan("LinqExecution").AddReference(References.FollowsFrom, spanContext).StartActive(finishSpanOnDispose: true))
                {
                    scope.Span.SetTag("ActionType", receivedMessage.Body.PayLoad.ToString());
                    _handler.HandleExecution(receivedMessage, workerNotification);
                }
            }
            else
            {
                using (IScope scope = _tracer.BuildSpan("LinqExecution").StartActive(finishSpanOnDispose: true))
                {
                    scope.Span.SetTag("ActionType", receivedMessage.Body.PayLoad.ToString());
                    _handler.HandleExecution(receivedMessage, workerNotification);
                }
            }
        }
    }
}
