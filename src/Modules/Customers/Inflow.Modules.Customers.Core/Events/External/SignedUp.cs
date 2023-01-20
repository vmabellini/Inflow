using Inflow.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Events.External
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}
