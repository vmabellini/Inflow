using Inflow.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Events.External.Handlers
{
    internal class SignedUpHandler : IEventHandler<SignedUp>
    {
        public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
        {
            await Task.Delay(10000);
            await Task.CompletedTask;
        }
    }
}
