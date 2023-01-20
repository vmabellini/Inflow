using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(IMessage message, CancellationToken cancellationToken = default);
        Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default);
    }
}
