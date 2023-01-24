using Inflow.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Messaging
{
    internal interface IMessageChannel
    {
        ChannelReader<MessageEnvelope> Reader { get; }
        ChannelWriter<MessageEnvelope> Writer { get; }
    }
}
