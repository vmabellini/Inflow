using Inflow.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Messaging
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IMessage;
    }
}
