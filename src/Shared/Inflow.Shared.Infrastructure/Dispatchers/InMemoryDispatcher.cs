using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Dispatchers
{
    internal class InMemoryDispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEventDispatcher _eventDispatcher;

        public InMemoryDispatcher(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IEventDispatcher eventDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _eventDispatcher = eventDispatcher;
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default) where TResult : class 
            => _queryDispatcher.QueryAsync(query, token);

        public Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand 
            => _commandDispatcher.SendAsync(command, cancellationToken);

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent
            => _eventDispatcher.PublishAsync(@event, cancellationToken);
    }
}
