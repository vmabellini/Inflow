using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Infrastructure.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}
