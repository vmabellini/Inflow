using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand;

        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default) where TResult : class;

        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
    }
}
