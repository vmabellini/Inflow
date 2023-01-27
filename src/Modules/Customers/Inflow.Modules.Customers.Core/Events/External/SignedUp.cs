using Inflow.Shared.Abstractions.Contracts;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Events.External
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;

    [Message("users")]
    internal class SignedUpContract : Contract<SignedUp>
    {
        public SignedUpContract()
        {
            RequireAll();
        }
    }
}
