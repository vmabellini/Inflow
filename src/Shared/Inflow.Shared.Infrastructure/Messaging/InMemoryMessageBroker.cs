using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Modules;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly ILogger<InMemoryMessageBroker> _logger;

        public InMemoryMessageBroker(IModuleClient moduleClient,
            ILogger<InMemoryMessageBroker> logger)
        {
            _moduleClient = moduleClient;
            _logger = logger;
        }

        public Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
            => PublishAsync(cancellationToken, message);

        public Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default)
            => PublishAsync(cancellationToken, messages);

        private async Task PublishAsync(CancellationToken cancellationToken, params IMessage[] messages)
        {
            if (messages is null)
                return;

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
                return;

            var tasks = messages.Select(x => _moduleClient.PublishAsync(x, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}
